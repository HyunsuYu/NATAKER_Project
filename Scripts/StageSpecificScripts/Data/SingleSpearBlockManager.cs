using UnityEngine;

using Assets.Scripts.StageSpecificScripts.Manager;
using Assets.Scripts.StageSpecificScripts.Data;



namespace Assets.Scripts.StageSpecificScripts.Data
{
    public enum SpearAttackDirection : byte
    {
        Up, Down
    };
    public enum SpearAnimationType : byte
    {
        Full = 1, Half = 2, None = 4
    };

    public sealed class SingleSpearBlockManager : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        [SerializeField] private Transform childHitBoxGameObject;

        [SerializeField] private RockPositionManager rockPositionManager;
        [SerializeField] private Vector2Int spearPosition;

        [SerializeField] private SingleStageData stageData;

        public SpearAttackDirection spearAttackDirection;

        private float animationTime;
        private SpearAnimationType spearAnimationType;
        private bool isAttacking;

        private float frameTime = 1.0f / 60.0f;
        private float curFrameTime;
        private float speed_Full = 12.0f;
        private float speed_Nalf = 27.0f;



        public void Awake()
        {
            animationTime = animator.runtimeAnimatorController.animationClips[0].length;

            spearPosition.x -= stageData.offset.x;
            spearPosition.y -= stageData.offset.y;
        }
        public void Update()
        {
            if (isAttacking)
            {
                curFrameTime += Time.deltaTime;
                HitBoxMove();
            }
        }
        public void OnTriggerStay2D(Collider2D other)
        {
            if (other.tag == "Player" && isAttacking == false)
            {
                spearAnimationType = SpearAnimationType.Full;

                foreach (Vector2Int coord in rockPositionManager.RockPositions)
                {
                    if ((spearPosition.x == coord.x && spearPosition.y + 1 == coord.y) || (spearPosition.x == coord.x && spearPosition.y - 1 == coord.y))
                    {
                        spearAnimationType = SpearAnimationType.None;

                        break;
                    }
                    else if ((spearPosition.x == coord.x && spearPosition.y + 2 == coord.y) || (spearPosition.x == coord.x && spearPosition.y - 2 == coord.y))
                    {
                        spearAnimationType = SpearAnimationType.Half;

                        break;
                    }
                }

                isAttacking = true;
                Attack();

                Invoke("AttackCallBack", animationTime);
            }
        }

        public void Attack()
        {
            // Must Implement diffrently depend on spearAnimationType
            // Spear's ANimations must be make three type(None, Half, Full)

            switch (spearAttackDirection)
            {
                case SpearAttackDirection.Up:
                    animator.SetBool("ToUp", true);
                    break;

                case SpearAttackDirection.Down:
                    animator.SetBool("ToDown", true);
                    break;
            }

            switch(spearAnimationType)
            {
                case SpearAnimationType.Full:
                    animator.SetBool("Full", true);
                    break;

                case SpearAnimationType.Half:
                    animator.SetBool("Half", true);
                    break;

                case SpearAnimationType.None:
                    animator.SetBool("None", true);
                    break;
            }

            curFrameTime = 0.0f;

            Invoke("AttackCallBack", animationTime);
        }

        private void AttackCallBack()
        {
            switch (spearAttackDirection)
            {
                case SpearAttackDirection.Up:
                    animator.SetBool("ToUp", false);
                    break;

                case SpearAttackDirection.Down:
                    animator.SetBool("ToDown", false);
                    break;
            }

            switch (spearAnimationType)
            {
                case SpearAnimationType.Full:
                    animator.SetBool("Full", false);
                    break;

                case SpearAnimationType.Half:
                    animator.SetBool("Half", false);
                    break;

                case SpearAnimationType.None:
                    animator.SetBool("None", false);
                    break;
            }

            Invoke("GetDelayedCallBack", 0.5f);
        }
        private void GetDelayedCallBack()
        {
            isAttacking = false;
        }

