using UnityEngine;
using UnityEngine.UI;



namespace Assets.Scripts.StageSpecificScripts.EventSpecific
{
    public sealed class MainStageEnter : MonoBehaviour
    {
        [SerializeField] private Image backGroundImage;
        [SerializeField] private Animator backGroundAnimator;



        public void FixedUpdate()
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

                backGroundAnimator.enabled = false;

                this.enabled = false;
            }
        }
    }
}