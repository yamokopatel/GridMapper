using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GridMapper.ViewModels.Components;
using GridMapper.Views.Components;

namespace GridMapper.ViewModels.Tabs;

public partial class ObjectEditorViewModel : ViewModelBase
{
    #region FIELDS
    private int CellIndex = -1;
    private BaseObject? mapObject;
    private bool isObjLoaded = false;
    private bool ParentMapChanging = false;
    private List<Map>? maps;
    private List<byte>? structures;
    private List<BaseObject>? mapObjects;
    #endregion

    #region CONSTRUCTOR
    public ObjectEditorViewModel()
    {
        // BearerIn.SelectedCellChanged += (cellIndex) =>
        // {
        //     CellIndex = cellIndex;
        //     if(cellIndex > -1){
        //         int m = cellIndex / 256, x = cellIndex % 256 % 16, z = cellIndex % 256 / 16;
        //         int mid = BearerIn.GetMaps()![m].GetMapId();
        //         mapObject = BearerIn.GetMapObjects()!.Find(mo => mo.GetMapId() == mid 
        //             && mo.GetX() == x 
        //             && mo.GetZ() == z);
        //         isObjLoaded = true;
        //         switch (mapObject)
        //         {
        //             case null: SelectedObjectType = 0; break;
        //             case Chest: SelectedObjectType = 1; break;
        //             case Group: SelectedObjectType = 2; break;
        //             case Transition: SelectedObjectType = 3; break;
        //         }
        //     }
        // };
    }
    #endregion

    #region PROPERTIES
    [ObservableProperty]
    public partial ObservableCollection<ComboBoxItem> MapOptions { get; set; } = new ObservableCollection<ComboBoxItem>();
    [ObservableProperty]
    public partial int SelectedMap { get; set; } = -1;
    //  CHOSES
    //  Object Type
    [ObservableProperty]
    public partial int SelectedObjectType { get; set; } = 0;
    //  PARAMETERS
    //  Chest
    private bool ChangingSubject = false;
    private int processableSubject = -1;
    [ObservableProperty]
    public partial int SelectedSubject { get; set; } = -1;
    [ObservableProperty]
    public partial ObservableCollection<byte> SubjectList { get; set; } = new ObservableCollection<byte>();
    [ObservableProperty]
    public partial decimal? SubjectId { get; set; } = null;
    //  Group
    private bool ChangingEnemy = false;
    private int processableEnemy = -1;
    [ObservableProperty]
    public partial int SelectedEnemy { get; set; } = -1;
    [ObservableProperty]
    public partial ObservableCollection<byte> EnemyList { get; set; } = new ObservableCollection<byte>();
    [ObservableProperty]
    public partial decimal? EnemyId { get; set; } = null;
    //  Transition
    //  map
    [ObservableProperty]
    public partial int SelectedNextMap { get; set; } = -1;
    //  x
    [ObservableProperty]
    public partial decimal? NextXValue { get; set; } = null;
    //  z
    [ObservableProperty]
    public partial decimal? NextZValue { get; set; } = null;
    //  x direction
    [ObservableProperty]
    public partial decimal? NextXDirValue { get; set; } = null;
    //  z direction
    [ObservableProperty]
    public partial decimal? NextZDirValue { get; set; } = null;
    //  VISIBILITY
    //  Object
    //  chest
    [ObservableProperty]
    public partial bool ChestChosen { get; set; } = false;
    //  group
    [ObservableProperty]
    public partial bool GroupChosen { get; set; } = false;
    //  transition
    [ObservableProperty]
    public partial bool TransitionChosen { get; set; } = false;
    //  transition parameters
    [ObservableProperty]
    public partial bool TransitionParametersVisible { get; set; } = false;
    #endregion

