using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Assets.Scripts.StageSpecificScripts.Manager;



namespace Assets.Scripts.SystemScripts.SituaitionScripts
{
    public sealed class Situaition_MainStage_Exit : MonoBehaviour
    {
        [SerializeField] private OptionManager optionManager;

        [SerializeField] private Situaition_MainStage_Scripts situaition_MainStage_Scripts;

        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Transform NanayangTransform;

        [SerializeField] private Image textBoxBarImage;
        [SerializeField] private Text text_title;
        [SerializeField] private Text text_mainText;

        [SerializeField] private Image backGroundImage;
        [SerializeField] private Animator backGroundAnimator;

        [SerializeField] private Sprite[] emotionSprites_Left;
        [SerializeField] private Sprite[] emotionSprites_Right;
        [SerializeField] private SpriteRenderer[] spriteRenderers;
        [SerializeField] private Transform[] charactorTransforms;

        [SerializeField] private Animator nanayangAnimator;
        [SerializeField] private Animator darkNanayangAnimator;
        [SerializeField] private Animator ttuttiAnimator;
        [SerializeField] private Animator temtemAnimator;
        [SerializeField] private Animator chunyangAnimator;
        [SerializeField] private Animator gateAnimator;
        [SerializeField] private Animator[] chainAnimators;

        [SerializeField] private GameObject darkNanayangObject;
        [SerializeField] private GameObject ttuttiObject;
        [SerializeField] private GameObject temtemObject;
        [SerializeField] private GameObject chunyangObject;

        [SerializeField] private SpriteRenderer[] chainSpriteRenderers;

        private bool scriptFinish;
        [SerializeField] private int curScriptIndex;

        public bool eventEnd;
        public bool startEvent;

        [SerializeField] private GameObject frame;

        private bool tempFlag;
        private bool chainFlag, gateAppearFlag, firstRunFlag;

        private bool somthingExcuted;



