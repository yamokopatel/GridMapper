using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using GridMapper.ViewModels.Components;
using GridMapper.Views.Components;

public class FileBearer
{
    
    public const ushort VERSION = 0x7D_01;
    public FileBearer(DataBearer bearer)
    {
        this.bearer = bearer;
        bearer.DataChanged += (changed) =>
        {
            isChanged = changed;
        };
    }


    #region FIELDS
        private bool isChanged = false;
        private DataBearer bearer;
        //  File Paths
        private string? MapMetadataFilePath;
        private string? MapStructureFilePath;
        private string? CellMetadataFilePath;
        //  Files
        private FileInfo? MapMetadataFile;
        private FileInfo? MapStructureFile;
        private FileInfo? CellMetadataFile;
    #endregion


    #region GETTERS
        //  File Paths
        public string? GetMapMetadataPath() => MapMetadataFilePath;
        public string? GetMapStructureFilePath() => MapStructureFilePath;
        public string? GetCellMetadataFilePath() => CellMetadataFilePath;
        //  Files
        public FileInfo? GetMapMetadataFile() => MapMetadataFile;
        public FileInfo? GetMapStructureFile() => MapStructureFile;
        public FileInfo? GetCellMetadataFile() => CellMetadataFile;
    #endregion 


    #region SETTERS
    //  Generifier?
        private bool CheckPathNotNull(ref string? path, string newPath)
        {
            if(path != null && isChanged)
            {
                return true;
            }
            path = newPath;
            return false;
        }
        //  Paths
        public void SetMapMetaPath(string newPath)
        {
            if(CheckPathNotNull(ref MapMetadataFilePath, newPath))
            {
                TryLaodFiles();
            }
        }
        public void SetStructurePath(string newPath)
        {
            if(CheckPathNotNull(ref MapStructureFilePath, newPath))
            {
                TryLaodFiles();
            }
        }
        public void SetCellMetaPath(string newPath)
        {
            if(CheckPathNotNull(ref CellMetadataFilePath, newPath))
            {
                TryLaodFiles();
            }
        }
    #endregion


    #region FUNCTIONS
        private void Notify(string text)
        {
            NoticeWindow notice = new NoticeWindow();
            NoticeWindowViewModel nwvm = new NoticeWindowViewModel(text);
            notice.DataContext = nwvm;
            notice.Show();
        }
        public void TryLaodFiles()
        {
            try{
                if(!string.IsNullOrWhiteSpace(MapMetadataFilePath)
                    && !string.IsNullOrWhiteSpace(MapStructureFilePath)
                    && !string.IsNullOrWhiteSpace(CellMetadataFilePath))
                {
                    MapMetadataFile = new FileInfo(MapMetadataFilePath!);
                    MapStructureFile = new FileInfo(MapStructureFilePath!);
                    CellMetadataFile = new FileInfo(CellMetadataFilePath!);
                }
                else
                {
                    if(MapMetadataFilePath != null
                        && MapStructureFilePath != null
                        && CellMetadataFilePath != null)
                    {
                        Notify("Any of provided paths is not valid.");
                    }
                }
            }
            catch(Exception e)
            {
                Notify(e.ToString());
            }
        }
        public void TryLoadData()
        {
            if(MapMetadataFile != null
                && MapStructureFile != null
                && CellMetadataFile != null)
            {
                try
                {
                    LoadList<Map>(MapMetadataFile);
                    LoadStructures();
                    LoadList<BaseObject>(CellMetadataFile);
                }
                catch(Exception e)
                {
                    Notify(e.ToString());
                }
            }
        }
        public void SaveData(List<Map>? maps, List<byte>? structures, List<BaseObject>? objects)
        {
            try
            {
                Notify($"{MapMetadataFile != null}\n{MapStructureFile != null}\n{CellMetadataFile != null}\n{isChanged}");
            }
            catch(Exception e)
            {
                Notify(e.ToString());
            }
            if(MapMetadataFile != null
                && MapStructureFile != null
                && CellMetadataFile != null
                && isChanged)
            {
                SaveList<Map>(ref MapMetadataFile, maps!);
                SaveStructires(structures);
                SaveList<BaseObject>(ref CellMetadataFile, objects!);
            }
            else
            {
                Notify("Nothing to save. Change some data firstly.");
            }
        }
        #region SAVERS
            private void SaveList<T>(ref FileInfo file, List<T> values)
            {
                Container<T> container = new Container<T>(VERSION, values.ToArray());
                FileStream stream = file.OpenWrite();
                StreamWriter writer = new StreamWriter(stream);
                string jsonContainer = JsonSerializer.Serialize<Container<T>>(container);
                writer.Write(jsonContainer);
                writer.Dispose(); stream.Dispose();
            }
            private void SaveStructires(List<byte>? mapStructures)
            {
                byte[] bytes = new byte[mapStructures!.Count + 2];
                bytes[0] = VERSION / 256; bytes[1] = VERSION & 0xFF;
                mapStructures.CopyTo(bytes, 2);
                FileStream stream = MapStructureFile!.OpenWrite();
                stream.Write(bytes);
                stream.Dispose();
            }
        #endregion

        #region LOADERS
            private Exception? LoadList<T>(FileInfo file)
            {
                List<T> values = null!;
                bool isValid = false;
                FileStream stream = file!.OpenRead();
                if(stream.Length == 0)
                {
                    values = new List<T>();
                    isValid = true;
                }
                else
                {
                    StreamReader reader = new StreamReader(stream);
                    Container<T> container = JsonSerializer.Deserialize<Container<T>>(reader.ReadToEnd())!;
                    if(container.GetVersion() == VERSION)
                    {
                        values = container.GetValues();
                        isValid = true;
                    }
                    reader.Dispose();
                }
                stream.Dispose();
                if (isValid)
                {
                    if(typeof(T) == typeof(Map))
                    {
                        MapsLoaded!((List<Map>)(object)values);
                    }
                    else if(typeof(T) == typeof(BaseObject))
                    {
                        ObjectsLoaded!((List<BaseObject>)(object)values);
                    }
                }
                else 
                {
                    return new JsonException();
                }
                return null;
            }
            private Exception? LoadStructures()
            {
                List<byte> structures = null!;
                bool isValid = false;
                FileStream stream = MapStructureFile!.OpenRead();
                if(stream.Length == 0)
                {
                    structures = new List<byte>();
                    isValid = true;
                }
                else
                {
                    byte[] v = new byte[2];
                    stream.ReadExactly(v);
                    if(v[0] * 256 + v[1] == VERSION)
                    {
                        stream.Seek(2, SeekOrigin.Begin);
                        byte[] bytes = new byte[stream.Length - 2];
                        stream.ReadExactly(bytes);
                        structures = bytes.ToList();
                        isValid = true;
                    }
                }
                stream.Dispose();
                if (isValid)
                {
                    OnStructuresLoaded(structures);
                }
                else
                {
                    return new InvalidDataException();
                }
                return null;
            }
        #endregion
    #endregion


    #region EVENTS
        public event Action<List<Map>>? MapsLoaded;
        public event Action<List<BaseObject>>? ObjectsLoaded;
        public event Action<List<byte>>? StructuresLoaded;
    #endregion


    #region SENDERS
        protected virtual void OnMapsLoaded(List<Map> maps)
        {
            MapsLoaded?.Invoke(maps);
        }
        protected virtual void OnObjectsLoaded(List<BaseObject> objects)
        {
            ObjectsLoaded?.Invoke(objects);
        }
        protected virtual void OnStructuresLoaded(List<byte> structures)
        {
            StructuresLoaded?.Invoke(structures);
        }
    #endregion
}