    #region PROPERTY EVENTS
    partial void OnSelectedMapChanged(int value)
    {
        if (!ParentMapChanging)
        {
            OnOtherMapSelected(value);
        }
    }
    partial void OnSelectedObjectTypeChanged(int value)
    {
        ChestChosen = (value == 1);
        GroupChosen = (value == 2);
        TransitionChosen = (value ==3);
    }
    /*partial void OnSelectedObjectTypeChanged(int oldValue, int newValue)
    {
        if(!isObjLoaded)
        {
            if(oldValue != newValue)
            {
                if(oldValue == 0)
                {
                    int newIndex = -1;
                    switch (newValue)
                    {
                        case 1: newIndex = BearerIn.CreateObject<Chest>(); break;
                        case 2: newIndex = BearerIn.CreateObject<Group>(); break;
                        case 3: newIndex = BearerIn.CreateObject<Transition>(); break;
                    }
                    if(newIndex > -1)
                    {
                        mapObject = BearerIn.GetMapObjects()![newIndex];
                    }
                }
                else
                {
                    if(newValue == 0)
                    {
                        BearerIn.DeleteObject();
                        mapObject = null;
                    }
                    else
                    {
                        switch (newValue)
                        {
                            case 1:
                                mapObject = BearerIn.ChangeObject<Chest>();
                                processableSubject = -1;
                                break;
                            case 2:
                                mapObject = BearerIn.ChangeObject<Group>();
                                processableEnemy = -1;
                                break;
                            case 3:
                                mapObject = BearerIn.ChangeObject<Transition>();
                                break;
                        }
                    }
                }
            }
        }
        else
        {
            ClearFields();
            switch (mapObject)
            {
                case Chest chest:
                    processableSubject = -1;
                    SubjectList = new ObservableCollection<byte>(chest.GetSubjectIds());
                    break;
                case Group group:
                    processableEnemy = -1;
                    EnemyList = new ObservableCollection<byte>(group.GetEnemyIds());
                    break;
                case Transition transition:
                    FillTransition(transition);
                    break;
                default:
                    // I don't remember, what I wanted here...
                    break;
            }
            isObjLoaded = false;
        }
    }*/
    partial void OnSelectedSubjectChanged(int value)
    {
        ChangingSubject = true;
        if(value > -1)
        {
            processableSubject = value;
        }
        SubjectId = SubjectList[processableSubject];
        ChangingSubject = false;
    }
    /*partial void OnSubjectIdChanged(decimal? value)
    {
        if (!ChangingSubject)
        {
            SubjectList[processableSubject] = (byte)value!;
            BearerIn.SetSubject(processableSubject, (byte)value);
        }
    }*/
    partial void OnSelectedEnemyChanged(int value)
    {
        ChangingEnemy = true;
        if(value > -1)
        {
            processableEnemy = value;
        }
        EnemyId = EnemyList[processableEnemy];
        ChangingEnemy = false;
    }
    /*partial void OnEnemyIdChanged(decimal? value)
    {
        if (!ChangingEnemy)
        {
            EnemyList[processableEnemy] = (byte)value!;
            BearerIn.SetEnemy(processableSubject, (byte)value);
        }
    }*/
    /*partial void OnSelectedNextMapChanged(int value)
    {
        TransitionParametersVisible = (value > -1);
        if(value > -1)
        {
            BearerIn.SetNMapId((byte)value);
        }
    }*/
    /*partial void OnNextXValueChanged(decimal? value)
    {
        if(value != null)
        {
            BearerIn.SetNX((byte)value);
        }
    }*/
    /*partial void OnNextZValueChanged(decimal? value)
    {
        if(value != null)
        {
            BearerIn.SetNZ((byte)value);
        }
    }*/
    /*partial void OnNextXDirValueChanged(decimal? oldValue, decimal? newValue)
    {
        if(oldValue != newValue)
        {
            if(newValue != null)
            {
                if(newValue != 0 && NextZDirValue != 0)
                {
                    NextZDirValue = 0;
                    BearerIn.SetNDir(new Direction((sbyte)newValue, 0));
                }
            }
            else if(NextZDirValue != null)
            {
                NextZDirValue = null;
                BearerIn.SetNDir(new Direction());
            }
        }
    }*/
    /*partial void OnNextZDirValueChanged(decimal? oldValue, decimal? newValue)
    {
        if(oldValue != newValue)
        {
            if(newValue != null)
            {
                if(newValue != 0 && NextXDirValue != 0)
                {
                    NextXDirValue = 0;
                    BearerIn.SetNDir(new Direction(0, (sbyte)newValue));
                }
            }
            else if(NextXDirValue != null)
            {
                NextXDirValue = null;
                BearerIn.SetNDir(new Direction());
            }
        }
    }*/
    #endregion

