using UnityEngine;

using Assets.Scripts.StageSpecificScripts.Data;



namespace Assets.Scripts.StageSpecificScripts.Manager
{
    public class RockPositionManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] rockGameObjects;
        [SerializeField] private Vector2Int[] rockPositions;

        [SerializeField] private SingleStageData stageData;

        public RockMovement[] RockMovements { get; private set; }



        public void Awake()
        {
            rockPositions = new Vector2Int[rockGameObjects.Length];
            RockMovements = new RockMovement[rockGameObjects.Length];

            for (int index = 0; index < rockGameObjects.Length; index++)
            {
                Vector3 position = rockGameObjects[index].transform.position;

                rockPositions[index] = new Vector2Int()
                {
                    x = (int)position.x - stageData.offset.x,
                    y = (int)position.y - stageData.offset.y
                };

                RockMovements[index] = rockGameObjects[index].GetComponent<RockMovement>();
                RockMovements[index].RockPositoon = rockPositions[index];
                RockMovements[index].Index = index;
            }
        }

        public Vector2Int[] RockPositions
        {
            get => rockPositions;
            set => rockPositions = value;
        }
    }
}
