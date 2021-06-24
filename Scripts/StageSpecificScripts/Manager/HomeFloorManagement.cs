using UnityEngine;

using NATAKER_DLL.StageSpecific.Home.Actor;
using Assets.Scripts.StageSpecificScripts.Manager;



namespace NATAKER_DLL.StageSpecific.Home.Manager

{
    public enum FloorMoveDirection : byte
    {
        None = 0,
        F_0_to_1 = 1,
        F_1_to_0 = 2,
        F_1_to_2 = 4,
        F_2_to_1 = 8
    };

    public sealed class HomeFloorManagement : MonoBehaviour
    {

        [SerializeField] private GameObject floor_0;
        [SerializeField] private GameObject floor_1;
        [SerializeField] private GameObject floor_2;

        [SerializeField] private WallPositionManager floor_0_wallManager;
        [SerializeField] private WallPositionManager floor_1_wallManager;
        [SerializeField] private WallPositionManager floor_2_wallManager;

        [SerializeField] private TotalDoorManager floor_0_totalDoorManager;
        [SerializeField] private TotalDoorManager floor_1_totalDoorManager;
        [SerializeField] private TotalDoorManager floor_2_totalDoorManager;

        [SerializeField] private PlayerMovement_Home player;
        [SerializeField] private Transform playerImage;

        [SerializeField] private FloorEnterManager floorEnterManager;

        [SerializeField] private int curFloor;
        [SerializeField] private bool floorChange;

        [SerializeField] private float layerChangeInterval;

        private FloorMoveDirection floorMoveDirection;



        public void Awake()
        {
            curFloor = 0;

            player.PlayerPosition = new Vector2Int()
            {
                x = 14,
                y = 2
            };
            playerImage.position = new Vector3()
            {
                x = 14.5f,
                y = 2.5f,
                z = 0.0f
            };
            player.WallPositionManager = floor_0_wallManager;
            player.TotalDoorManager = floor_0_totalDoorManager;
        }
        public void Update()
        {
            if (floorEnterManager.Floor_0_to_1_enter.Enter && !floorChange)
            {
                curFloor++;
                floorChange = true;
            }
            else if (floorEnterManager.Floor_1_to_0_enter.Enter && !floorChange)
            {
                curFloor--;
                floorChange = true;
            }
            else if (floorEnterManager.Floor_1_to_2_enter.Enter && !floorChange)
            {
                curFloor++;
                floorChange = true;
            }
            else if (floorEnterManager.Floor_2_to_1_enter.Enter && !floorChange)
            {
                curFloor--;
                floorChange = true;
            }


            if (floorChange)
            {
                switch (curFloor)
                {
                    case 0:
                        player.WallPositionManager = floor_0_wallManager;
                        player.totalDoorManager = floor_0_totalDoorManager;

                        floorEnterManager.Floor_1_to_0_enter.Enter = false;

                        floorMoveDirection = FloorMoveDirection.F_1_to_0;
                        break;

                    case 1:
                        player.WallPositionManager = floor_1_wallManager;
                        player.totalDoorManager = floor_1_totalDoorManager;

                        if (floorEnterManager.Floor_0_to_1_enter.Enter)
                        {
                            floorEnterManager.Floor_0_to_1_enter.Enter = false;

                            floorMoveDirection = FloorMoveDirection.F_0_to_1;
                        }
                        else if (floorEnterManager.Floor_2_to_1_enter.Enter)
                        {
                            floorEnterManager.Floor_2_to_1_enter.Enter = false;

                            floorMoveDirection = FloorMoveDirection.F_2_to_1;
                        }
                        break;

                    case 2:
                        player.WallPositionManager = floor_2_wallManager;
                        player.totalDoorManager = floor_2_totalDoorManager;

                        floorEnterManager.Floor_1_to_2_enter.Enter = false;

                        floorMoveDirection = FloorMoveDirection.F_1_to_2;
                        break;
                }

                Invoke("PlayerPositionCallBack", layerChangeInterval);
            }
            else
            {
                floorMoveDirection = FloorMoveDirection.None;
            }
        }

        private void PlayerPositionCallBack()
        {
            floorChange = false;

            switch (floorMoveDirection)
            {
                case FloorMoveDirection.F_1_to_0:
                    floor_0.SetActive(true);
                    floor_1.SetActive(false);
                    floor_2.SetActive(false);

                    player.PlayerPosition = new Vector2Int()
                    {
                        x = 2,
                        y = 8
                    };
                    playerImage.position = new Vector3()
                    {
                        x = 2.5f,
                        y = 8.5f,
                        z = 0.0f
                    };
                    break;

                case FloorMoveDirection.F_0_to_1:
                    floor_0.SetActive(false);
                    floor_1.SetActive(true);
                    floor_2.SetActive(false);

                    player.PlayerPosition = new Vector2Int()
                    {
                        x = 1,
                        y = 2
                    };
                    playerImage.position = new Vector3()
                    {
                        x = 1.5f,
                        y = 2.5f,
                        z = 0.0f
                    };
                    break;

                case FloorMoveDirection.F_2_to_1:
                    floor_0.SetActive(false);
                    floor_1.SetActive(true);
                    floor_2.SetActive(false);

                    player.PlayerPosition = new Vector2Int()
                    {
                        x = 3,
                        y = 2
                    };
                    playerImage.position = new Vector3()
                    {
                        x = 3.5f,
                        y = 2.5f,
                        z = 0.0f
                    };
                    break;

                case FloorMoveDirection.F_1_to_2:
                    floor_0.SetActive(false);
                    floor_1.SetActive(false);
                    floor_2.SetActive(true);

                    player.PlayerPosition = new Vector2Int()
                    {
                        x = 3,
                        y = 11
                    };
                    playerImage.position = new Vector3()
                    {
                        x = 3.5f,
                        y = 11.5f,
                        z = 0.0f
                    };
                    break;
            }
        }
    }
}