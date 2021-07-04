using UnityEngine;



namespace Assets.Scripts.StageSpecificScripts.Data
{
    public sealed class SingleFloorEnter : MonoBehaviour
    {
        public bool Enter { get; set; }



        public void OnTriggerStay2D(Collider2D other)
        {
            Enter = true;
        }
        public void OnTriggerExit2D(Collider2D other)
        {
            Enter = false;
        }
    }
}