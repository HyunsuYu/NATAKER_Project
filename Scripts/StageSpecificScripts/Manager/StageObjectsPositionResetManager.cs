using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

using Assets.Scripts.StageSpecificScripts.Data;



namespace Assets.Scripts.StageSpecificScripts.Manager
{
    public sealed class StageObjectsPositionResetManager : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private OptionManager optionManager;

        [SerializeField] private SingleStageData singleStageData;

        [SerializeField] private Transform[] arrowPositions;
        [SerializeField] private SingleArrowBlockManager[] arrowBlockManagers;

        [SerializeField] private Transform[] rockPositions;

        [SerializeField] private RockPositionManager rockPositionManager;

        [SerializeField] private SingleStageData stageData;

        private Vector3[] rockBasePositions;



        public void Awake()
        {
            rockBasePositions = new Vector3[rockPositions.Length];

            for(int index = 0; index < rockPositions.Length; index++)
            {
                rockBasePositions[index] = rockPositions[index].position;
            }
        }
        public void ResetObjectPositions()
        {
            foreach(var temp in arrowPositions)
            {
                temp.localPosition = new Vector3()
                {
                    x = 0.0f,
                    y = 0.0f,
                    z = 0.0f
                };
            }
            foreach (var temp in arrowBlockManagers)
            {
                temp.ArrowCount = 3;
            }

            for (int index = 0; index < rockBasePositions.Length; index++)
            {
                rockPositionManager.RockPositions[index] = new Vector2Int()
                {
                    x = (int)rockBasePositions[index].x - stageData.offset.x,
                    y = (int)rockBasePositions[index].y - stageData.offset.y
                };
                rockPositionManager.RockMovements[index].RockPositoon = rockPositionManager.RockPositions[index];
            }
            for(int index = 0; index < rockBasePositions.Length; index++)
            {
                rockPositions[index].position = rockBasePositions[index];
            }

            foreach(var temp in singleStageData.statueManagers)
            {
                temp.Active = false;
                temp.Light2D.enabled = false;
            }

            optionManager.OptionIsActive = false;
            panel.SetActive(false);
        }
    }
}