    #region SETTERS
    public void SetSelectedMap(int index)
    {
        if(SelectedMap != index)
        {
            ParentMapChanging = true;
            SelectedMap = index;
            ParentMapChanging = false;
        }
    }
    public void SetMapList(List<Map> maps)
    {
        this.maps = maps;
        MapsToItems();
    }
    public void SetStructureList(List<byte> structures)
    {
        this.structures = structures;
    }
    public void SetObjectList(List<BaseObject> objects)
    {
        this.mapObjects = objects;
    }
    #endregion

    #region FUNCTIONS
    private void MapsToItems()
    {
        MapOptions.Clear();
        for(int a = 0; a < maps!.Count; a++)
        {
            ComboBoxItem item = new ComboBoxItem();
            item.Content = $"{maps![a].GetMapName()} ({maps![a].GetMapId()})";
            MapOptions.Add(item);
        }
    }
    public void UpdateSelector()
    {
        MapsToItems();
    }
    private void ClearFields()
    {
        //chest
        SelectedSubject = -1;
        SubjectId = null;
        SubjectList = new ObservableCollection<byte>();
        //group
        SelectedEnemy = -1;
        EnemyId = null;
        EnemyList = new ObservableCollection<byte>();
        //transition
        SelectedNextMap = -1;
        NextXValue = NextZValue = null;
        NextXDirValue = null;

    }
    /*private void FillTransition(Transition obj)
    {
        SelectedNextMap = BearerIn.GetMaps()!.FindIndex(t => t.GetMapId() == obj!.GetMapId());
        NextXValue = obj.GetNextX(); NextZValue = obj.GetNextZ();
        NextXDirValue = obj.GetNextDirection().X; NextZDirValue = obj.GetNextDirection().Z;
    }*/
    #endregion

    #region COMMANDS
    /*[RelayCommand]
    private void CreateSubject()
    {
        SubjectList.Add(0);
        SelectedSubject = BearerIn.AddSubject();
    }*/
    /*[RelayCommand]
    private void DeleteSubject()
    {
        int toDel;
        if(SubjectList.Count > 0 && processableSubject != -1)
        {
            toDel = processableSubject;
            processableSubject = -1;
            SubjectList.RemoveAt(toDel);
            BearerIn.RemoveSubject(toDel);
        }
    }*/
    /*[RelayCommand]
    private void CreateEnemy()
    {
        EnemyList.Add(0);
        SelectedEnemy = BearerIn.AddEnemy();
    }*/
    /*[RelayCommand]
    private void DeleteEnemy()
    {
        int toDel;
        if(EnemyList.Count > 0 && processableEnemy != -1){
            toDel = processableEnemy;
            processableEnemy = -1;
            EnemyList.RemoveAt(toDel);
            BearerIn.RemoveEnemy(toDel);
        }
    }*/
    #endregion

    #region EVENTS
    public event Action<int>? OtherMapSelected;
    #endregion

    #region EVENT SENDERS
    protected virtual void OnOtherMapSelected(int mapIndex)
    {
        OtherMapSelected?.Invoke(mapIndex);
    }
    #endregion
}