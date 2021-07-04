using UnityEngine;
using UnityEngine.UI;



namespace Assets.Scripts.StageSpecificScripts.Actor
{
    public sealed class PlayerStatus : MonoBehaviour
    {
        public int MoveChance { get; set; }

        [SerializeField] private Text leftMoveChance;



        public void Awake()
        {
            leftMoveChance.text = MoveChance.ToString();
        }
        public void UpdateMoveChance()
        {
            leftMoveChance.text = MoveChance.ToString();
        }
    }
}