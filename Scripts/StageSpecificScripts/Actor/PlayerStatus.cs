using UnityEngine;
using UnityEngine.UI;



namespace Assets.Scripts.StageSpecificScripts.Actor
{
    public sealed class PlayerStatus : MonoBehaviour
    {
        public int moveChance;

        [SerializeField] Text leftMoveChance;



        public void Awake()
        {
            leftMoveChance.text = moveChance.ToString();
        }
        public void UpdateMoveChance()
        {
            leftMoveChance.text = moveChance.ToString();
        }
    }
}