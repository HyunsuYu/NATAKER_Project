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
        [SerializeField] private Image backGroundImage;

        [SerializeField] private PlayerMovement_Stage playerMovement;
        [SerializeField] private Transform playerTranstorm;

        [SerializeField] private StageObjectsPositionResetManager[] stageObjectsPositionResetManagers;

        [SerializeField] private SingleStageData[] singleStageDatas;

        [SerializeField] private PlayerStatus playerStatus;

        [SerializeField] private GameObject cloud;
        [SerializeField] private RectTransform cloudRectTransform;
        [SerializeField] private float cloudSpeed;
        [SerializeField] private float stageChangeInvokeTime;
        [SerializeField] private Vector2 basePosition;
        [SerializeField] private Vector2 targetPosition;
        private bool cloudMove;

        [SerializeField] private int curStageIndex;

        [SerializeField] private Situaition_MainStage_Exit situaition_MainStage_Exit;
        [SerializeField] private GameObject exitScriptGameObject;
        [SerializeField] private GameObject lastStageObject;

        [SerializeField] private Text stageNum;

        private bool finish, tempFlag;
        private bool stageisChanging;

        public bool die;
        private bool cloudAppear;



        public void Awake()
        {
            stageNum.text = "1";

            playerStatus.moveChance = singleStageDatas[curStageIndex].maxMoveChance;

            playerMovement.stageData = singleStageDatas[curStageIndex];
        }
        public void Update()
        {
            if (finish == false && die == false)
            {
                if (cloudMove)
                {
                    cloudRectTransform.anchoredPosition = Vector3.MoveTowards(cloudRectTransform.anchoredPosition, targetPosition, Time.deltaTime * cloudSpeed);

                    if (Vector2.Distance(cloudRectTransform.anchoredPosition, targetPosition) <= 10.0f)
                    {
                        cloud.SetActive(false);
                        cloudMove = false;

                        cloudRectTransform.anchoredPosition = basePosition;

                        stageisChanging = false;
                    }
                }
                else
                {
                    //  Clear Check
                    bool flag = false;
                    foreach (var statue in singleStageDatas[curStageIndex].statueManagers)
                    {
                        if (!statue.active)
                        {
                            flag = true;
                            break;
                        }
                    }

                    //  Go to next stage
                    if (!flag)
                    {
                        stageisChanging = true;

                        cloud.SetActive(true);
                        cloudMove = true;

                        //  Implement stage speecific calculations

                        curStageIndex++;

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
            else if(finish && tempFlag == false)
            {
                cloudRectTransform.anchoredPosition = Vector3.MoveTowards(cloudRectTransform.anchoredPosition, targetPosition, Time.deltaTime * cloudSpeed);

                if (Vector2.Distance(cloudRectTransform.anchoredPosition, targetPosition) <= 10.0f)
                {
                    cloud.SetActive(false);
                    cloudMove = false;

                    cloudRectTransform.anchoredPosition = basePosition;

                    tempFlag = true;
                }
            }

            if (playerStatus.moveChance <= 0 && cloudMove == false)
            {
                Invoke("Temp", 1.0f);
            }

            if (die)
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
                        die = false;
                    }
                }
                
            }
        }

        private void GoNextStage()
        {
            playerMovement.playerPosition = singleStageDatas[curStageIndex].startPosition;
            playerMovement.playerPosition.x -= singleStageDatas[curStageIndex].offset.x;
            playerMovement.playerPosition.y -= singleStageDatas[curStageIndex].offset.y;

            playerTranstorm.position = singleStageDatas[curStageIndex].StartPosition_Real;

            playerMovement.rockPositionManager = singleStageDatas[curStageIndex].rockPositionManager;
            playerMovement.wallPositionManager = singleStageDatas[curStageIndex].wallPositionManager;

            playerStatus.moveChance = singleStageDatas[curStageIndex].maxMoveChance;
            playerStatus.UpdateMoveChance();

            playerMovement.stageData = singleStageDatas[curStageIndex];

            playerMovement.singleTubeManagers = singleStageDatas[curStageIndex].singleTubeManagers;

            stageNum.text = (curStageIndex + 1).ToString();

            foreach (var temp in singleStageDatas[curStageIndex].statueManagers)
            {
                temp.active = false;
                temp.light2D.enabled = false;
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
            die = true;

            Invoke("ResetAll", 0.5f);   
        }
        private void ResetAll()
        {
            // Restart
            playerMovement.playerPosition = singleStageDatas[curStageIndex].startPosition;
            playerMovement.playerPosition.x -= singleStageDatas[curStageIndex].offset.x;
            playerMovement.playerPosition.y -= singleStageDatas[curStageIndex].offset.y;

            playerTranstorm.position = singleStageDatas[curStageIndex].StartPosition_Real;

            playerStatus.moveChance = singleStageDatas[curStageIndex].maxMoveChance;
            playerStatus.UpdateMoveChance();

            // reset the objects positions
            stageObjectsPositionResetManagers[curStageIndex].ResetObjectPositions();
        }
    }
}