using UnityEngine;



namespace Assets.Scripts.StageSpecificScripts.Data
{
    public sealed class StatueHitBox : MonoBehaviour
    {
        public bool Active { get; private set; }
        [SerializeField] private KeyCode targetKeyCode;



        public void OnTriggerEnter2D(Collider2D other)
        {
            Active = true;
        }
        public void OnTriggerExit2D(Collider2D other)
        {
            Active = false;
        }

        public KeyCode TargetKeyCode
        {
            get => targetKeyCode;
        }
    }
}