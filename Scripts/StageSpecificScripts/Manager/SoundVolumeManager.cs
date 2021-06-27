using UnityEngine;
using UnityEngine.UI;



namespace Assets.Scripts.StageSpecificScripts.Manager
{
    public sealed class SoundVolumeManager : MonoBehaviour
    {
        public float value;
        public Scrollbar scrollbar;



        public void Awake()
        {
            value = 0.5f;

            DontDestroyOnLoad(this.gameObject);
        }
        public void FixedUpdate()
        {
            value = scrollbar.value;
        }
    }
}