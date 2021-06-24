using UnityEngine;

using Assets.Scripts.StageSpecificScripts.Manager;



namespace NATAKER_DLL.StageSpecific.Home.Actor
{
    public sealed class PlayerMovement_Home : MonoBehaviour
    {
        public Animator childPlayerAnimator;

        public WallPositionManager wallPositionManager;
        public TotalDoorManager totalDoorManager;

        public float animationSpeed;

        public float animationLength;
        private bool moving;
        private bool kicking;

        [SerializeField] private Situaition_Home_Intro eventManager;

        public Vector2Int playerPosition;

        private KeyCode curKeyCode;

        public OptionManager optionManager;



        public void Awake()
        {
            animationLength = childPlayerAnimator.runtimeAnimatorController.animationClips[0].length / animationSpeed;

            //playerPosition = new Vector2Int()
            //{
            //    x = (int)transform.position.x,
            //    y = (int)transform.position.y
            //};
        }
        public void Update()
        {
            if(eventManager.eventEnd && !optionManager.optionIsActive)
            {
                if (moving == false && kicking == false && Input.GetKeyDown(KeyCode.W))
                {
                    moving = true;
                    kicking = false;

                    if (!(wallPositionManager.WallPosition[playerPosition.y + 1 - wallPositionManager.Origin.y, playerPosition.x - wallPositionManager.Origin.x]))
                    {
                        moving = false;
                        kicking = true;
                    }

                    if (!kicking)
                    {
                        for (int index = 0; index < totalDoorManager.DoorPositions.Count; index++)
                        {
                            bool flag = false;

                            if (totalDoorManager.DoorSides[index] == Side.Left && (playerPosition.x == totalDoorManager.DoorPositions[index].x && playerPosition.y + 1 == totalDoorManager.DoorPositions[index].y))
                            {
                                flag = true;

                                totalDoorManager.IsToUp = true;
                            }
                            else if(totalDoorManager.DoorSides[index] == Side.Right && (playerPosition.x == totalDoorManager.DoorPositions[index].x && playerPosition.y + 1 == totalDoorManager.DoorPositions[index].y))
                            {
                                flag = true;

                                totalDoorManager.IsToUp = true;
                            }

                            if (flag)
                            {
                                totalDoorManager.ActiveDoorIndex = index;

                                moving = false;
                                kicking = true;

                                break;
                            }
                        }
                    }

                    if (moving)
                    {
                        playerPosition.y += 1;

                        childPlayerAnimator.SetBool("Go", true);
                        childPlayerAnimator.SetBool("Up", true);
                        //childPlayerAnimator.Play("GoUp");
                        Invoke("MoveUp", animationLength);
                    }
                    else if (kicking)
                    {
                        curKeyCode = KeyCode.W;

                        childPlayerAnimator.SetBool("Kick", true);
                        //childPlayerAnimator.SetBool("Up", true);
                        //childPlayerAnimator.Play("KickUp");
                        Invoke("KickCallBack", animationLength);
                    }
                }
                if (moving == false && kicking == false && Input.GetKeyDown(KeyCode.S))
                {
                    moving = true;
                    kicking = false;

                    if (!(wallPositionManager.WallPosition[playerPosition.y - 1 - wallPositionManager.Origin.y, playerPosition.x - wallPositionManager.Origin.x]))
                    {
                        moving = false;
                        kicking = true;
                    }

                    if (!kicking && totalDoorManager.DoorPositions.Count > 0)
                    {
                        for (int index = 0; index < totalDoorManager.DoorPositions.Count; index++)
                        {
                            bool flag = false;

                            if (totalDoorManager.DoorSides[index] == Side.Left && (playerPosition.x == totalDoorManager.DoorPositions[index].x && playerPosition.y - 1 == totalDoorManager.DoorPositions[index].y))
                            {
                                flag = true;

                                totalDoorManager.IsToUp = false;
                            }
                            else if(totalDoorManager.DoorSides[index] == Side.Right && (playerPosition.x == totalDoorManager.DoorPositions[index].x && playerPosition.y - 1 == totalDoorManager.DoorPositions[index].y))
                            {
                                flag = true;

                                totalDoorManager.IsToUp = false;
                            }

                            if (flag)
                            {
                                totalDoorManager.ActiveDoorIndex = index;

                                moving = false;
                                kicking = true;

                                break;
                            }
                        }
                    }

                    if (moving)
                    {
                        playerPosition.y -= 1;

                        childPlayerAnimator.SetBool("Go", true);
                        childPlayerAnimator.SetBool("Down", true);
                        //childPlayerAnimator.Play("GoDown");
                        Invoke("MoveDown", animationLength);
                    }
                    else if (kicking)
                    {
                        curKeyCode = KeyCode.S;

                        childPlayerAnimator.SetBool("Kick", true);
                        //childPlayerAnimator.SetBool("Up", true);
                        //childPlayerAnimator.Play("KickUp");
                        Invoke("KickCallBack", animationLength);
                    }
                }
                if (moving == false && kicking == false && Input.GetKeyDown(KeyCode.D))
                {
                    moving = true;
                    kicking = false;

                    if (!(wallPositionManager.WallPosition[playerPosition.y - wallPositionManager.Origin.y, playerPosition.x + 1 - wallPositionManager.Origin.x]))
                    {
                        moving = false;
                        kicking = true;
                    }

                    if (moving)
                    {
                        playerPosition.x += 1;

                        childPlayerAnimator.SetBool("Go", true);
                        childPlayerAnimator.SetBool("Right", true);

                        childPlayerAnimator.SetBool("ToRight", true);

                        //childPlayerAnimator.Play("GoRight");
                        Invoke("MoveRight", animationLength);
                    }
                    else if (kicking)
                    {
                        curKeyCode = KeyCode.D;
                         
                        childPlayerAnimator.SetBool("Kick", true);
                        //childPlayerAnimator.SetBool("Up", true);
                        //childPlayerAnimator.Play("KickUp");
                        Invoke("KickCallBack", animationLength);
                    }
                }
                if (moving == false && kicking == false && Input.GetKeyDown(KeyCode.A))
                {
                    moving = true;
                    kicking = false;

                    if (!(wallPositionManager.WallPosition[playerPosition.y - wallPositionManager.Origin.y, playerPosition.x - 1 - wallPositionManager.Origin.x]))
                    {
                        moving = false;
                        kicking = true;
                    }

                    if (moving)
                    {
                        playerPosition.x -= 1;

                        childPlayerAnimator.SetBool("Go", true);
                        childPlayerAnimator.SetBool("Left", true);

                        childPlayerAnimator.SetBool("ToRight", false);

                        //childPlayerAnimator.Play("GoLeft");
                        Invoke("MoveLeft", animationLength);
                    }
                    else if (kicking)
                    {
                        curKeyCode = KeyCode.A;

                        childPlayerAnimator.SetBool("Kick", true);
                        //childPlayerAnimator.SetBool("Up", true);
                        //childPlayerAnimator.Play("KickUp");
                        Invoke("KickCallBack", animationLength);
                    }
                }
            }
        }


