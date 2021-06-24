using UnityEngine;
using UnityEngine.UI;



namespace Assets.Scripts.StageSpecificScripts.Manager
{
    public sealed class OptionManager : MonoBehaviour
    {
        [SerializeField] private GameObject optionPanel;

        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Scrollbar scrollbar;

        public bool optionIsActive;



        public void Awake()
        {
            audioSource.volume = 0.5f;
        }
        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                optionPanel.SetActive(true);

                optionIsActive = true;
            }
        }
        public void GameQuit()
        {
            Application.Quit();
        }
        public void SoundChange()
        {
            audioSource.volume = scrollbar.value;
        }
        public void OptionPanelOff()
        {
            optionPanel.SetActive(false);

            optionIsActive = false;
        }
    }
}