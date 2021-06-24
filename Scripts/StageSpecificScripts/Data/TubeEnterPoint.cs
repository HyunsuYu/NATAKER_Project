using UnityEngine;

using NATAKER_DLL.StageSpecific.Home.Actor;
using Assets.Scripts.StageSpecificScripts.Data;



namespace Assets.Scripts.StageSpecificScripts.Data
{
    public enum TubeEnterPosition : byte
    {
        Up, Down, Left, Right, None
    };

    public sealed class TubeEnterPoint : MonoBehaviour
    {
        public bool active;
        public TubeEnterPosition tubeEnterPosition;

        public Vector2Int tubeEnterPointPosition;
        public PlayerMovement_Stage playerMovement;

        public SingleStageData stageData;



        public void Awake()
        {
            tubeEnterPointPosition.x -= stageData.offset.x;
            tubeEnterPointPosition.y -= stageData.offset.y;
        }
        public void OnTriggerStay2D(Collider2D other)
        {
            if (other.tag == "Player" && tubeEnterPointPosition == playerMovement.playerPosition)
            {
                if (tubeEnterPosition == TubeEnterPosition.Up && Input.GetKey(KeyCode.W))
                {
                    active = true;
                }
                else if (tubeEnterPosition == TubeEnterPosition.Down && Input.GetKey(KeyCode.S))
                {
                    active = true;
                }
                else if (tubeEnterPosition == TubeEnterPosition.Left && Input.GetKey(KeyCode.A))
                {
                    active = true;
                }
                else if (tubeEnterPosition == TubeEnterPosition.Right && Input.GetKey(KeyCode.D))
                {
                    active = true;
                }
            }
        }
    }
}