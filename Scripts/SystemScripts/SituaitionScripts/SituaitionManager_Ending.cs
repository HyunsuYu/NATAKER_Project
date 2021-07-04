using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

using NATAKER_DLL.StageSpecific.Home.Actor;



namespace Assets.Scripts.SystemScripts.SituaitionScripts
{
    public class SituaitionManager_Ending : MonoBehaviour
    {
        [SerializeField] private PlayerMovement_Home playerMovement;
        [SerializeField] private Transform playerTransform;

        [SerializeField] private Animator nanayang_ending_animator;
        [SerializeField] private RuntimeAnimatorController nanayangDefaultAnimator;

        [SerializeField] private Animator temtemAnimator;

        [SerializeField] private SpriteRenderer[] spriteRenderers;
        [SerializeField] private Light2D[] light2Ds;

        [SerializeField] private Text text;

        [SerializeField] private Image backGroundImage;

        [SerializeField] private Image textBoxBar;
        [SerializeField] private Text text_Title;
        [SerializeField] private Text text_MainText;

        [SerializeField] private GameObject[] buttons;

        private bool backGroundDisappear;
        private bool backGroundAppear;

        private bool eventEnd;
        private bool lookAroundCloudDisappear;

        private bool firstEventEnd;

        private bool somethingExcuted;

        private int curIndex;



        public void Awake()
        {
            backGroundDisappear = true;
        }
        public void Update()
        {
            if (eventEnd == false)
            {
                if (backGroundDisappear)
                {
                    float tempA = backGroundImage.color.a;
                    backGroundImage.color = new Color()
                    {
                        r = 1.0f,
                        g = 1.0f,
                        b = 1.0f,
                        a = tempA - Time.deltaTime
                    };

                    if (backGroundImage.color.a <= 0.05f)
                    {
                        backGroundDisappear = false;

                        backGroundImage.color = new Color()
                        {
                            r = 1.0f,
                            g = 1.0f,
                            b = 1.0f,
                            a = 0.0f
                        };

                        nanayang_ending_animator.SetBool("Start", true);

                        Event();
                        //Invoke("Event", 3.0f);
                    }
                }
                if (backGroundAppear)
                {
                    float tempA = backGroundImage.color.a;
                    backGroundImage.color = new Color()
                    {
                        r = 1.0f,
                        g = 1.0f,
                        b = 1.0f,
                        a = tempA + Time.deltaTime
                    };

                    if (backGroundImage.color.a >= 0.95f)
                    {
                        backGroundAppear = false;

                        backGroundImage.color = new Color()
                        {
                            r = 1.0f,
                            g = 1.0f,
                            b = 1.0f,
                            a = 1.0f
                        };
                    }
                }


                if (firstEventEnd && curIndex == 0)
                {
                    Index6Func();
                }
                if (firstEventEnd && Input.GetKeyDown(KeyCode.Return) && somethingExcuted == false)
                {
                    somethingExcuted = true;

                    switch (curIndex)
                    {
                        case 0:
                            Index7Func();
                            break;

                        case 1:
                            Index8Func();
                            break;

                        case 2:
                            Index9Func();
                            break;

                        case 3:
                            Index10Func();
                            break;

                        case 4:
                            Index11Func();
                            break;

                        case 5:
                            Index12Func();
                            break;

                        case 6:
                            Index13Func();
                            break;

                        case 7:
                            Index14Func();
                            break;

                        case 8:
                            Index15Func();
                            break;

                        case 9:
                            Index16Func();
                            break;

                        case 10:
                            Index17Func();
                            break;

                        case 11:
                            Index18Func();
                            break;

                        case 12:
                            Index19Func();
                            break;

                        case 13:
                            Index20Func();
                            break;
                    }

                    curIndex++;
                }
            }


            if (lookAroundCloudDisappear)
            {
                float tempA = backGroundImage.color.a;
                backGroundImage.color = new Color()
                {
                    r = 1.0f,
                    g = 1.0f,
                    b = 1.0f,
                    a = tempA - Time.deltaTime
                };

                if (backGroundImage.color.a <= 0.05f)
                {
                    backGroundAppear = false;

                    backGroundImage.color = new Color()
                    {
                        r = 1.0f,
                        g = 1.0f,
                        b = 1.0f,
                        a = 0.0f
                    };

                    lookAroundCloudDisappear = false;
                }
            }
        }

        private void Event()
        {
            Invoke("Index0Func", 3.0f);
            Invoke("Index1Func", 6.0f);
            Invoke("Index2Func", 9.0f);
            Invoke("Index3Func", 12.0f);
            Invoke("Index4Func", 15.0f);
            //Invoke("Index6Func", 21.0f);
            //Invoke("Index7Func", 26.0f);
            //Invoke("Index8Func", 31.0f);
            //Invoke("Index9Func", 36.0f);
            //Invoke("Index10Func", 41.0f);
            //Invoke("Index11Func", 45.0f);

            //Invoke("Index11Func", 45.0f);
            //Invoke("Index12Func", 50.0f);
            //Invoke("Index13Func", 55.0f);
            //Invoke("Index14Func", 60.0f);
            //Invoke("Index15Func", 65.0f);
            //Invoke("Index16Func", 70.0f);
            //Invoke("Index17Func", 75.0f);
            //Invoke("Index18Func", 80.0f);
            //Invoke("Index19Func", 85.0f);
            //Invoke("Index20Func", 90.0f);
            //Invoke("Index21Func", 95.0f);
        }

