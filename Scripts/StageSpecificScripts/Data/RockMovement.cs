using UnityEngine;

using NATAKER_DLL.StageSpecific.Home.Actor;
using Assets.Scripts.StageSpecificScripts.Manager;



namespace Assets.Scripts.StageSpecificScripts.Data
{
    public class RockMovement : MonoBehaviour
    {
        private Animator animator;

        [SerializeField] private PlayerMovement_Stage playerMovement;

        [SerializeField] private RockPositionManager rockPositionManager;
        [SerializeField] private WallPositionManager wallPositionManager;

        public Vector2Int RockPositoon { get; set; }
        public int Index { get; set; }

        private bool move;

        private float callBackTime;



        public void Awake()
        {
            animator = GetComponent<Animator>();

            callBackTime = animator.runtimeAnimatorController.animationClips[0].length / 2.0f;
        }

        public void OnTriggerStay2D(Collider2D other)
        {
            if (playerMovement.Kicking)
            {
                switch (playerMovement.CurKeyCode)
                {
                    case KeyCode.W:
                        if (!(RockPositoon.y == playerMovement.PlayerPosition.y + 1 && RockPositoon.x == playerMovement.PlayerPosition.x) || !(wallPositionManager.WallPosition[RockPositoon.y + 1 - wallPositionManager.Origin.y, RockPositoon.x - wallPositionManager.Origin.x]))
                        {
                            break;
                        }

                        foreach (Vector2Int coord in rockPositionManager.RockPositions)
                        {
                            if (RockPositoon.x == coord.x && RockPositoon.y + 1 == coord.y)
                            {
                                move = false;
                                break;
                            }
                        }

                        if (move)
                        {
                            RockPositoon = new Vector2Int()
                            {
                                x = RockPositoon.x,
                                y = RockPositoon.y + 1
                            };

                            rockPositionManager.RockPositions[Index].y += 1;
                            animator.SetBool("Up", true);

                            Invoke("AnimationCallBack", callBackTime);

                            move = false;
                        }
                        break;

                    case KeyCode.S:
                        if (!(RockPositoon.y == playerMovement.PlayerPosition.y - 1 && RockPositoon.x == playerMovement.PlayerPosition.x) || !(wallPositionManager.WallPosition[RockPositoon.y - 1 - wallPositionManager.Origin.y, RockPositoon.x - wallPositionManager.Origin.x]))
                        {
                            break;
                        }

                        foreach (Vector2Int coord in rockPositionManager.RockPositions)
                        {
                            if (RockPositoon.x == coord.x && RockPositoon.y - 1 == coord.y)
                            {
                                move = false;
                                break;
                            }
                        }

                        if (move)
                        {
                            RockPositoon = new Vector2Int()
                            {
                                x = RockPositoon.x,
                                y = RockPositoon.y - 1
                            };

                            rockPositionManager.RockPositions[Index].y -= 1;
                            animator.SetBool("Down", true);

                            Invoke("AnimationCallBack", callBackTime);

                            move = false;
                        }
                        break;

                    case KeyCode.A:
                        if (!(RockPositoon.y == playerMovement.PlayerPosition.y && RockPositoon.x == playerMovement.PlayerPosition.x - 1) || !(wallPositionManager.WallPosition[RockPositoon.y - wallPositionManager.Origin.y, RockPositoon.x - 1 - wallPositionManager.Origin.x]))
                        {
                            break;
                        }

                        foreach (Vector2Int coord in rockPositionManager.RockPositions)
                        {
                            if (RockPositoon.x - 1 == coord.x && RockPositoon.y == coord.y)
                            {
                                move = false;
                                break;
                            }
                        }

                        if (move)
                        {
                            RockPositoon = new Vector2Int()
                            {
                                x = RockPositoon.x - 1,
                                y = RockPositoon.y
                            };

                            rockPositionManager.RockPositions[Index].x -= 1;
                            animator.SetBool("Left", true);

                            Invoke("AnimationCallBack", callBackTime);

                            move = false;
                        }
                        break;

                    case KeyCode.D:
                        if (!(RockPositoon.y == playerMovement.PlayerPosition.y && RockPositoon.x == playerMovement.PlayerPosition.x + 1) || !(wallPositionManager.WallPosition[RockPositoon.y - wallPositionManager.Origin.y, RockPositoon.x + 1 - wallPositionManager.Origin.x]))
                        {
                            break;
                        }

                        foreach (Vector2Int coord in rockPositionManager.RockPositions)
                        {
                            if (RockPositoon.x + 1 == coord.x && RockPositoon.y == coord.y)
                            {
                                move = false;
                                break;
                            }
                        }

                        if (move)
                        {
                            RockPositoon = new Vector2Int()
                            {
                                x = RockPositoon.x + 1,
                                y = RockPositoon.y
                            };

                            rockPositionManager.RockPositions[Index].x += 1;
                            animator.SetBool("Right", true);

                            Invoke("AnimationCallBack", callBackTime);

                            move = false;
                        }
                        break;
                }
            }
        }
        public void OnTriggerEnter2D(Collider2D other)
        {
            move = true;
        }

        private void AnimationCallBack()
        {
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
        }
    }
}