        private void HitBoxMove()
        {
            switch(spearAttackDirection)
            {
                case SpearAttackDirection.Up:
                    switch(spearAnimationType)
                    {
                        case SpearAnimationType.Full:
                            if (curFrameTime >= frameTime * 20.0f && curFrameTime <= frameTime * 30.0f)
                            {
                                Vector3 targetPosition = new Vector3()
                                {
                                    x = 0.0f,
                                    y = 0.75f,
                                    z = 0.0f
                                };
                                childHitBoxGameObject.localPosition = Vector3.MoveTowards(childHitBoxGameObject.localPosition, targetPosition, Time.deltaTime * speed_Full);
                            }
                            else if(curFrameTime >= frameTime * 42.0f)
                            {
                                Vector3 basePosition = new Vector3()
                                {
                                    x = 0.0f,
                                    y = -1.25f,
                                    z = 0.0f
                                };
                                childHitBoxGameObject.localPosition = Vector3.MoveTowards(childHitBoxGameObject.localPosition, basePosition, Time.deltaTime * speed_Full);
                            }
                            break;

                        case SpearAnimationType.Half:
                            if (curFrameTime >= frameTime * 20.0f && curFrameTime <= frameTime * 22.0f)
                            {
                                Vector3 targetPosition = new Vector3()
                                {
                                    x = 0.0f,
                                    y = -0.25f,
                                    z = 0.0f
                                };
                                childHitBoxGameObject.localPosition = Vector3.MoveTowards(childHitBoxGameObject.localPosition, targetPosition, Time.deltaTime * speed_Nalf);
                            }
                            else if(curFrameTime >= frameTime * 54.0f)
                            {
                                Vector3 basePosition = new Vector3()
                                {
                                    x = 0.0f,
                                    y = -1.25f,
                                    z = 0.0f
                                };
                                childHitBoxGameObject.localPosition = Vector3.MoveTowards(childHitBoxGameObject.localPosition, basePosition, Time.deltaTime * speed_Nalf);
                            }
                            break;
                    }
                    break;

                case SpearAttackDirection.Down:
                    switch (spearAnimationType)
                    {
                        case SpearAnimationType.Full:
                            if (curFrameTime >= frameTime * 20.0f && curFrameTime <= frameTime * 30.0f)
                            {
                                Vector3 targetPosition = new Vector3()
                                {
                                    x = 0.0f,
                                    y = -1.0f,
                                    z = 0.0f
                                };
                                childHitBoxGameObject.localPosition = Vector3.MoveTowards(childHitBoxGameObject.localPosition, targetPosition, Time.deltaTime * speed_Full);
                            }
                            else if(curFrameTime >= frameTime * 42.0f)
                            {
                                Vector3 basePosition = new Vector3()
                                {
                                    x = 0.0f,
                                    y = 1.0f,
                                    z = 0.0f
                                };
                                childHitBoxGameObject.localPosition = Vector3.MoveTowards(childHitBoxGameObject.localPosition, basePosition, Time.deltaTime * speed_Full);
                            }
                            break;

                        case SpearAnimationType.Half:
                            if (curFrameTime >= frameTime * 20.0f && curFrameTime <= frameTime * 24.0f)
                            {
                                Vector3 targetPosition = new Vector3()
                                {
                                    x = 0.0f,
                                    y = -0.5f,
                                    z = 0.0f
                                };
                                childHitBoxGameObject.localPosition = Vector3.MoveTowards(childHitBoxGameObject.localPosition, targetPosition, Time.deltaTime * speed_Nalf);
                            }
                            else if(curFrameTime >= frameTime * 51.0f)
                            {
                                Vector3 basePosition = new Vector3()
                                {
                                    x = 0.0f,
                                    y = 1.0f,
                                    z = 0.0f
                                };
                                childHitBoxGameObject.localPosition = Vector3.MoveTowards(childHitBoxGameObject.localPosition, basePosition, Time.deltaTime * speed_Nalf);
                            }
                            break;
                    }
                    break;
            }
        }
    }
}