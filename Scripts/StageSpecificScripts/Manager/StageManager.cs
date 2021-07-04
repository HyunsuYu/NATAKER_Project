using UnityEngine;
using UnityEngine.UI;

using NATAKER_DLL.StageSpecific.Home.Actor;
using Assets.Scripts.StageSpecificScripts.Actor;
using Assets.Scripts.StageSpecificScripts.Data;
using Assets.Scripts.SystemScripts.SituaitionScripts;



namespace Assets.Scripts.StageSpecificScripts.Manager
{
    public sealed class StageManager : MonoBehaviour
    {
        [SerializeField] private PlayerMovement_Stage playerMovement;
        [SerializeField] private Transform playerTranstorm;

        [SerializeField] private PlayerStatus playerStatus;

        [SerializeField] private StageObjectsPositionResetManager[] stageObjectsPositionResetManagers;
        [SerializeField] private SingleStageData[] singleStageDatas;

        private int curStageIndex;

        [SerializeField] private GameObject cloud;
        [SerializeField] private RectTransform cloudRectTransform;
        [SerializeField] private float cloudSpeed;
        [SerializeField] private float stageChangeInvokeTime;
        [SerializeField] private Vector2 basePosition;
        [SerializeField] private Vector2 targetPosition;
        private bool cloudMove;

        [SerializeField] private Situaition_MainStage_Exit situaition_MainStage_Exit;
        [SerializeField] private GameObject exitScriptGameObject;
        [SerializeField] private GameObject lastStageObject;

        [SerializeField] private Image backGroundImage;

        [SerializeField] private Text stageNum;

        private bool finish;

        public bool IsDie { get; private set; }

        private bool cloudAppear;



        public void Awake()
        {
            stageNum.text = "1";

            //playerStatus.MoveChance = singleStageDatas[curStageIndex].maxMoveChance;
            //playerMovement.StageData = singleStageDatas[curStageIndex];
            //playerMovement.WallPositionManager = singleStageDatas[curStageIndex].wallPositionManager;
            //playerMovement.RockPositionManager = singleStageDatas[curStageIndex].rockPositionManager;
        }
        public void Start()
        {
            GoNextStage();
        }
        public void Update()
        {
            if (finish == false && IsDie == false)
            {
                if (cloudMove)
                {
                    cloudRectTransform.anchoredPosition = Vector3.MoveTowards(cloudRectTransform.anchoredPosition, targetPosition, Time.deltaTime * cloudSpeed);

                    if (Vector2.Distance(cloudRectTransform.anchoredPosition, targetPosition) <= 10.0f)
                    {
                        cloud.SetActive(false);
                        cloudMove = false;

                        cloudRectTransform.anchoredPosition = basePosition;
                    }
                }
                else
                {
                    //  Clear Check
                    bool flag = false;
                    foreach (var statue in singleStageDatas[curStageIndex].statueManagers)
                    {
                        if (!statue.Active)
                        {
                            flag = true;
                            
                            break;
                        }
                    }

                    //  Go to next stage
                    if (!flag)
                    {
                        cloud.SetActive(true);
                        
                        curStageIndex++;
                        cloudMove = true;

                        Invoke("GoNextStage", stageChangeInvokeTime);

                        if (curStageIndex == singleStageDatas.Length)
                        {
                            finish = true;

                            CancelInvoke("GoNextStage");

                            Invoke("GoLastStage", stageChangeInvokeTime);

                            //situaition_MainStage_Exit.Awake();
                        }
                    }
                }
            }
            else if(finish)
            {
                cloudRectTransform.anchoredPosition = Vector3.MoveTowards(cloudRectTransform.anchoredPosition, targetPosition, Time.deltaTime * cloudSpeed);

                if (Vector2.Distance(cloudRectTransform.anchoredPosition, targetPosition) <= 10.0f)
                {
                    cloud.SetActive(false);
                    cloudMove = false;

                    cloudRectTransform.anchoredPosition = basePosition;
                }
            }

            if (playerStatus.MoveChance <= 0 && cloudMove == false)
            {
                Invoke("Temp", 1.0f);
            }

            if (IsDie && !cloudMove)
            {
                if(cloudAppear == false)
                {
                    float tempA = backGroundImage.color.a;
                    backGroundImage.color = new Color()
                    {
                        r = 1.0f,
                        g = 1.0f,
                        b = 1.0f,
                        a = tempA + Time.deltaTime * 2.0f
                    };

                    if (backGroundImage.color.a >= 0.95f)
                    {
                        backGroundImage.color = new Color()
                        {
                            r = 1.0f,
                            g = 1.0f,
                            b = 1.0f,
                            a = 1.0f
                        };

                        cloudAppear = true;
                    }
                }
                else
                {
                    float tempA = backGroundImage.color.a;
                    backGroundImage.color = new Color()
                    {
                        r = 1.0f,
                        g = 1.0f,
                        b = 1.0f,
                        a = tempA - Time.deltaTime / 3.0f
                    };

                    if (backGroundImage.color.a <= 0.05f)
                    {
                        backGroundImage.color = new Color()
                        {
                            r = 1.0f,
                            g = 1.0f,
                            b = 1.0f,
                            a = 0.0f
                        };

                        cloudAppear = false;
                        IsDie = false;
                    }
                }
            }
            else
            {
                IsDie = false;
            }
        }

        private void GoNextStage()
        {
            playerMovement.PlayerPosition = singleStageDatas[curStageIndex].startPosition;
            playerMovement.PlayerPosition = new Vector2Int()
            {
                x = playerMovement.PlayerPosition.x - singleStageDatas[curStageIndex].offset.x,
                y = playerMovement.PlayerPosition.y - singleStageDatas[curStageIndex].offset.y
            };

            playerTranstorm.position = singleStageDatas[curStageIndex].StartPosition_Real;

            playerMovement.RockPositionManager = singleStageDatas[curStageIndex].rockPositionManager;
            playerMovement.WallPositionManager = singleStageDatas[curStageIndex].wallPositionManager;

            playerStatus.MoveChance = singleStageDatas[curStageIndex].maxMoveChance;
            playerStatus.UpdateMoveChance();

            playerMovement.StageData = singleStageDatas[curStageIndex];

            playerMovement.SingleTubeManagers = singleStageDatas[curStageIndex].singleTubeManagers;

            stageNum.text = (curStageIndex + 1).ToString();

            foreach (var temp in singleStageDatas[curStageIndex].statueManagers)
            {
                temp.Active = false;
                temp.Light2D.enabled = false;
            }
        }
        private void GoLastStage()
        {
            exitScriptGameObject.SetActive(true);
            lastStageObject.SetActive(true);
        }
        private void Temp()
        {
            Restart();
        }

        public void Restart()
        {
            IsDie = true;

            Invoke("ResetAll", 0.5f);   
        }
        private void ResetAll()
        {
            // Restart
            playerMovement.PlayerPosition = singleStageDatas[curStageIndex].startPosition;
            playerMovement.PlayerPosition = new Vector2Int()
            {
                x = playerMovement.PlayerPosition.x - singleStageDatas[curStageIndex].offset.x,
                y = playerMovement.PlayerPosition.y - singleStageDatas[curStageIndex].offset.y
            };

            playerTranstorm.position = singleStageDatas[curStageIndex].StartPosition_Real;

            playerStatus.MoveChance = singleStageDatas[curStageIndex].maxMoveChance;
            playerStatus.UpdateMoveChance();

            // reset the objects positions
            stageObjectsPositionResetManagers[curStageIndex].ResetObjectPositions();
        }
    }
}