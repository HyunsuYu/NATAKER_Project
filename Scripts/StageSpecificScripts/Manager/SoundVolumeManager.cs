using UnityEngine;
using UnityEngine.UI;



namespace Assets.Scripts.StageSpecificScripts.Manager
{
    public sealed class SoundVolumeManager : MonoBehaviour
    {
        public float Value { get; private set; }
        [SerializeField] private Scrollbar scrollbar;



        public void Awake()
        {
            Value = 0.5f;

            DontDestroyOnLoad(this.gameObject);
        }
        public void FixedUpdate()
        {
            Value = scrollbar.value;
        }

        public Scrollbar SoundScrollbar
        {
            set => scrollbar = value;
        }
    }
}