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
        public bool Active { get; set; }

        [SerializeField] private TubeEnterPosition tubeEnterPosition;

        [SerializeField] private Vector2Int tubeEnterPointPosition;
        [SerializeField] private PlayerMovement_Stage playerMovement;

        [SerializeField] private SingleStageData stageData;



        public void Awake()
        {
            tubeEnterPointPosition.x -= stageData.offset.x;
            tubeEnterPointPosition.y -= stageData.offset.y;
        }
        public void OnTriggerStay2D(Collider2D other)
        {
            if (other.tag == "Player" && tubeEnterPointPosition == playerMovement.PlayerPosition && playerMovement.Kicking)
            {
                if (tubeEnterPosition == TubeEnterPosition.Up && Input.GetKey(KeyCode.W))
                {
                    Active = true;
                }
                else if (tubeEnterPosition == TubeEnterPosition.Down && Input.GetKey(KeyCode.S))
                {
                    Active = true;
                }
                else if (tubeEnterPosition == TubeEnterPosition.Left && Input.GetKey(KeyCode.A))
                {
                    Active = true;
                }
                else if (tubeEnterPosition == TubeEnterPosition.Right && Input.GetKey(KeyCode.D))
                {
                    Active = true;
                }
            }
        }
    }
}