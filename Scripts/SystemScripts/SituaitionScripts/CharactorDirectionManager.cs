using UnityEngine;



namespace Assets.Scripts.SystemScripts.SituaitionScripts
{
    public sealed class CharactorDirectionManager : MonoBehaviour
    {
        [SerializeField] private bool toRight;
        [SerializeField] private Animator animator;



        public void Awake()
        {
            if(toRight)
            {
                animator.SetBool("ToRight", true);
            }
            else
            {
                animator.SetBool("ToRight", false);
            }
        }
    }
}