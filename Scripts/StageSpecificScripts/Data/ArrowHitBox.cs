using UnityEngine;

using Assets.Scripts.StageSpecificScripts.Actor;



namespace Assets.Scripts.StageSpecificScripts.Data
{
    public sealed class ArrowHitBox : MonoBehaviour
    {
        [SerializeField] private PlayerStatus playerStatus;
        public bool collision;



        public void OnTriggerEnter2D(Collider2D other)
        {
            switch(other.tag)
            {
                case "Player":
                    playerStatus.moveChance -= 3;
                    playerStatus.UpdateMoveChance();
                    break;

                case "Rock":
                    collision = true;
                    break;
            }
        }
    }
}