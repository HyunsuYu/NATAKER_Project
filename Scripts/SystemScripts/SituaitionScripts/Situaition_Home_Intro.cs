using UnityEngine;
using UnityEngine.UI;

using Assets.Scripts.StageSpecificScripts.Manager;



public sealed class Situaition_Home_Intro : MonoBehaviour
{
    [SerializeField] private OptionManager optionManager;

    [SerializeField] private Transform mainCameraTransform;
    [SerializeField] private Camera mainCamera;
    private Vector3 basePosition;

    [SerializeField] private Image backGroundImage;
    [SerializeField] private Animator backGroundAnimator;

    [SerializeField] private SpriteRenderer potalSpriteRenderer;

    [SerializeField] private SpriteRenderer darkNanayangSpriteRenderer;
    [SerializeField] private Animator darkNanayangAnimator;

    [SerializeField] private SpriteRenderer ttuttiSpriteRenderer;
    [SerializeField] private SpriteRenderer temtemSpriteRenderer;
    [SerializeField] private SpriteRenderer chunyangSpriteRenderer;

    private bool backGroundFinish;
    private bool potalFinish_Appear;
    private bool darkNanayang_Appear;
    private bool hostage_Appear;
    private bool charactors_Disappear;
    private bool tempFlag = false;

    [SerializeField] private Situaition_Home_Intro_Scripts situaition_Home_Intro_Scripts;

    [SerializeField] private Text text_Title;
    [SerializeField] private Text text_MainText;

    [SerializeField] private Sprite[] emotionSprites_Left;
    [SerializeField] private Sprite[] emotionSprites_Right;
    [SerializeField] private Transform emotionGameobject_Left;
    [SerializeField] private Transform emotionGameobject_Right;
    [SerializeField] private SpriteRenderer[] spriteRenderers;

    [SerializeField] private Image textBoxBar;

    private bool cameraMove, scriptFinish;
    [SerializeField] private int curScriptIndex;

    public bool eventEnd;



