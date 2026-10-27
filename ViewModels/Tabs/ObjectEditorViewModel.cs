using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using GridMapper.ViewModels.Components;
using GridMapper.Views.Components;

namespace GridMapper.ViewModels.Tabs;

public partial class ObjectEditorViewModel : ViewModelBase
{
    private DataBearer BearerIn;
    private int CellIndex = -1;
    private BaseObject? mapObject;

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
    //  PARAMETERS
    //  Chest
    [ObservableProperty]
    public partial int SelectedSubject { get; set; } = -1;
    [ObservableProperty]
    public partial decimal? SubjectId { get; set; } = null;
    //  Group
    [ObservableProperty]
    public partial int SelectedEnemy { get; set; } = -1;
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
    public partial decimal? NextYValue { get; set; } = null;
    //  x direction
    [ObservableProperty]
    public partial decimal? NextXDirValue { get; set; } = null;
    partial void OnNextXDirValueChanged(decimal? value)
    {
        if(NextYDirValue != 0)
        {
            NextYDirValue = 0;
        }
    }
    //  y direction
    [ObservableProperty]
    public partial decimal? NextYDirValue { get; set; } = null;
    partial void OnNextYDirValueChanged(decimal? value)
    {
        if(NextXDirValue != 0)
        {
            NextXDirValue = 0;
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
            }
        };
    }
}