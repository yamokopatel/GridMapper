using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GridMapper.ViewModels.Components;
using GridMapper.Views.Components;

namespace GridMapper.ViewModels.Tabs;

public partial class ObjectEditorViewModel : ViewModelBase
{
    private DataBearer BearerIn;
    private int CellIndex = -1;
    private BaseObject? mapObject;
    private bool isObjLoaded = false;

    #region PROPERTIES
    //  CHOSES
    //  Object Type
    [ObservableProperty]
    public partial int SelectedObjectType { get; set; } = 0;
    partial void OnSelectedObjectTypeChanged(int value)
    {
        ChestChosen = (value == 1);
        GroupChosen = (value == 2);
        TransitionChosen = (value ==3);
    }
    partial void OnSelectedObjectTypeChanged(int oldValue, int newValue)
    {
        if(!isObjLoaded)
        {
            if(oldValue != newValue)
            {
                if(oldValue == 0)
                {
                    int m = CellIndex / 256, x = CellIndex % 256 % 16, z = CellIndex % 256 / 16;
                    int mid = BearerIn.GetMaps()![m].GetMapId();
                    List<ushort> occupedObjIds = new List<ushort>();
                    for(int a = 0; a < BearerIn.GetMapObjects()!.Count; a++)
                    {
                        occupedObjIds.Add(BearerIn.GetMapObjects()![a].GetObjectId());
                    }
                    int newObjId = -1;
                    for(int a = 0; a < 65_536; a++)
                    {
                        if (!occupedObjIds.Contains((ushort)a))
                        {
                            newObjId = (ushort)a;
                            break;
                        }
                    }
                    if(newObjId != -1)
                    {
                        BaseObject newObject = null!;
                        switch (newValue)
                        {
                            case 1: newObject = new Chest((ushort)newObjId, (byte)mid, (byte)x, (byte)z, []); break;
                            case 2: newObject = new Group((ushort)newObjId, (byte)mid, (byte)x, (byte)z, []); break;
                            case 3: newObject = new Transition((ushort)newObjId, (byte)mid, (byte)x, (byte)z,0, 0, 0, new Direction()); break;
                        }
                        BearerIn.GetMapObjects()!.Add(newObject);
                        mapObject = BearerIn.GetMapObjects()![BearerIn.GetMapObjects()!.Count - 1];
                    }
                    else
                    {
                        NoticeWindow notice = new NoticeWindow();
                        NoticeWindowViewModel nwvm = new NoticeWindowViewModel("New map object will be out of limit.");
                        notice.DataContext = nwvm;
                    }
                }
                else
                {
                    if(newValue == 0)
                    {
                        BearerIn.GetMapObjects()!.Remove(mapObject!);
                        mapObject = null;
                    }
                    else
                    {
                        int currentIndex = BearerIn.GetMapObjects()!.FindIndex(obj => obj == mapObject);
                        BaseObject newObj = null!;
                        switch (newValue)
                        {
                            case 1:
                                newObj = new Chest(mapObject!.GetObjectId(), mapObject.GetMapId(),
                                    mapObject.GetX(), mapObject.GetZ(), []);
                                processableSubject = -1;
                                break;
                            case 2:
                                newObj = new Chest(mapObject!.GetObjectId(), mapObject.GetMapId(),
                                    mapObject.GetX(), mapObject.GetZ(), []);
                                break;
                            case 3:
                                newObj = new Transition(mapObject!.GetObjectId(), mapObject.GetMapId(),
                                    mapObject.GetX(), mapObject.GetZ(), 0, 0, 0, new Direction());
                                break;
                        }
                        BearerIn.GetMapObjects()![currentIndex] = newObj;
                        mapObject = BearerIn.GetMapObjects()![currentIndex];
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
                    EnemyList = new ObservableCollection<byte>(group.GetEnemyIds());
                    break;
                case Transition transition:
                    FillTransition(transition);
                    break;
                default:

                    break;
            }
            isObjLoaded = false;
        }
    }
    //  PARAMETERS
    //  Chest
    private bool ChangingSubject = false;
    private int processableSubject = -1;
    [ObservableProperty]
    public partial int SelectedSubject { get; set; } = -1;
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
    [ObservableProperty]
    public partial ObservableCollection<byte> SubjectList { get; set; } = new ObservableCollection<byte>();
    [ObservableProperty]
    public partial decimal? SubjectId { get; set; } = null;
    partial void OnSubjectIdChanged(decimal? value)
    {
        if (!ChangingSubject)
        {
            SubjectList[processableSubject] = (byte)value!;
            // Chest chest = (Chest)mapObject!;
            // chest.SetSubjectIds(SubjectList.ToList());
            // int currentObjectIndex = BearerIn.GetMapObjects()!.FindIndex(c => c == mapObject);
            // BearerIn.GetMapObjects()![currentObjectIndex] = chest;
            // mapObject = BearerIn.GetMapObjects()![currentObjectIndex];
        }
    }
    //  Group
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
    partial void OnSelectedNextMapChanged(int value)
    {
        TransitionParametersVisible = (value > -1);
    }
    //  x
    [ObservableProperty]
    public partial decimal? NextXValue { get; set; } = null;
    //  y
    [ObservableProperty]
    public partial decimal? NextZValue { get; set; } = null;
    //  x direction
    [ObservableProperty]
    public partial decimal? NextXDirValue { get; set; } = null;
    partial void OnNextXDirValueChanged(decimal? oldValue, decimal? newValue)
    {
        if(oldValue != newValue)
        {
            if(newValue != null)
            {
                if(newValue != 0 && NextZDirValue != 0)
                {
                    NextZDirValue = 0;
                }
            }
            else if(NextZDirValue != null)
            {
                NextZDirValue = null;
            }
        }
    }
    //  y direction
    [ObservableProperty]
    public partial decimal? NextZDirValue { get; set; } = null;
    partial void OnNextZDirValueChanged(decimal? oldValue, decimal? newValue)
    {
        if(oldValue != newValue)
        {
            if(newValue != null)
            {
                if(newValue != 0 && NextXDirValue != 0)
                {
                    NextXDirValue = 0;
                }
            }
            else if(NextXDirValue != null)
            {
                NextXDirValue = null;
            }
        }
    }
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

    public ObjectEditorViewModel(DataBearer bearerIn)
    {
        BearerIn = bearerIn;
        BearerIn.SelectedCellChanged += (cellIndex) =>
        {
            CellIndex = cellIndex;
            if(cellIndex > -1){
                int m = cellIndex / 256, x = cellIndex % 256 % 16, z = cellIndex % 256 / 16;
                int mid = BearerIn.GetMaps()![m].GetMapId();
                mapObject = BearerIn.GetMapObjects()!.Find(mo => mo.GetMapId() == mid 
                    && mo.GetX() == x 
                    && mo.GetZ() == z);
                isObjLoaded = true;
                switch (mapObject)
                {
                    case null: SelectedObjectType = 0; break;
                    case Chest: SelectedObjectType = 1; break;
                    case Group: SelectedObjectType = 2; break;
                    case Transition: SelectedObjectType = 3; break;
                }
            }
        };
    }

    #region FUNCTIONS
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
    private void FillTransition(Transition obj)
    {
        SelectedNextMap = BearerIn.GetMaps()!.FindIndex(t => t.GetMapId() == obj!.GetMapId());
        NextXValue = obj.GetNextX(); NextZValue = obj.GetNextZ();
        NextXDirValue = obj.GetNextDirection().X; NextZDirValue = obj.GetNextDirection().Z;
    }
    #endregion

    #region COMMANDS
    [RelayCommand]
    private void CreateSubject()
    {
        SubjectList.Add(0);
        SelectedSubject = SubjectList.Count - 1;
    }
    [RelayCommand]
    private void DeleteSubject()
    {
        int toDel;
        if(SubjectList.Count > 0 && SelectedSubject != -1)
        {
            toDel = processableSubject;
            processableSubject = -1;
            SubjectList.RemoveAt(toDel);
        }
    }
    [RelayCommand]
    private void CreateEnemy()
    {
        EnemyList.Add(0);
        SelectedEnemy = EnemyList.Count - 1;
    }
    [RelayCommand]
    private void DeleteEnemy()
    {
        if(EnemyList.Count > 0 && SelectedEnemy != -1){
            int toDel = SelectedEnemy;
            SelectedEnemy = -1;
            EnemyList.RemoveAt(toDel);
        }
    }
    #endregion
}