    public void Awake()
    {
        cameraMove = true;
        curScriptIndex = 0;

        basePosition = new Vector3()
        {
            x = 0.0f,
            y = 0.0f,
            z = -10.0f
        };

        UpdatScriptState();
    }
    public void Update()
    {
        if(!scriptFinish && !optionManager.optionIsActive)
        {
            if (cameraMove)
            {
                if (curScriptIndex == 0)
                {
                    mainCameraTransform.localPosition = situaition_Home_Intro_Scripts.targetCameraPosition;
                    mainCamera.orthographicSize = situaition_Home_Intro_Scripts.targetCameraScale.x;

                    cameraMove = false;
                }
                else
                {
                    mainCameraTransform.localPosition = Vector3.MoveTowards(mainCameraTransform.localPosition, basePosition, Time.deltaTime * situaition_Home_Intro_Scripts.cameraMoveSpeed);
                    mainCamera.orthographicSize = Vector2.MoveTowards(new Vector2(mainCamera.orthographicSize, 0.0f), new Vector2(situaition_Home_Intro_Scripts.baseCameraScale, 0.0f), Time.deltaTime * situaition_Home_Intro_Scripts.cameraScaleSpeed).x;

                    if (Vector3.Distance(mainCameraTransform.localPosition, basePosition) <= 0.2f)
                    {
                        cameraMove = false;
                        scriptFinish = true;

                        mainCameraTransform.localPosition = basePosition;
                        mainCamera.orthographicSize = situaition_Home_Intro_Scripts.baseCameraScale;

                        ScriptFinish();
                    }
                }
            }
            else
            {
                if (Input.anyKeyDown)
                {
                    if(curScriptIndex == 4 && backGroundFinish == false)
                    {
                        tempFlag = true;
                    }
                    else if(curScriptIndex == 6 && potalFinish_Appear == false)
                    {
                        tempFlag = true;
                    }
                    else if(curScriptIndex == 7 && darkNanayang_Appear == false)
                    {
                        //darkNanayangAnimator.enabled = false;

                        tempFlag = true;
                    }
                    else if(curScriptIndex == 11 && hostage_Appear == false)
                    {
                        tempFlag = true;
                    }
                    else if(curScriptIndex == 13 && charactors_Disappear == false)
                    {
                        tempFlag = true;
                    }
                    else
                    {
                        curScriptIndex++;

                        UpdatScriptState();
                    }
                }

                if(tempFlag && curScriptIndex == 4)
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
                        backGroundFinish = true;
                        tempFlag = false;

                        backGroundImage.color = new Color()
                        {
                            r = 1.0f,
                            g = 1.0f,
                            b = 1.0f,
                            a = 0.0f
                        };

                        backGroundAnimator.enabled = false;

                        curScriptIndex++;

                        UpdatScriptState();
                    }
                }
                else if(tempFlag && curScriptIndex == 6)
                {
                    float tempA = potalSpriteRenderer.color.a;
                    potalSpriteRenderer.color = new Color()
                    {
                        r = 1.0f,
                        g = 1.0f,
                        b = 1.0f,
                        a = tempA + Time.deltaTime
                    };

                    if (potalSpriteRenderer.color.a >= 0.95f)
                    {
                        tempFlag = false;

                        potalSpriteRenderer.color = new Color()
                        {
                            r = 1.0f,
                            g = 1.0f,
                            b = 1.0f,
                            a = 1.0f
                        };

                        curScriptIndex++;

                        UpdatScriptState();
                    }
                }
                else if(tempFlag && curScriptIndex == 7)
                {
                    float tempA = darkNanayangSpriteRenderer.color.a;
                    darkNanayangSpriteRenderer.color = new Color()
                    {
                        r = 1.0f,
                        g = 1.0f,
                        b = 1.0f,
                        a = tempA + Time.deltaTime
                    };

                    if (darkNanayangSpriteRenderer.color.a >= 0.95f)
                    {
                        tempFlag = false;

                        darkNanayangSpriteRenderer.color = new Color()
                        {
                            r = 1.0f,
                            g = 1.0f,
                            b = 1.0f,
                            a = 1.0f
                        };

                        //darkNanayangAnimator.enabled = true;

                        curScriptIndex++;

                        UpdatScriptState();
                    }
                }
                else if(tempFlag && curScriptIndex == 11)
                {
                    float tempA = temtemSpriteRenderer.color.a;
                    ttuttiSpriteRenderer.color = new Color()
                    {
                        r = 1.0f,
                        g = 1.0f,
                        b = 1.0f,
                        a = tempA + Time.deltaTime
                    };
                    temtemSpriteRenderer.color = new Color()
                    {
                        r = 1.0f,
                        g = 1.0f,
                        b = 1.0f,
                        a = tempA + Time.deltaTime
                    };
                    chunyangSpriteRenderer.color = new Color()
                    {
                        r = 1.0f,
                        g = 1.0f,
                        b = 1.0f,
                        a = tempA + Time.deltaTime
                    };

                    if (temtemSpriteRenderer.color.a >= 0.95f)
                    {
                        tempFlag = false;

                        ttuttiSpriteRenderer.color = new Color()
                        {
                            r = 1.0f,
                            g = 1.0f,
                            b = 1.0f,
                            a = 1.0f
                        };
                        temtemSpriteRenderer.color = new Color()
                        {
                            r = 1.0f,
                            g = 1.0f,
                            b = 1.0f,
                            a = 1.0f
                        };
                        chunyangSpriteRenderer.color = new Color()
                        {
                            r = 1.0f,
                            g = 1.0f,
                            b = 1.0f,
                            a = 1.0f
                        };

                        curScriptIndex++;

                        UpdatScriptState();
                    }
                }
                else if(tempFlag && curScriptIndex == 13)
                {
                    float tempA = darkNanayangSpriteRenderer.color.a;
                    darkNanayangSpriteRenderer.color = new Color()
                    {
                        r = 1.0f,
                        g = 1.0f,
                        b = 1.0f,
                        a = tempA - Time.deltaTime
                    };
                    ttuttiSpriteRenderer.color = new Color()
                    {
                        r = 1.0f,
                        g = 1.0f,
                        b = 1.0f,
                        a = tempA - Time.deltaTime
                    };
                    temtemSpriteRenderer.color = new Color()
                    {
                        r = 1.0f,
                        g = 1.0f,
                        b = 1.0f,
                        a = tempA - Time.deltaTime
                    };
                    chunyangSpriteRenderer.color = new Color()
                    {
                        r = 1.0f,
                        g = 1.0f,
                        b = 1.0f,
                        a = tempA - Time.deltaTime
                    };

                    if (darkNanayangSpriteRenderer.color.a <= 0.05f)
                    {
                        tempFlag = false;

                        darkNanayangSpriteRenderer.enabled = false;
                        ttuttiSpriteRenderer.enabled = false;
                        temtemSpriteRenderer.enabled = false;
                        chunyangSpriteRenderer.enabled = false;

                        curScriptIndex++;

                        UpdatScriptState();
                    }
                }
            }
        }
    }

    private void UpdatScriptState()
    {
        if (curScriptIndex == situaition_Home_Intro_Scripts.mainTexts.Length)
        {
            cameraMove = true;

            return;
        }

        text_Title.text = situaition_Home_Intro_Scripts.titles[curScriptIndex];
        text_MainText.text = situaition_Home_Intro_Scripts.mainTexts[curScriptIndex];

        if (situaition_Home_Intro_Scripts.charactorIndexesPerText[curScriptIndex] != -1)
        {
            switch (situaition_Home_Intro_Scripts.imotDirection[curScriptIndex])
            {
                case ImotDirection.Left:
                    emotionGameobject_Left.localPosition = new Vector3(-0.8f, 0.8f, 0.0f);

                    switch (situaition_Home_Intro_Scripts.imots[curScriptIndex])
                    {
                        case ImotType.Cheer:
                            spriteRenderers[situaition_Home_Intro_Scripts.charactorIndexesPerText[curScriptIndex]].sprite = emotionSprites_Left[(int)ImotType.Cheer];
                            break;

                        case ImotType.IAmNanayang:
                            spriteRenderers[situaition_Home_Intro_Scripts.charactorIndexesPerText[curScriptIndex]].sprite = emotionSprites_Left[(int)ImotType.IAmNanayang];
                            break;

                        case ImotType.Talking:
                            spriteRenderers[situaition_Home_Intro_Scripts.charactorIndexesPerText[curScriptIndex]].sprite = emotionSprites_Left[(int)ImotType.Talking];
                            break;

                        case ImotType.Timo:
                            spriteRenderers[situaition_Home_Intro_Scripts.charactorIndexesPerText[curScriptIndex]].sprite = emotionSprites_Left[(int)ImotType.Timo];
                            break;

                        case ImotType.Warning:
                            spriteRenderers[situaition_Home_Intro_Scripts.charactorIndexesPerText[curScriptIndex]].sprite = emotionSprites_Left[(int)ImotType.Warning];
                            break;
                    }
                    break;

                case ImotDirection.Right:
                    emotionGameobject_Right.localPosition = new Vector3(0.8f, 0.8f, 0.0f);

                    switch (situaition_Home_Intro_Scripts.imots[curScriptIndex])
                    {
                        case ImotType.Cheer:
                            spriteRenderers[situaition_Home_Intro_Scripts.charactorIndexesPerText[curScriptIndex]].sprite = emotionSprites_Right[(int)ImotType.Cheer];
                            break;

                        case ImotType.IAmNanayang:
                            spriteRenderers[situaition_Home_Intro_Scripts.charactorIndexesPerText[curScriptIndex]].sprite = emotionSprites_Right[(int)ImotType.IAmNanayang];
                            break;

                        case ImotType.Talking:
                            spriteRenderers[situaition_Home_Intro_Scripts.charactorIndexesPerText[curScriptIndex]].sprite = emotionSprites_Right[(int)ImotType.Talking];
                            break;

                        case ImotType.Timo:
                            spriteRenderers[situaition_Home_Intro_Scripts.charactorIndexesPerText[curScriptIndex]].sprite = emotionSprites_Right[(int)ImotType.Timo];
                            break;

                        case ImotType.Warning:
                            spriteRenderers[situaition_Home_Intro_Scripts.charactorIndexesPerText[curScriptIndex]].sprite = emotionSprites_Right[(int)ImotType.Warning];
                            break;
                    }
                    break;
            }
        }
        else
        {
            foreach(var renderer in spriteRenderers)
            {
                renderer.sprite = null;
            }
        }
    }
    private void ScriptFinish()
    {
        text_Title.text = "지금 포탈 속으로 뛰어들까요?";

        textBoxBar.enabled = false;
        text_MainText.enabled = false;
        text_Title.enabled = false;

        foreach (var renderer in spriteRenderers)
        {
            renderer.sprite = null;
        }

        eventEnd = true;
    }
}