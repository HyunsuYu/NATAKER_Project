using UnityEngine;

using Assets.Scripts.StageSpecificScripts.Manager;



namespace Assets.Scripts.StageSpecificScripts.Data
{
    public sealed class SingleStageData : MonoBehaviour
    {
        public Vector2Int startPosition;
        public Vector3 StartPosition_Real;

        public Vector2Int offset;

        public RockPositionManager rockPositionManager;
        public WallPositionManager wallPositionManager;

        public int maxMoveChance;

        public StatueManager[] statueManagers;
    }
}