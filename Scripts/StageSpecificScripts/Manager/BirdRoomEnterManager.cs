using UnityEngine;

using NATAKER_DLL.StageSpecific.Home.Actor;



namespace Assets.Scripts.StageSpecificScripts.Manager
{
    public sealed class BirdRoomEnterManager : MonoBehaviour
    {
        [SerializeField] private PlayerMovement_Home playerMovement;

        [SerializeField] private Vector2Int basePosition;
        [SerializeField] private Vector2Int wallPosition;

        [SerializeField] private SpriteRenderer[] renderers;
        [SerializeField] private Vector2Int[] password;

        [SerializeField] private WallPositionManager wallPositionManager;

        [SerializeField] private int curIndex;

        private Vector2Int prevPosition;

        public bool Flag { get; private set; }
        public bool CheckStart { get; private set; }



        public void Update()
        {
            if(Flag == false)
            {
                if(curIndex == 0 && playerMovement.PlayerPosition == basePosition)
                {
                    CheckStart = true;
                }

                if(CheckStart)
                {
                    if (playerMovement.Kicking)
                    {
                        curIndex = 0;
                        CheckStart = false;
                    }
                    else if (prevPosition != playerMovement.PlayerPosition && curIndex < password.Length)
                    {
                        if (playerMovement.PlayerPosition == password[curIndex])
                        {
                            curIndex++;
                        }
                        else
                        {
                            curIndex = 0;
                            CheckStart = false;
                        }

                        prevPosition = playerMovement.PlayerPosition;
                    }
                }
            }

            if(curIndex == password.Length)
            {
                Flag = true;

                wallPositionManager.WallPosition[wallPosition.y - wallPositionManager.Origin.y, wallPosition.x - wallPositionManager.Origin.x] = true;
            }

            if(Flag)
            {
                float tempA = renderers[0].color.a;

                foreach(var renderer in renderers)
                {
                    renderer.color = new Color()
                    {
                        r = renderer.color.r,
                        g = renderer.color.g,
                        b = renderer.color.b,
                        a = tempA - Time.deltaTime
                    };
                }

                if (renderers[0].color.a <= 0.05f)
                {
                    foreach (var renderer in renderers)
                    {
                        renderer.color = new Color()
                        {
                            r = renderer.color.r,
                            g = renderer.color.g,
                            b = renderer.color.b,
                            a = 0.0f
                        };
                    }
                }
            }
        }
    }
}