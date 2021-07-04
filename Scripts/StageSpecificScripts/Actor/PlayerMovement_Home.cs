using UnityEngine;

using Assets.Scripts.StageSpecificScripts.Manager;
using Assets.Scripts.SystemScripts.SituaitionScripts;



namespace NATAKER_DLL.StageSpecific.Home.Actor
{
    public sealed class PlayerMovement_Home : MonoBehaviour
    {
        public WallPositionManager WallPositionManager { get; set; }
        public TotalDoorManager TotalDoorManager { get; set; }

        [SerializeField] private Animator childPlayerAnimator;
        [SerializeField] private float animationSpeed;
        [SerializeField] private float animationLength;
        
        [SerializeField] private Situaition_Home_Intro eventManager;

        public Vector2Int PlayerPosition { get; set; }

        public bool Moving { get; private set; }
        public bool Kicking { get; private set; }
        public KeyCode CurKeyCode { get; private set; }

        [SerializeField] private OptionManager optionManager;

        [SerializeField] private AudioSource playerAudioSource;
        [SerializeField] private AudioClip kickaudioClip;
        [SerializeField] private AudioClip dashAudioClip;



        public void Awake()
        {
            animationLength = childPlayerAnimator.runtimeAnimatorController.animationClips[0].length / animationSpeed;

            var tempPosition = transform.position;
            PlayerPosition = new Vector2Int()
            {
                x = (int)tempPosition.x,
                y = (int)tempPosition.y
            };
        }
        public void Update()
        {
            if((eventManager == null ? true : eventManager.eventEnd) && !optionManager.OptionIsActive)
            {
                if (Moving == false && Kicking == false && Input.GetKeyDown(KeyCode.W))
                {
                    Moving = true;
                    Kicking = false;

                    if (!(WallPositionManager.WallPosition[PlayerPosition.y + 1 - WallPositionManager.Origin.y, PlayerPosition.x - WallPositionManager.Origin.x]))
                    {
                        Moving = false;
                        Kicking = true;
                    }

                    if (!Kicking)
                    {
                        for (int index = 0; index < TotalDoorManager.DoorPositions.Count; index++)
                        {
                            bool flag = false;

                            if (TotalDoorManager.DoorSides[index] == Side.Left && (PlayerPosition.x == TotalDoorManager.DoorPositions[index].x && PlayerPosition.y + 1 == TotalDoorManager.DoorPositions[index].y))
                            {
                                flag = true;

                                TotalDoorManager.IsToUp = true;
                            }
                            else if(TotalDoorManager.DoorSides[index] == Side.Right && (PlayerPosition.x == TotalDoorManager.DoorPositions[index].x && PlayerPosition.y + 1 == TotalDoorManager.DoorPositions[index].y))
                            {
                                flag = true;

                                TotalDoorManager.IsToUp = true;
                            }

                            if (flag)
                            {
                                TotalDoorManager.ActiveDoorIndex = index;

                                Moving = false;
                                Kicking = true;

                                break;
                            }
                        }
                    }

                    if (Moving)
                    {
                        CurKeyCode = KeyCode.W;

                        PlayerPosition = new Vector2Int()
                        {
                            x = PlayerPosition.x,
                            y = PlayerPosition.y + 1
                        };

                        childPlayerAnimator.SetBool("Go", true);
                        childPlayerAnimator.SetBool("Up", true);
                        //childPlayerAnimator.Play("GoUp");
                        Invoke("MoveUp", animationLength);
                    }
                    else if (Kicking)
                    {
                        CurKeyCode = KeyCode.W;

                        childPlayerAnimator.SetBool("Kick", true);
                        //childPlayerAnimator.SetBool("Up", true);
                        //childPlayerAnimator.Play("KickUp");
                        Invoke("KickCallBack", animationLength);
                    }
                }
                if (Moving == false && Kicking == false && Input.GetKeyDown(KeyCode.S))
                {
                    Moving = true;
                    Kicking = false;

                    if (!(WallPositionManager.WallPosition[PlayerPosition.y - 1 - WallPositionManager.Origin.y, PlayerPosition.x - WallPositionManager.Origin.x]))
                    {
                        Moving = false;
                        Kicking = true;
                    }

                    if (!Kicking && TotalDoorManager.DoorPositions.Count > 0)
                    {
                        for (int index = 0; index < TotalDoorManager.DoorPositions.Count; index++)
                        {
                            bool flag = false;

                            if (TotalDoorManager.DoorSides[index] == Side.Left && (PlayerPosition.x == TotalDoorManager.DoorPositions[index].x && PlayerPosition.y - 1 == TotalDoorManager.DoorPositions[index].y))
                            {
                                flag = true;

                                TotalDoorManager.IsToUp = false;
                            }
                            else if(TotalDoorManager.DoorSides[index] == Side.Right && (PlayerPosition.x == TotalDoorManager.DoorPositions[index].x && PlayerPosition.y - 1 == TotalDoorManager.DoorPositions[index].y))
                            {
                                flag = true;

                                TotalDoorManager.IsToUp = false;
                            }

                            if (flag)
                            {
                                TotalDoorManager.ActiveDoorIndex = index;

                                Moving = false;
                                Kicking = true;

                                break;
                            }
                        }
                    }

                    if (Moving)
                    {
                        CurKeyCode = KeyCode.S;

                        PlayerPosition = new Vector2Int()
                        {
                            x = PlayerPosition.x,
                            y = PlayerPosition.y - 1
                        };

                        childPlayerAnimator.SetBool("Go", true);
                        childPlayerAnimator.SetBool("Down", true);
                        //childPlayerAnimator.Play("GoDown");
                        Invoke("MoveDown", animationLength);
                    }
                    else if (Kicking)
                    {
                        CurKeyCode = KeyCode.S;

                        childPlayerAnimator.SetBool("Kick", true);
                        //childPlayerAnimator.SetBool("Up", true);
                        //childPlayerAnimator.Play("KickUp");
                        Invoke("KickCallBack", animationLength);
                    }
                }
                if (Moving == false && Kicking == false && Input.GetKeyDown(KeyCode.D))
                {
                    Moving = true;
                    Kicking = false;

                    if (!(WallPositionManager.WallPosition[PlayerPosition.y - WallPositionManager.Origin.y, PlayerPosition.x + 1 - WallPositionManager.Origin.x]))
                    {
                        Moving = false;
                        Kicking = true;
                    }

                    if (Moving)
                    {
                        CurKeyCode = KeyCode.D;

                        PlayerPosition = new Vector2Int()
                        {
                            x = PlayerPosition.x + 1,
                            y = PlayerPosition.y
                        };

                        childPlayerAnimator.SetBool("Go", true);
                        childPlayerAnimator.SetBool("Right", true);

                        childPlayerAnimator.SetBool("ToRight", true);

                        //childPlayerAnimator.Play("GoRight");
                        Invoke("MoveRight", animationLength);
                    }
                    else if (Kicking)
                    {
                        CurKeyCode = KeyCode.D;
                         
                        childPlayerAnimator.SetBool("Kick", true);

                        childPlayerAnimator.SetBool("ToRight", true);
                        //childPlayerAnimator.SetBool("Up", true);
                        //childPlayerAnimator.Play("KickUp");
                        Invoke("KickCallBack", animationLength);
                    }
                }
                if (Moving == false && Kicking == false && Input.GetKeyDown(KeyCode.A))
                {
                    Moving = true;
                    Kicking = false;

                    if (!(WallPositionManager.WallPosition[PlayerPosition.y - WallPositionManager.Origin.y, PlayerPosition.x - 1 - WallPositionManager.Origin.x]))
                    {
                        Moving = false;
                        Kicking = true;
                    }

                    if (Moving)
                    {
                        CurKeyCode = KeyCode.A;

                        PlayerPosition = new Vector2Int()
                        {
                            x = PlayerPosition.x - 1,
                            y = PlayerPosition.y
                        };

                        childPlayerAnimator.SetBool("Go", true);
                        childPlayerAnimator.SetBool("Left", true);

                        childPlayerAnimator.SetBool("ToRight", false);

                        //childPlayerAnimator.Play("GoLeft");
                        Invoke("MoveLeft", animationLength);
                    }
                    else if (Kicking)
                    {
                        CurKeyCode = KeyCode.A;

                        childPlayerAnimator.SetBool("Kick", true);

                        childPlayerAnimator.SetBool("ToRight", false);
                        //childPlayerAnimator.SetBool("Up", true);
                        //childPlayerAnimator.Play("KickUp");
                        Invoke("KickCallBack", animationLength);
                    }
                }

                if (Kicking)
                {
                    playerAudioSource.clip = kickaudioClip;
                    playerAudioSource.Play();
                }
                else if(Moving)
                {
                    playerAudioSource.clip = dashAudioClip;
                    playerAudioSource.Play();
                }
            }
        }

