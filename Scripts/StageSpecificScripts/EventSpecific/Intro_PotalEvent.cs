using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using NATAKER_DLL.StageSpecific.Home.Actor;



namespace Assets.Scripts.StageSpecificScripts.EventSpecific
{
    public sealed class Intro_PotalEvent : MonoBehaviour
    {
        [SerializeField] private PlayerMovement_Home playerMovement;
        [SerializeField] private Vector2Int potalPosition;

        [SerializeField] private Image backGroundImage;
        [SerializeField] private Animator backGroundAnimator;

        [SerializeField] private Image textBoxBar;

        [SerializeField] private Text text_title;

        [SerializeField] private GameObject[] buttons;

        private bool eventEnter;



        public void FixedUpdate()
        {
            if(eventEnter)
            {
                float tempA = backGroundImage.color.a;
                backGroundImage.color = new Color()
                {
                    r = 1.0f,
                    g = 1.0f,
                    b = 1.0f,
                    a = tempA + Time.deltaTime
                };

                if (backGroundImage.color.a >= 9.50f)
                {
                    backGroundImage.color = new Color()
                    {
                        r = 1.0f,
                        g = 1.0f,
                        b = 1.0f,
                        a = 1.0f
                    };

                    SceneManager.LoadScene(1);
                }
            }
        }
        public void OnTriggerStay2D(Collider2D other)
        {
            if(other.tag == "Player" && eventEnter == false)
            {
                if (playerMovement.Kicking)
                {
                    bool flag = false;

                    switch (playerMovement.CurKeyCode)
                    {
                        case KeyCode.W:
                            if(potalPosition.x == playerMovement.PlayerPosition.x && potalPosition.y == playerMovement.PlayerPosition.y + 1)
                            {
                                flag = true;
                            }
                            break;

                        case KeyCode.S:
                            if (potalPosition.x == playerMovement.PlayerPosition.x && potalPosition.y == playerMovement.PlayerPosition.y - 1)
                            {
                                flag = true;
                            }
                            break;

                        case KeyCode.A:
                            if (potalPosition.x == playerMovement.PlayerPosition.x - 1 && potalPosition.y == playerMovement.PlayerPosition.y)
                            {
                                flag = true;
                            }
                            break;

                        case KeyCode.D:
                            if (potalPosition.x == playerMovement.PlayerPosition.x + 1 && potalPosition.y == playerMovement.PlayerPosition.y)
                            {
                                flag = true;
                            }
                            break;
                    }

                    if (flag)
                    {
                        //backGroundImage.enabled = true;

                        //eventEnter = true;

                        textBoxBar.enabled = true;
                        text_title.enabled = true;

                        buttons[0].SetActive(true);
                        buttons[1].SetActive(true);
                    }
                }
            }
        }

        public void JunpToPotal(bool flag)
        {
            if(flag)
            {
                backGroundImage.enabled = true;

                eventEnter = true;

                backGroundAnimator.enabled = true;
            }

            text_title.enabled = false;
            textBoxBar.enabled = false;

            buttons[0].SetActive(false);
            buttons[1].SetActive(false);
        }
    }
}