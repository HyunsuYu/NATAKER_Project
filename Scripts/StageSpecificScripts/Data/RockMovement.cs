using UnityEngine;

using NATAKER_DLL.StageSpecific.Home.Actor;
using Assets.Scripts.StageSpecificScripts.Manager;


public class RockMovement : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private PlayerMovement_Stage playerMovement;

    [SerializeField] private RockPositionManager rockPositionManager;
    [SerializeField] private WallPositionManager wallPositionManager;

    [SerializeField] private Vector2Int rockPositoon;
    [SerializeField] private int index;
    [SerializeField] private bool move;

    private KeyCode prevKeyCode;
    private float callBackTime;



    public void Awake()
    {
        animator = GetComponent<Animator>();

        callBackTime = animator.runtimeAnimatorController.animationClips[0].length / 2.0f;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (playerMovement.Kicking)
        {
            switch (playerMovement.CurKeyCode)
            {
                case KeyCode.W:
                    if (!(rockPositoon.y == playerMovement.PlayerPosition.y + 1 && rockPositoon.x == playerMovement.PlayerPosition.x) || !(wallPositionManager.WallPosition[rockPositoon.y + 1 - wallPositionManager.Origin.y, rockPositoon.x - wallPositionManager.Origin.x]))
                    {
                        break;
                    }

                    foreach (Vector2Int coord in rockPositionManager.RockPositions)
                    {
                        if (rockPositoon.x == coord.x && rockPositoon.y + 1 == coord.y)
                        {
                            move = false;
                            break;
                        }
                    }

                    if (move)
                    {
                        rockPositoon.y += 1;
                        rockPositionManager.RockPositions[index].y += 1;
                        animator.SetBool("Up", true);

                        Invoke("AnimationCallBack", callBackTime);

                        move = false;
                    }
                    break;

                case KeyCode.S:
                    if (!(rockPositoon.y == playerMovement.PlayerPosition.y - 1 && rockPositoon.x == playerMovement.PlayerPosition.x) || !(wallPositionManager.WallPosition[rockPositoon.y - 1 - wallPositionManager.Origin.y, rockPositoon.x - wallPositionManager.Origin.x]))
                    {
                        break;
                    }

                    foreach (Vector2Int coord in rockPositionManager.RockPositions)
                    {
                        if (rockPositoon.x == coord.x && rockPositoon.y - 1 == coord.y)
                        {
                            move = false;
                            break;
                        }
                    }

                    if (move)
                    {
                        rockPositoon.y -= 1;
                        rockPositionManager.RockPositions[index].y -= 1;
                        animator.SetBool("Down", true);

                        Invoke("AnimationCallBack", callBackTime);

                        move = false;
                    }
                    break;

                case KeyCode.A:
                    if (!(rockPositoon.y == playerMovement.PlayerPosition.y && rockPositoon.x == playerMovement.PlayerPosition.x - 1) || !(wallPositionManager.WallPosition[rockPositoon.y - wallPositionManager.Origin.y, rockPositoon.x - 1 - wallPositionManager.Origin.x]))
                    {
                        break;
                    }

                    foreach (Vector2Int coord in rockPositionManager.RockPositions)
                    {
                        if (rockPositoon.x - 1 == coord.x && rockPositoon.y == coord.y)
                        {
                            move = false;
                            break;
                        }
                    }

                    if (move)
                    {
                        rockPositoon.x -= 1;
                        rockPositionManager.RockPositions[index].x -= 1;
                        animator.SetBool("Left", true);

                        Invoke("AnimationCallBack", callBackTime);

                        move = false;
                    }
                    break;

                case KeyCode.D:
                    if (!(rockPositoon.y == playerMovement.PlayerPosition.y && rockPositoon.x == playerMovement.PlayerPosition.x + 1) || !(wallPositionManager.WallPosition[rockPositoon.y - wallPositionManager.Origin.y, rockPositoon.x + 1 - wallPositionManager.Origin.x]))
                    {
                        break;
                    }

                    foreach (Vector2Int coord in rockPositionManager.RockPositions)
                    {
                        if (rockPositoon.x + 1 == coord.x && rockPositoon.y == coord.y)
                        {
                            move = false;
                            break;
                        }
                    }

                    if (move)
                    {
                        rockPositoon.x += 1;
                        rockPositionManager.RockPositions[index].x += 1;
                        animator.SetBool("Right", true);

                        Invoke("AnimationCallBack", callBackTime);

                        move = false;
                    }
                    break;
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        move = true;
    }

    public Vector2Int RockPosition
    {
        set => rockPositoon = value;
    }
    public int Index
    {
        set => index = value;
    }

    private void AnimationCallBack()
    {
        animator.SetBool("Up", false);
        animator.SetBool("Down", false);
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);
    }
}