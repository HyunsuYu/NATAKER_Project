using UnityEngine;

using NATAKER_DLL.StageSpecific.Home.Data;



namespace NATAKER_DLL.StageSpecific.Home.Manager
{
    public sealed class FloorEnterManager : MonoBehaviour
    {
        [SerializeField] private SingleFloorEnter floor_0_to_1_enter;
        [SerializeField] private SingleFloorEnter floor_1_to_0_enter;

        [SerializeField] private SingleFloorEnter floor_1_to_2_enter;
        [SerializeField] private SingleFloorEnter floor_2_to_1_enter;



        public SingleFloorEnter Floor_0_to_1_enter
        {
            get => floor_0_to_1_enter;
        }
        public SingleFloorEnter Floor_1_to_0_enter
        {
            get => floor_1_to_0_enter;
        }
        public SingleFloorEnter Floor_1_to_2_enter
        {
            get => floor_1_to_2_enter;
        }
        public SingleFloorEnter Floor_2_to_1_enter
        {
            get => floor_2_to_1_enter;
        }
    }
}