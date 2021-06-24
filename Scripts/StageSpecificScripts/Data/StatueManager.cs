using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

using NATAKER_DLL.StageSpecific.Home.Actor;
using Assets.Scripts.StageSpecificScripts.Data;



namespace Assets.Scripts.StageSpecificScripts.Data
{
    public enum RGB : byte
    {
        R, G, B
    };

    public sealed class StatueManager : MonoBehaviour
    {
        [SerializeField] private Vector2Int statuePosition;
        [SerializeField] private PlayerMovement_Stage playerMovement;

        [SerializeField] public Light2D light2D;

        [SerializeField] private StatueHitBox[] statueHitBoxes;

        [SerializeField] private SingleStageData stageData;

        public bool active;




        public void Awake()
        {
            light2D.enabled = false;

            statuePosition.x -= stageData.offset.x;
            statuePosition.y -= stageData.offset.y;
        }
        public void FixedUpdate()
        {
            if (active == false && playerMovement.Kicking)
            {
                foreach (StatueHitBox statueHitBox in statueHitBoxes)
                {
                    switch(statueHitBox.keyCode)
                    {
                        case KeyCode.W:
                            if(statuePosition.x == playerMovement.PlayerPosition.x && statuePosition.y == playerMovement.PlayerPosition.y + 1)
                            {
                                active = true;

                                break;
                            }
                            break;

                        case KeyCode.S:
                            if (statuePosition.x == playerMovement.PlayerPosition.x && statuePosition.y == playerMovement.PlayerPosition.y - 1)
                            {
                                active = true;

                                break;
                            }
                            break;

                        case KeyCode.A:
                            if (statuePosition.x == playerMovement.PlayerPosition.x - 1 && statuePosition.y == playerMovement.PlayerPosition.y)
                            {
                                active = true;

                                break;
                            }
                            break;

                        case KeyCode.D:
                            if (statuePosition.x == playerMovement.PlayerPosition.x + 1 && statuePosition.y == playerMovement.PlayerPosition.y)
                            {
                                active = true;

                                break;
                            }
                            break;
                    }
                }
            }
            else if (active == true && light2D.enabled == false)
            {
                light2D.enabled = true;
            }
        }
    }
}