        public bool Moving
        {
            get => moving;
        }
        public bool Kicking
        {
            get => kicking;
        }
        public WallPositionManager WallPositionManager
        {
            get => wallPositionManager;
            set => wallPositionManager = value;
        }
        public TotalDoorManager TotalDoorManager
        {
            get => totalDoorManager;
            set => totalDoorManager = value;
        }
        public Vector2Int PlayerPosition
        {
            get => playerPosition;
            set => playerPosition = value;
        }
        public KeyCode CurKeyCode
        {
            get => curKeyCode;
        }


        private void KickCallBack()
        {
            childPlayerAnimator.SetBool("Kick", false);

            curKeyCode = KeyCode.None;

            kicking = false;
        }

        private void MoveUp()
        {
            childPlayerAnimator.SetBool("Go", false);
            childPlayerAnimator.SetBool("Up", false);

            moving = false;

            //childPlayerAnimator.SetBool("GoUp", false);
        }
        private void MoveDown()
        {
            childPlayerAnimator.SetBool("Go", false);
            childPlayerAnimator.SetBool("Down", false);

            moving = false;

            //childPlayerAnimator.SetBool("GoDown", false);
        }
        private void MoveRight()
        {
            childPlayerAnimator.SetBool("Go", false);
            childPlayerAnimator.SetBool("Right", false);

            moving = false;

            //childPlayerAnimator.SetBool("GoRight", false);
        }
        private void MoveLeft()
        {
            childPlayerAnimator.SetBool("Go", false);
            childPlayerAnimator.SetBool("Left", false);

            moving = false;

            //childPlayerAnimator.SetBool("GoLeft", false);
        }
    }
}