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



        public void Awake()
        {
            stageNum.text = "1";

            playerStatus.moveChance = singleStageDatas[curStageIndex].maxMoveChance;

            playerMovement.stageData = singleStageDatas[curStageIndex];
        }
        public void FixedUpdate()
        {
            if(finish == false)
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
                        if (!statue.active)
                        {
                            flag = true;
                            break;
                        }
                    }

                    //  Go to next stage
                    if (!flag)
                    {
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


                    //  Move chance check
                    if (playerStatus.moveChance <= 0)
                    {
                        Restart();
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

            stageNum.text = (curStageIndex + 1).ToString();
        }
        private void GoLastStage()
        {
            exitScriptGameObject.SetActive(true);
            lastStageObject.SetActive(true);
        }

        public void Restart()
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