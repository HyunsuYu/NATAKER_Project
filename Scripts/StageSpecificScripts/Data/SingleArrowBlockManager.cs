using UnityEngine;

using Assets.Scripts.StageSpecificScripts.Manager;
using Assets.Scripts.StageSpecificScripts.Actor;
using Assets.Scripts.StageSpecificScripts.Data;



namespace Assets.Scripts.StageSpecificScripts.Data
{
    public enum Direction : byte
    {
        Left,
        Right
    };

    public sealed class SingleArrowBlockManager : MonoBehaviour
    {
        [SerializeField] private Direction shotDirection;
        [SerializeField] private Vector3 targetPosition;

        private bool isShot;

        public int ArrowCount { get; set; }

        [SerializeField] private int maxFlyLength;
        [SerializeField] private float flySpeed;

        [SerializeField] private Transform arrowTransform;
        [SerializeField] private BoxCollider2D arrowCollider;
        [SerializeField] private ArrowHitBox arrowHitBox;



        public void Awake()
        {
            ArrowCount = 3;

            targetPosition = new Vector3()
            {
                x = transform.position.x + (((shotDirection == Direction.Left) ? -1.0f : 1.0f) * maxFlyLength),
                y = transform.position.y,
                z = 0.0f
            };
        }
        public void FixedUpdate()
        {
            if (isShot && !arrowHitBox.Collision)
            {
                if (Vector3.Distance(arrowTransform.position, targetPosition) <= 0.2f)
                {
                    isShot = false;

                    arrowCollider.enabled = false;
                }

                float step = flySpeed * Time.deltaTime;

                arrowTransform.position = Vector3.MoveTowards(arrowTransform.position, targetPosition, step);
            }
            else if(arrowHitBox.Collision)
            {
                isShot = false;

                arrowCollider.enabled = false;
            }
        }
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (ArrowCount > 0 && isShot == false)
            {
                arrowCollider.enabled = true;

                ArrowCount--;

                arrowTransform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

                isShot = true;

                arrowHitBox.Collision = false;
            }
        }
    }
}