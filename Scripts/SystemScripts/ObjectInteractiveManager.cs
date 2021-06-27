using UnityEngine;
using UnityEngine.UI;

using NATAKER_DLL.StageSpecific.Home.Actor;




namespace Assets.Scripts.SystemScripts
{
    public sealed class ObjectInteractiveManager : MonoBehaviour
    {
        [SerializeField] private Image textBoxBarImage;
        [SerializeField] private Text mainText;

        [SerializeField] private PlayerMovement_Home playerMovement_Home;
        [SerializeField] private PlayerMovement_Stage playerMovement_Stage;

        [SerializeField] private bool isHome;
        [SerializeField] private KeyCode direction;

        [SerializeField] private string textToDisplay;

        private bool flag;



        public void OnTriggerStay2D(Collider2D other)
        {
            if(other.tag == "Player" && flag == false && ((isHome ? playerMovement_Home.Kicking : playerMovement_Stage.Kicking)) && (direction == KeyCode.None ? true : (isHome ? direction == playerMovement_Home.CurKeyCode : direction == playerMovement_Stage.CurKeyCode)))
            {
                flag = true;

                textBoxBarImage.enabled = true;

                mainText.enabled = true;
                mainText.text = textToDisplay;
            }
        }
        public void Update()
        {
            if(flag && Input.anyKeyDown)
            {
                mainText.text = null;
                mainText.enabled = false;

                textBoxBarImage.enabled = false;

                flag = false;
            }
        }
    }
}