        public void Awake()
        {
            curScriptIndex = 0;

            cameraTransform.parent = NanayangTransform;
            cameraTransform.localPosition = new Vector3(0.0f, 0.0f, -10.0f);

            nanayangAnimator.Play("Nanayang_Final_0");
            Invoke("StartFunc", 2.0f);

            frame.SetActive(false);

            UpdatScriptState();
        }
        public void Update()
        {
            if (startEvent && !scriptFinish && !optionManager.optionIsActive)
            {
                if (Input.GetKeyDown(KeyCode.Return) && somthingExcuted == false)
                {
                    somthingExcuted = true;

                    if(curScriptIndex == 7 && chainFlag == false)
                    {
                        chainFlag = true;
                        tempFlag = true;
                    }
                    else if(curScriptIndex == 10 && gateAppearFlag == false)
                    {
                        gateAppearFlag = true;
                        tempFlag = true;
                    }
                    else if(curScriptIndex == 11 && firstRunFlag == false)
                    {
                        firstRunFlag = true;
                        tempFlag = true;
                    }
                    else if(curScriptIndex == 15)
                    {
                        darkNanayangAnimator.Play("DarkNanatang_Final_6");

                        curScriptIndex++;

                        UpdatScriptState();
                    }
                    else if(curScriptIndex == situaition_MainStage_Scripts.mainTexts.Length - 1)
                    {
                        backGroundAnimator.enabled = true;

                        tempFlag = true;
                    }
                    else
                    {
                        curScriptIndex++;

                        if(curScriptIndex < situaition_MainStage_Scripts.mainTexts.Length)
                        {
                            UpdatScriptState();
                        }
                    }
                }

                if(tempFlag && curScriptIndex == 7 && chainFlag)
                {
                    chainFlag = false;

                    foreach (var temp in chainAnimators)
                    {
                        temp.Play("Chain");
                    }

                    Invoke("ChainDisappearFunc", 3.0f);
                }
                else if(tempFlag && curScriptIndex == 10 && gateAppearFlag)
                {
                    gateAppearFlag = false;

                    gateAnimator.Play("Gate_Final_2");

                    Invoke("GateAppearFunc", gateAnimator.runtimeAnimatorController.animationClips[0].length);
                }
                else if(tempFlag && curScriptIndex == 11 && firstRunFlag)
                {
                    firstRunFlag = false;

                    Invoke("TemtemRunFunc", 1.0f);
                    Invoke("ChunyangRunFunc", 2.0f);
                    Invoke("TtuttiRunFunc", 3.0f);
                    Invoke("NanayangRunFunc", 5.0f);
                    Invoke("DarkNanayangSadFunc", 6.5f);

                    Invoke("FirstRunEndFunc", 11.0f);
                }
                else if(tempFlag && curScriptIndex == situaition_MainStage_Scripts.mainTexts.Length - 1)
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
                        tempFlag = false;

                        backGroundImage.color = new Color()
                        {
                            r = 1.0f,
                            g = 1.0f,
                            b = 1.0f,
                            a = 1.0f
                        };

                        curScriptIndex++;

                        UpdatScriptState();
                    }

                    Invoke("GoToEnding", 7.0f);
                }
            }
        }

        private void UpdatScriptState()
        {
            if (curScriptIndex == situaition_MainStage_Scripts.mainTexts.Length)
            {
                text_title.enabled = false;
                text_mainText.enabled = false;

                textBoxBarImage.enabled = false;

                scriptFinish = true;

                return;
            }
            else
            {
                text_title.text = situaition_MainStage_Scripts.titles[curScriptIndex];
                text_mainText.text = situaition_MainStage_Scripts.mainTexts[curScriptIndex];

                somthingExcuted = false;
            }
        }
        private void ScriptFinish()
        {
            textBoxBarImage.enabled = false;
            text_mainText.enabled = false;
            text_title.enabled = false;

            if (situaition_MainStage_Scripts.charactorIndexesPerText[curScriptIndex] != -1)
            {
                switch (situaition_MainStage_Scripts.imotDirection[curScriptIndex])
                {
                    case ImotDirection.Left:
                        charactorTransforms[situaition_MainStage_Scripts.charactorIndexesPerText[curScriptIndex]].position = new Vector3(-0.8f, 0.8f, 0.0f);

                        switch (situaition_MainStage_Scripts.imots[curScriptIndex])
                        {
                            case ImotType.Cheer:
                                spriteRenderers[situaition_MainStage_Scripts.charactorIndexesPerText[curScriptIndex]].sprite = emotionSprites_Left[(int)ImotType.Cheer];
                                break;

                            case ImotType.IAmNanayang:
                                spriteRenderers[situaition_MainStage_Scripts.charactorIndexesPerText[curScriptIndex]].sprite = emotionSprites_Left[(int)ImotType.IAmNanayang];
                                break;

                            case ImotType.Talking:
                                spriteRenderers[situaition_MainStage_Scripts.charactorIndexesPerText[curScriptIndex]].sprite = emotionSprites_Left[(int)ImotType.Talking];
                                break;

                            case ImotType.Timo:
                                spriteRenderers[situaition_MainStage_Scripts.charactorIndexesPerText[curScriptIndex]].sprite = emotionSprites_Left[(int)ImotType.Timo];
                                break;

                            case ImotType.Warning:
                                spriteRenderers[situaition_MainStage_Scripts.charactorIndexesPerText[curScriptIndex]].sprite = emotionSprites_Left[(int)ImotType.Warning];
                                break;
                        }
                        break;

                    case ImotDirection.Right:
                        charactorTransforms[situaition_MainStage_Scripts.charactorIndexesPerText[curScriptIndex]].position = new Vector3(0.8f, 0.8f, 0.0f);

                        switch (situaition_MainStage_Scripts.imots[curScriptIndex])
                        {
                            case ImotType.Cheer:
                                spriteRenderers[situaition_MainStage_Scripts.charactorIndexesPerText[curScriptIndex]].sprite = emotionSprites_Right[(int)ImotType.Cheer];
                                break;

                            case ImotType.IAmNanayang:
                                spriteRenderers[situaition_MainStage_Scripts.charactorIndexesPerText[curScriptIndex]].sprite = emotionSprites_Right[(int)ImotType.IAmNanayang];
                                break;

                            case ImotType.Talking:
                                spriteRenderers[situaition_MainStage_Scripts.charactorIndexesPerText[curScriptIndex]].sprite = emotionSprites_Right[(int)ImotType.Talking];
                                break;

                            case ImotType.Timo:
                                spriteRenderers[situaition_MainStage_Scripts.charactorIndexesPerText[curScriptIndex]].sprite = emotionSprites_Right[(int)ImotType.Timo];
                                break;

                            case ImotType.Warning:
                                spriteRenderers[situaition_MainStage_Scripts.charactorIndexesPerText[curScriptIndex]].sprite = emotionSprites_Right[(int)ImotType.Warning];
                                break;
                        }
                        break;
                }
            }
            else
            {
                foreach (var renderer in spriteRenderers)
                {
                    renderer.sprite = null;
                }
            }


            eventEnd = true;
        }

        private void StartFunc()
        {
            darkNanayangObject.SetActive(true);
            ttuttiObject.SetActive(true);
            temtemObject.SetActive(true);
            chunyangObject.SetActive(true);

            darkNanayangAnimator.enabled = true;
            ttuttiAnimator.enabled = true;
            temtemAnimator.enabled = true;
            chunyangAnimator.enabled = true;

            darkNanayangAnimator.Play("DarkNanayang_Final_1");
            ttuttiAnimator.Play("Ttutti_Final_1");
            temtemAnimator.Play("Temtem_Final_1");
            chunyangAnimator.Play("Chunyang_Final_1");

            Invoke("TempFunc", 1.5f);   
        }
        private void TempFunc()
        {
            startEvent = true;

            textBoxBarImage.enabled = true;
            text_title.enabled = true;
            text_mainText.enabled = true;
        }
        private void ChainDisappearFunc()
        {
            foreach(var temp in chainSpriteRenderers)
            {
                temp.enabled = false;
            }

            curScriptIndex++;

            UpdatScriptState();
        }
        private void GateAppearFunc()
        {
            tempFlag = false;

            curScriptIndex++;

            UpdatScriptState();
        }
        private void TtuttiRunFunc()
        {
            ttuttiAnimator.Play("Ttutti_Final_3");
        }
        private void TemtemRunFunc()
        {
            temtemAnimator.Play("Temtem_Final_3");
        }
        private void ChunyangRunFunc()
        {
            chunyangAnimator.Play("Chunyang_Final_3");
        }
        private void NanayangRunFunc()
        {
            nanayangAnimator.Play("Nanayang_Final_4");
        }
        private void DarkNanayangSadFunc()
        {
            darkNanayangAnimator.Play("DarkNanatang_Final_5");
        }
        private void FirstRunEndFunc()
        {
            tempFlag = false;

            curScriptIndex++;

            UpdatScriptState();
        }
        private void GoToEnding()
        {
            SceneManager.LoadScene(2);
        }
    }
}