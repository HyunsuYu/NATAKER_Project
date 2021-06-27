using UnityEngine;
using UnityEngine.UI;



namespace Assets.Scripts.StageSpecificScripts.Manager
{
    public sealed class BackGroundMusicManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Scrollbar scrollbar;



        public void UpdateVolume()
        {
            audioSource.volume = scrollbar.value;
        }
    }
}