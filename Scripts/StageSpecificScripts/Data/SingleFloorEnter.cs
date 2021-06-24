using UnityEngine;



namespace NATAKER_DLL.StageSpecific.Home.Data
{
    public sealed class SingleFloorEnter : MonoBehaviour
    {
        [SerializeField] private bool enter;



        public void OnTriggerStay2D(Collider2D other)
        {
            enter = true;
        }
        public void OnTriggerExit2D(Collider2D other)
        {
            enter = false;
        }

        public bool Enter
        {
            get => enter;
            set => enter = value;
        }
    }
}