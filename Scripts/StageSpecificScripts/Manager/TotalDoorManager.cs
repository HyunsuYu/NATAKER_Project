using System.Collections.Generic;

using UnityEngine;



namespace Assets.Scripts.StageSpecificScripts.Manager
{
    public enum Side : byte
    {
        Left, Right
    };

    public sealed class TotalDoorManager : MonoBehaviour
    {
        [SerializeField] private List<Vector2Int> doorPositions;
        [SerializeField] private List<Side> doorSides;
        [SerializeField] private List<Animator> doorAnimators;

        [SerializeField] private int activeDoorIndex;
        private bool isToUp;



        public void Awake()
        {
            activeDoorIndex = -1;
        }
        public void Update()
        {
            if (activeDoorIndex != -1)
            {
                switch(isToUp)
                {
                    case true:
                        switch (doorSides[activeDoorIndex])
                        {
                            case Side.Left:
                                doorAnimators[activeDoorIndex].Play("DoorOpen_Left_ToUp");
                                break;

                            case Side.Right:
                                doorAnimators[activeDoorIndex].Play("DoorOpen_Right_ToUp");
                                break;
                        }
                        break;

                    case false:
                        switch (doorSides[activeDoorIndex])
                        {
                            case Side.Left:
                                doorAnimators[activeDoorIndex].Play("DoorOpen_Left_ToDown");
                                break;

                            case Side.Right:
                                doorAnimators[activeDoorIndex].Play("DoorOpen_Right_ToDown");
                                break;
                        }
                        break;
                }

                doorPositions.RemoveAt(activeDoorIndex);
                doorSides.RemoveAt(activeDoorIndex);
                doorAnimators.RemoveAt(activeDoorIndex);

                activeDoorIndex = -1;
            }
        }

        public List<Vector2Int> DoorPositions
        {
            get => doorPositions;
        }
        public List<Side> DoorSides
        {
            get => doorSides;
        }
        public int ActiveDoorIndex
        {
            get => activeDoorIndex;
            set => activeDoorIndex = value;
        }
        public bool IsToUp
        {
            get => isToUp;
            set => isToUp = value;
        }
    }
}