        private void CalculateMoveing(in KeyCode keyCode)
        {

        }

        private void KickCallBack()
        {
            childPlayerAnimator.SetBool("Kick", false);

            CurKeyCode = KeyCode.None;

            playerAudioSource.clip = null;

            Kicking = false;
        }

        private void MoveUp()
        {
            childPlayerAnimator.SetBool("Go", false);
            childPlayerAnimator.SetBool("Up", false);

            playerAudioSource.clip = null;

            Moving = false;

            CurKeyCode = KeyCode.None;

            //childPlayerAnimator.SetBool("GoUp", false);
        }
        private void MoveDown()
        {
            childPlayerAnimator.SetBool("Go", false);
            childPlayerAnimator.SetBool("Down", false);

            playerAudioSource.clip = null;

            Moving = false;

            CurKeyCode = KeyCode.None;

            //childPlayerAnimator.SetBool("GoDown", false);
        }
        private void MoveRight()
        {
            childPlayerAnimator.SetBool("Go", false);
            childPlayerAnimator.SetBool("Right", false);

            playerAudioSource.clip = null;

            Moving = false;

            CurKeyCode = KeyCode.None;

            //childPlayerAnimator.SetBool("GoRight", false);
        }
        private void MoveLeft()
        {
            childPlayerAnimator.SetBool("Go", false);
            childPlayerAnimator.SetBool("Left", false);

            playerAudioSource.clip = null;

            Moving = false;

            CurKeyCode = KeyCode.None;

            //childPlayerAnimator.SetBool("GoLeft", false);
        }
    }
}