using UnityEngine;

using Assets.Scripts.StageSpecificScripts.Actor;



namespace Assets.Scripts.StageSpecificScripts.Data
{
    public sealed class SpearHitBox : MonoBehaviour
    {
        [SerializeField] PlayerStatus playerStatus;



        public void OnTriggerEnter2D(Collider2D other)
        {
            if(other.tag == "Player")
            {
                playerStatus.moveChance--;
                playerStatus.UpdateMoveChance();
            }
        }
    }
}