        private void Index0Func()
        {
            nanayang_ending_animator.SetBool("index_0", true);
        }
        private void Index1Func()
        {
            nanayang_ending_animator.SetBool("index_1", true);
        }
        private void Index2Func()
        {
            nanayang_ending_animator.SetBool("index_2", true);

            temtemAnimator.Play("Temtem_Idle_Left");
        }
        private void Index3Func()
        {
            foreach (var temp in spriteRenderers)
            {
                temp.enabled = true;
            }
        }
        private void Index4Func()
        {
            foreach (var temp in light2Ds)
            {
                temp.enabled = true;
            }

            firstEventEnd = true;
        }
        private void Index6Func()
        {
            text.enabled = true;
            text.text = "기획, 아트, 구현  :   + 악질리오이 +";
        }
        private void Index7Func()
        {
            text.text = "여러모로 많이 부족한 게임 플레이해주셔서 정말 감사합니다";

            somethingExcuted = false;
        }
        private void Index8Func()
        {
            text.text = "또한 나나양님의 방송 4주년을 매우매우매우 축하드립니다";

            somethingExcuted = false;
        }
        private void Index9Func()
        {
            text.text = "군대 가서도 꾸준히 방송 보고 응원하겠습니다. 군.바.";

            somethingExcuted = false;
        }
        private void Index10Func()
        {
            text.enabled = false;
            backGroundAppear = true;

            somethingExcuted = false;
        }
        private void Index11Func()
        {
            textBoxBar.enabled = true;
            text_Title.enabled = true;
            text_MainText.enabled = true;

            text_Title.text = "+ ??? +";
            text_MainText.text = "그래. 이 이야기는 이렇게 끝을 맺게 되지";

            somethingExcuted = false;
        }
        private void Index12Func()
        {
            text_Title.text = "+ ??? +";
            text_MainText.text = "...음?";

            somethingExcuted = false;
        }
        private void Index13Func()
        {
            text_Title.text = "+ ??? +";
            text_MainText.text = "그 뒤로 어떻게 되었냐고?";

            somethingExcuted = false;
        }
        private void Index14Func()
        {
            text_Title.text = "+ ??? +";
            text_MainText.text = "난 여전히 나의 위대한 계획을 멈추지 않았어";

            somethingExcuted = false;
        }
        private void Index15Func()
        {
            text_Title.text = "+ ??? +";
            text_MainText.text = "다만, 방향을 조금 바꿔 화해한 너와 함께 우주를 돌아다니며 공연을 뛰어 우주대스타로 거듭났지";

            somethingExcuted = false;
        }
        private void Index16Func()
        {
            text_Title.text = "+ ??? +";
            text_MainText.text = "덕분에 난 차세대 범우주적 음악의 신으로 급부상!";

            somethingExcuted = false;
        }
        private void Index17Func()
        {
            text_Title.text = "+ ??? +";
            text_MainText.text = "이제 내 정체를 알겠어...?";

            somethingExcuted = false;
        }
        private void Index18Func()
        {
            text_Title.text = "+ 다른 차원의 차세대 우주대스타 음악의 신 나나양 +";
            text_MainText.text = "그럼 오늘의 이야기는 해피엔딩으로 이렇게 끝~";

            somethingExcuted = false;
        }
        private void Index19Func()
        {
            text_Title.text = "+ 다른 차원의 차세대 우주대스타 음악의 신 나나양 +";
            text_MainText.text = "빠잉~";

            somethingExcuted = false;
        }
        private void Index20Func()
        {
            //text_Title.text = "";
            //text_MainText.text = "당신은 나나양이 리코더를 부르는 우주 속에서 게임을 종료하려면 엔터키를 눌러야함을 느꼈다";

            text_MainText.text = null;
            text_Title.text = null;

            foreach (var button in buttons)
            {
                button.SetActive(true);
            }

            somethingExcuted = false;

            //Invoke("GameOffActive", 1.0f);
        }

        public void GameOff()
        {
            Application.Quit();
        }
        public void LookAround()
        {
            CancelInvoke("Index0Func");
            CancelInvoke("Index1Func");
            CancelInvoke("Index2Func");
            CancelInvoke("Index3Func");
            CancelInvoke("Index4Func");

            // 7, 18
            playerMovement.PlayerPosition = new Vector2Int()
            {
                x = 7,
                y = 18
            };
            playerTransform.position = new Vector3()
            {
                x = 7.5f,
                y = 18.5f,
                z = 0.0f
            };

            textBoxBar.enabled = false;
            foreach (var button in buttons)
            {
                button.SetActive(false);
            }

            foreach (var imot in spriteRenderers)
            {
                imot.sprite = null;
            }

            nanayang_ending_animator.runtimeAnimatorController = nanayangDefaultAnimator;

            eventEnd = true;
            lookAroundCloudDisappear = true;
        }

        //private void GameOffActive()
        //{
        //    gameOff = true;
        //}
    }
}