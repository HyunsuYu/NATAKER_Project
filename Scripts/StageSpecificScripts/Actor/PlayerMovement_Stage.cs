using UnityEngine;

using Assets.Scripts.StageSpecificScripts.Manager;
using Assets.Scripts.StageSpecificScripts.Actor;
using Assets.Scripts.StageSpecificScripts.Data;
using Assets.Scripts.SystemScripts.SituaitionScripts;



namespace NATAKER_DLL.StageSpecific.Home.Actor
{
    public sealed class PlayerMovement_Stage : MonoBehaviour
    {
        public WallPositionManager WallPositionManager { get; set; }
        public RockPositionManager RockPositionManager { get; set; }
        public SingleStageData StageData { get; set; }

        [SerializeField] private PlayerStatus playerStatus;

        [SerializeField] private Animator childPlayerAnimator;
        [SerializeField] private float animationSpeed;
        [SerializeField] private float animationLength;

        public bool Moving { get; private set; }
        public bool Kicking { get; private set; }
        public Vector2Int PlayerPosition { get; set; }
        public KeyCode CurKeyCode { get; private set; }

        [SerializeField] private Situaition_MainStage situaition_MainStage;
        [SerializeField] private StageManager stageManager;

        [SerializeField] private SingleTubeManager[] singleTubeManagers;

        [SerializeField] private OptionManager optionManager;

        [SerializeField] private AudioSource playerAudioSource;
        [SerializeField] private AudioClip kickAudioClip;
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
            bool tempFlag = true;
            if(singleTubeManagers.Length >= 1)
            {
                foreach (var tube in singleTubeManagers)
                {
                    if (tube.Arrive == false && tube.Enter)
                    {
                        tempFlag = false;

                        break;
                    }
                }
            }
            

            if(tempFlag && stageManager.IsDie == false && situaition_MainStage.eventEnd && !optionManager.OptionIsActive)
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
                        foreach (Vector2Int coord in RockPositionManager.RockPositions)
                        {
                            if (PlayerPosition.x == coord.x && PlayerPosition.y + 1 == coord.y)
                            {
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

                        playerStatus.MoveChance--;
                        playerStatus.UpdateMoveChance();

                        childPlayerAnimator.SetBool("Go", true);
                        childPlayerAnimator.SetBool("Up", true);
                        //childPlayerAnimator.Play("GoUp");
                        Invoke("MoveUp", animationLength);
                    }
                    else if (Kicking)
                    {
                        CurKeyCode = KeyCode.W;

                        playerStatus.MoveChance--;
                        playerStatus.UpdateMoveChance();

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

                    if (!Kicking)
                    {
                        foreach (Vector2Int coord in RockPositionManager.RockPositions)
                        {
                            if (PlayerPosition.x == coord.x && PlayerPosition.y - 1 == coord.y)
                            {
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

                        playerStatus.MoveChance--;
                        playerStatus.UpdateMoveChance();

                        childPlayerAnimator.SetBool("Go", true);
                        childPlayerAnimator.SetBool("Down", true);
                        //childPlayerAnimator.Play("GoDown");
                        Invoke("MoveDown", animationLength);
                    }
                    else if (Kicking)
                    {
                        CurKeyCode = KeyCode.S;

                        playerStatus.MoveChance--;
                        playerStatus.UpdateMoveChance();

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

                    if (!Kicking)
                    {
                        foreach (Vector2Int coord in RockPositionManager.RockPositions)
                        {
                            if (PlayerPosition.x + 1 == coord.x && PlayerPosition.y == coord.y)
                            {
                                Moving = false;
                                Kicking = true;
                                break;
                            }
                        }
                    }

                    if (Moving)
                    {
                        CurKeyCode = KeyCode.D;

                        PlayerPosition = new Vector2Int()
                        {
                            x = PlayerPosition.x + 1,
                            y = PlayerPosition.y
                        };

                        playerStatus.MoveChance--;
                        playerStatus.UpdateMoveChance();

                        childPlayerAnimator.SetBool("Go", true);
                        childPlayerAnimator.SetBool("Right", true);

                        childPlayerAnimator.SetBool("ToRight", true);

                        //childPlayerAnimator.Play("GoRight");
                        Invoke("MoveRight", animationLength);
                    }
                    else if (Kicking)
                    {
                        CurKeyCode = KeyCode.D;

                        playerStatus.MoveChance--;
                        playerStatus.UpdateMoveChance();

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

                    if (!Kicking)
                    {
                        foreach (Vector2Int coord in RockPositionManager.RockPositions)
                        {
                            if (PlayerPosition.x - 1 == coord.x && PlayerPosition.y == coord.y)
                            {
                                Moving = false;
                                Kicking = true;
                                break;
                            }
                        }
                    }

                    if (Moving)
                    {
                        CurKeyCode = KeyCode.A;

                        PlayerPosition = new Vector2Int()
                        {
                            x = PlayerPosition.x - 1,
                            y = PlayerPosition.y
                        };

                        playerStatus.MoveChance--;
                        playerStatus.UpdateMoveChance();

                        childPlayerAnimator.SetBool("Go", true);
                        childPlayerAnimator.SetBool("Left", true);

                        childPlayerAnimator.SetBool("ToRight", false);

                        //childPlayerAnimator.Play("GoLeft");
                        Invoke("MoveLeft", animationLength);
                    }
                    else if (Kicking)
                    {
                        CurKeyCode = KeyCode.A;

                        playerStatus.MoveChance--;
                        playerStatus.UpdateMoveChance();

                        childPlayerAnimator.SetBool("Kick", true);

                        childPlayerAnimator.SetBool("ToRight", false);

                        //childPlayerAnimator.SetBool("Up", true);
                        //childPlayerAnimator.Play("KickUp");
                        Invoke("KickCallBack", animationLength);
                    }
                }

                if (Kicking)
                {
                    playerAudioSource.clip = kickAudioClip;
                    playerAudioSource.Play();
                }
                else if (Moving)
                {
                    playerAudioSource.clip = dashAudioClip;
                    playerAudioSource.Play();
                }
            }
        }

        public SingleTubeManager[] SingleTubeManagers
        {
            set => singleTubeManagers = value;
        }

        public void KickCallBack()
        {
            CurKeyCode = KeyCode.None;

            childPlayerAnimator.SetBool("Kick", false);

            playerAudioSource.clip = null;

            Kicking = false;
        }

        private void MoveUp()
        {
            CurKeyCode = KeyCode.None;

            childPlayerAnimator.SetBool("Go", false);
            childPlayerAnimator.SetBool("Up", false);

            playerAudioSource.clip = null;

            Moving = false;

            //childPlayerAnimator.SetBool("GoUp", false);
        }
        private void MoveDown()
        {
            CurKeyCode = KeyCode.None;

            childPlayerAnimator.SetBool("Go", false);
            childPlayerAnimator.SetBool("Down", false);

            playerAudioSource.clip = null;

            Moving = false;

            //childPlayerAnimator.SetBool("GoDown", false);
        }
        private void MoveRight()
        {
            CurKeyCode = KeyCode.None;

            childPlayerAnimator.SetBool("Go", false);
            childPlayerAnimator.SetBool("Right", false);

            playerAudioSource.clip = null;

            Moving = false;

            //childPlayerAnimator.SetBool("GoRight", false);
        }
        private void MoveLeft()
        {
            CurKeyCode = KeyCode.None;

            childPlayerAnimator.SetBool("Go", false);
            childPlayerAnimator.SetBool("Left", false);

            playerAudioSource.clip = null;

            Moving = false;

            //childPlayerAnimator.SetBool("GoLeft", false);
        }
    }
}