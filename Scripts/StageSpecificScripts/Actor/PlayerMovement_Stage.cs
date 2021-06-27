using UnityEngine;

using Assets.Scripts.StageSpecificScripts.Manager;
using Assets.Scripts.StageSpecificScripts.Actor;
using Assets.Scripts.StageSpecificScripts.Data;
using Assets.Scripts.SystemScripts.SituaitionScripts;



namespace NATAKER_DLL.StageSpecific.Home.Actor
{
    public sealed class PlayerMovement_Stage : MonoBehaviour
    {
        [SerializeField] public SingleTubeManager[] singleTubeManagers;

        public OptionManager optionManager;

        public Animator childPlayerAnimator;

        public WallPositionManager wallPositionManager;
        public RockPositionManager rockPositionManager;

        public PlayerStatus playerStatus;
        public SingleStageData stageData;

        public float animationSpeed;

        public float animationLength;
        [SerializeField] private bool moving;
        [SerializeField] private bool kicking;

        public Vector2Int playerPosition;

        public KeyCode CurKeyCode;

        [SerializeField] private Situaition_MainStage situaition_MainStage;
        [SerializeField] private StageManager stageManager;

        [SerializeField] private AudioSource playerAudioSource;
        [SerializeField] private AudioClip kickAudioClip;
        [SerializeField] private AudioClip dashAudioClip;



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
            bool tempFlag = true;
            if(singleTubeManagers.Length >= 1)
            {
                foreach (var tube in singleTubeManagers)
                {
                    if (tube.arrive == false && tube.enter)
                    {
                        tempFlag = false;

                        break;
                    }
                }
            }
            

            if(tempFlag && stageManager.die == false && situaition_MainStage.eventEnd && !optionManager.optionIsActive)
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
                        foreach (Vector2Int coord in rockPositionManager.RockPositions)
                        {
                            if (playerPosition.x == coord.x && playerPosition.y + 1 == coord.y)
                            {
                                moving = false;
                                kicking = true;
                                break;
                            }
                        }
                    }

                    if (moving)
                    {
                        playerPosition.y += 1;

                        playerStatus.moveChance--;
                        playerStatus.UpdateMoveChance();

                        childPlayerAnimator.SetBool("Go", true);
                        childPlayerAnimator.SetBool("Up", true);
                        //childPlayerAnimator.Play("GoUp");
                        Invoke("MoveUp", animationLength);
                    }
                    else if (kicking)
                    {
                        CurKeyCode = KeyCode.W;

                        playerStatus.moveChance--;
                        playerStatus.UpdateMoveChance();

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

                    if (!kicking)
                    {
                        foreach (Vector2Int coord in rockPositionManager.RockPositions)
                        {
                            if (playerPosition.x == coord.x && playerPosition.y - 1 == coord.y)
                            {
                                moving = false;
                                kicking = true;
                                break;
                            }
                        }
                    }

                    if (moving)
                    {
                        playerPosition.y -= 1;

                        playerStatus.moveChance--;
                        playerStatus.UpdateMoveChance();

                        childPlayerAnimator.SetBool("Go", true);
                        childPlayerAnimator.SetBool("Down", true);
                        //childPlayerAnimator.Play("GoDown");
                        Invoke("MoveDown", animationLength);
                    }
                    else if (kicking)
                    {
                        CurKeyCode = KeyCode.S;

                        playerStatus.moveChance--;
                        playerStatus.UpdateMoveChance();

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

                    if (!kicking)
                    {
                        foreach (Vector2Int coord in rockPositionManager.RockPositions)
                        {
                            if (playerPosition.x + 1 == coord.x && playerPosition.y == coord.y)
                            {
                                moving = false;
                                kicking = true;
                                break;
                            }
                        }
                    }

                    if (moving)
                    {
                        playerPosition.x += 1;

                        playerStatus.moveChance--;
                        playerStatus.UpdateMoveChance();

                        childPlayerAnimator.SetBool("Go", true);
                        childPlayerAnimator.SetBool("Right", true);

                        childPlayerAnimator.SetBool("ToRight", true);

                        //childPlayerAnimator.Play("GoRight");
                        Invoke("MoveRight", animationLength);
                    }
                    else if (kicking)
                    {
                        CurKeyCode = KeyCode.D;

                        playerStatus.moveChance--;
                        playerStatus.UpdateMoveChance();

                        childPlayerAnimator.SetBool("Kick", true);

                        childPlayerAnimator.SetBool("ToRight", true);

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

                    if (!kicking)
                    {
                        foreach (Vector2Int coord in rockPositionManager.RockPositions)
                        {
                            if (playerPosition.x - 1 == coord.x && playerPosition.y == coord.y)
                            {
                                moving = false;
                                kicking = true;
                                break;
                            }
                        }
                    }

                    if (moving)
                    {
                        playerPosition.x -= 1;

                        playerStatus.moveChance--;
                        playerStatus.UpdateMoveChance();

                        childPlayerAnimator.SetBool("Go", true);
                        childPlayerAnimator.SetBool("Left", true);

                        childPlayerAnimator.SetBool("ToRight", false);

                        //childPlayerAnimator.Play("GoLeft");
                        Invoke("MoveLeft", animationLength);
                    }
                    else if (kicking)
                    {
                        CurKeyCode = KeyCode.A;

                        playerStatus.moveChance--;
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
        public Vector2Int PlayerPosition
        {
            get => playerPosition;
            set => playerPosition = value;
        }


        private void KickCallBack()
        {
            childPlayerAnimator.SetBool("Kick", false);

            CurKeyCode = KeyCode.None;

            playerAudioSource.clip = null;

            kicking = false;
        }

        private void MoveUp()
        {
            childPlayerAnimator.SetBool("Go", false);
            childPlayerAnimator.SetBool("Up", false);

            playerAudioSource.clip = null;

            moving = false;

            //childPlayerAnimator.SetBool("GoUp", false);
        }
        private void MoveDown()
        {
            childPlayerAnimator.SetBool("Go", false);
            childPlayerAnimator.SetBool("Down", false);

            playerAudioSource.clip = null;

            moving = false;

            //childPlayerAnimator.SetBool("GoDown", false);
        }
        private void MoveRight()
        {
            childPlayerAnimator.SetBool("Go", false);
            childPlayerAnimator.SetBool("Right", false);

            playerAudioSource.clip = null;

            moving = false;

            //childPlayerAnimator.SetBool("GoRight", false);
        }
        private void MoveLeft()
        {
            childPlayerAnimator.SetBool("Go", false);
            childPlayerAnimator.SetBool("Left", false);

            playerAudioSource.clip = null;

            moving = false;

            //childPlayerAnimator.SetBool("GoLeft", false);
        }
    }
}