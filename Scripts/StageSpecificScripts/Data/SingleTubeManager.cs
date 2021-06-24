using UnityEngine;

using NATAKER_DLL.StageSpecific.Home.Actor;
using Assets.Scripts.StageSpecificScripts.Manager;
using Assets.Scripts.StageSpecificScripts.Data;



namespace Assets.Scripts.StageSpecificScripts.Data
{
    public sealed class SingleTubeManager : MonoBehaviour
    {
        [SerializeField] private Vector3[] wayPointPositions;
        [SerializeField] private TubeEnterPoint[] tubeEnterPoints;

        [SerializeField] private PlayerMovement_Stage playerMovement;
        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private SpriteRenderer playerSpriteRenderer;
        [SerializeField] private Transform playerTransform;
        private TubeEnterPosition tubeEnterPosition;

        [SerializeField] private RockPositionManager rockPositionManager;

        [SerializeField] private float speed;
        private int curwayPointIndex;
        [SerializeField] private bool enter;
        [SerializeField] private bool arrive;

        [SerializeField] private SingleStageData stageData;


        public void FixedUpdate()
        {
            if (arrive == false && tubeEnterPoints[0].active && !tubeEnterPoints[1].active)
            {
                if (!enter)
                {
                    curwayPointIndex = 0;
                    tubeEnterPosition = TubeEnterPosition.Up;

                    boxCollider.enabled = false;
                    playerSpriteRenderer.enabled = false;
                }
                enter = true;

                float step = Time.deltaTime * speed;

                playerTransform.position = Vector3.MoveTowards(playerTransform.position, wayPointPositions[curwayPointIndex], step);

                if (curwayPointIndex == wayPointPositions.Length - 1 && Vector3.Distance(wayPointPositions[curwayPointIndex], playerTransform.position) <= 0.2f)
                {

                    playerTransform.position = wayPointPositions[curwayPointIndex];

                    curwayPointIndex++;

                    arrive = true;
                }
                else if (Vector3.Distance(wayPointPositions[curwayPointIndex], playerTransform.position) <= 0.2f)
                {
                    playerTransform.position = wayPointPositions[curwayPointIndex];

                    curwayPointIndex++;
                }
            }
            else if (arrive == false && !tubeEnterPoints[0].active && tubeEnterPoints[1].active)
            {
                if (!enter)
                {
                    curwayPointIndex = wayPointPositions.Length - 1;
                    tubeEnterPosition = TubeEnterPosition.Down;

                    boxCollider.enabled = false;
                    playerSpriteRenderer.enabled = false;
                }
                enter = true;

                float step = Time.deltaTime * speed;

                playerTransform.position = Vector3.MoveTowards(playerTransform.position, wayPointPositions[curwayPointIndex], step);

                if (curwayPointIndex == 0 && Vector3.Distance(wayPointPositions[curwayPointIndex], playerTransform.position) <= 0.2f)
                {
                    playerTransform.position = wayPointPositions[curwayPointIndex];

                    curwayPointIndex--;

                    arrive = true;
                }
                else if (Vector3.Distance(wayPointPositions[curwayPointIndex], playerTransform.position) <= 0.2f)
                {
                    playerTransform.position = wayPointPositions[curwayPointIndex];

                    curwayPointIndex--;
                }
            }

            if (arrive)
            {
                enter = false;
                arrive = false;

                tubeEnterPoints[0].active = false;
                tubeEnterPoints[1].active = false;


                bool flag = default(bool);
                int tempIndex = default(int);

                Vector2Int tempPosition = default(Vector2Int);

                switch (tubeEnterPosition)
                {
                    case TubeEnterPosition.Up:
                        flag = false;
                        tempIndex = wayPointPositions.Length - 1;

                        tempPosition = new Vector2Int()
                        {
                            x = (int)wayPointPositions[tempIndex].x - stageData.offset.x,
                            y = (int)wayPointPositions[tempIndex].y - stageData.offset.y
                        };
                        foreach (Vector2Int rock in rockPositionManager.RockPositions)
                        {
                            if (tempPosition == rock)
                            {
                                flag = true;
                                break;
                            }
                        }

                        if(flag)
                        {
                            tubeEnterPoints[1].active = true;
                        }
                        else
                        {
                            playerMovement.playerPosition = tempPosition;
                        }
                        break;

                    case TubeEnterPosition.Down:
                        flag = false;
                        tempIndex = 0;

                        tempPosition = new Vector2Int()
                        {
                            x = (int)wayPointPositions[tempIndex].x - stageData.offset.x,
                            y = (int)wayPointPositions[tempIndex].y - stageData.offset.y
                        };
                        foreach (Vector2Int rock in rockPositionManager.RockPositions)
                        {
                            if (tempPosition == rock)
                            {
                                flag = true;
                                break;
                            }
                        }

                        if (flag)
                        {
                            tubeEnterPoints[0].active = true;
                        }
                        else
                        {
                            playerMovement.playerPosition = tempPosition;
                        }
                        break;
                }
                tubeEnterPosition = TubeEnterPosition.None;

                if(!flag)
                {
                    boxCollider.enabled = true;
                    playerSpriteRenderer.enabled = true;
                }
            }
        }
    }
}