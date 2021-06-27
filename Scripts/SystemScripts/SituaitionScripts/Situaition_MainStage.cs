using UnityEngine;
using UnityEngine.UI;

using Assets.Scripts.StageSpecificScripts.Manager;



namespace Assets.Scripts.SystemScripts.SituaitionScripts
{
    public sealed class Situaition_MainStage : MonoBehaviour
    {
        [SerializeField] private OptionManager optionManager;

        [SerializeField] private Situaition_MainStage_Scripts situaition_MainStage_Scripts;

        [SerializeField] private Image textBoxBarImage;
        [SerializeField] private Text text_title;
        [SerializeField] private Text text_mainText;

        [SerializeField] private Image backGroundImage;
        [SerializeField] private Animator backGroundAnimator;

        private bool scriptFinish;
        private int curScriptIndex;

        public bool eventEnd;

        private bool tempFlag;
        private bool somthingExcuted;



        public void Awake()
        {
            curScriptIndex = 0;

            UpdatScriptState();
        }
        public void Update()
        {
            if (!scriptFinish && !optionManager.optionIsActive)
            {
                if (Input.GetKeyDown(KeyCode.Return) && somthingExcuted == false)
                {
                    somthingExcuted = true;

                    curScriptIndex++;

                    UpdatScriptState();
                }

                if (curScriptIndex == 0)
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
                        backGroundImage.color = new Color()
                        {
                            r = 1.0f,
                            g = 1.0f,
                            b = 1.0f,
                            a = 0.0f
                        };

                        UpdatScriptState();
                    }
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

                ScriptFinish();

                return;
            }

            text_title.text = situaition_MainStage_Scripts.titles[curScriptIndex];
            text_mainText.text = situaition_MainStage_Scripts.mainTexts[curScriptIndex];

            somthingExcuted = false;
        }
        private void ScriptFinish()
        {
            textBoxBarImage.enabled = false;
            text_mainText.enabled = false;
            text_title.enabled = false;

            eventEnd = true;
        }
    }
}