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

        [SerializeField] private SoundVolumeManager soundVolumeManager;



        public void Awake()
        {
            audioSource.volume = 0.5f;

            soundVolumeManager = GameObject.FindGameObjectsWithTag("VolumeManager")[0].GetComponent<SoundVolumeManager>();

            if (soundVolumeManager != null)
            {
                soundVolumeManager.scrollbar = scrollbar;

                scrollbar.value = soundVolumeManager.value;
                SoundChange();
            }
        }
        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape) && optionIsActive == false)
            {
                optionPanel.SetActive(true);

                optionIsActive = true;
            }
            else if(Input.GetKeyDown(KeyCode.Escape) && optionIsActive == true)
            {
                OptionPanelOff();
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