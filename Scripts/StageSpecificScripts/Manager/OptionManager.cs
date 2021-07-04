using UnityEngine;
using UnityEngine.UI;



namespace Assets.Scripts.StageSpecificScripts.Manager
{
    public sealed class OptionManager : MonoBehaviour
    {
        [SerializeField] private GameObject optionPanel;

        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Scrollbar scrollbar;

        public bool OptionIsActive { get; set; }

        [SerializeField] private SoundVolumeManager soundVolumeManager;



        public void Awake()
        {
            audioSource.volume = 0.5f;

            soundVolumeManager = GameObject.FindGameObjectsWithTag("VolumeManager")[0].GetComponent<SoundVolumeManager>();

            if (soundVolumeManager != null)
            {
                soundVolumeManager.SoundScrollbar = scrollbar;

                scrollbar.value = soundVolumeManager.Value;
                SoundChange();
            }
        }
        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape) && OptionIsActive == false)
            {
                optionPanel.SetActive(true);

                OptionIsActive = true;
            }
            else if(Input.GetKeyDown(KeyCode.Escape) && OptionIsActive == true)
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

            OptionIsActive = false;
        }
    }
}