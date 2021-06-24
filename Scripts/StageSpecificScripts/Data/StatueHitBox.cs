using UnityEngine;



namespace Assets.Scripts.StageSpecificScripts.Data
{
    public sealed class StatueHitBox : MonoBehaviour
    {
        public bool active;
        public KeyCode keyCode;



        public void OnTriggerEnter2D(Collider2D other)
        {
            active = true;
        }
        public void OnTriggerExit2D(Collider2D other)
        {
            active = false;
        }
    }
}