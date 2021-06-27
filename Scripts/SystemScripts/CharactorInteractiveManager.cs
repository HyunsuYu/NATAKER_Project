using UnityEngine;
using UnityEngine.UI;

using NATAKER_DLL.StageSpecific.Home.Actor;



namespace Assets.Scripts.SystemScripts
{
    public sealed class CharactorInteractiveManager : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        [SerializeField] private Image textBoxBarImage;
        [SerializeField] private Text mainText;
        [SerializeField] private Text titleText;

        [SerializeField] private PlayerMovement_Home playerMovement_Home;
        [SerializeField] private PlayerMovement_Stage playerMovement_Stage;

        [SerializeField] private bool isHome;

        [SerializeField] private Sprite[] imotSprites_Left;
        [SerializeField] private Sprite[] imotSprites_Right;

        [SerializeField] private SpriteRenderer[] charactorImotSpriteRenderers;
        [SerializeField] private Transform[] charactorImotTransforms;
        [SerializeField] private string[] charactorTitles;
        [SerializeField] private ImotDirection[] charactorImotDirections;

        [SerializeField] private int[] textIndex_First;
        [SerializeField] private string[] textToDisplay_MainText_First;
        [SerializeField] private ImotType[] imotTypes_First;

        [SerializeField] private int[] textIndex_Else;
        [SerializeField] private string[] textToDisplay_MainText_Else;
        [SerializeField] private ImotType[] imotTypes_Else;

        private bool firstEnter, elseEnter;
        private bool flag;
        private int curIndex;

        private Vector2Int basePosition;



        public void Awake()
        {
            basePosition = new Vector2Int()
            {
                x = (int)transform.position.x,
                y = (int)transform.position.y
            };
        }
        public void OnTriggerStay2D(Collider2D other)
        {
            if (other.tag == "Player" && flag == false && ((isHome ? playerMovement_Home.Kicking : playerMovement_Stage.Kicking)))
            {
                if (transform.position.x <= playerMovement_Home.PlayerPosition.x)
                {
                    animator.SetBool("ToRight", true);
                }
                else
                {
                    animator.SetBool("ToRight", false);
                }

                bool tempFlag = false;
                switch(playerMovement_Home.CurKeyCode)
                {
                    case KeyCode.W:
                        if(basePosition.x == playerMovement_Home.PlayerPosition.x && basePosition.y == playerMovement_Home.PlayerPosition.y + 1)
                        {
                            tempFlag = true;
                        }
                        break;

                    case KeyCode.S:
                        if (basePosition.x == playerMovement_Home.PlayerPosition.x && basePosition.y == playerMovement_Home.PlayerPosition.y - 1)
                        {
                            tempFlag = true;
                        }
                        break;

                    case KeyCode.A:
                        if (basePosition.x == playerMovement_Home.PlayerPosition.x - 1 && basePosition.y == playerMovement_Home.PlayerPosition.y)
                        {
                            tempFlag = true;
                        }
                        break;

                    case KeyCode.D:
                        if (basePosition.x == playerMovement_Home.PlayerPosition.x + 1 && basePosition.y == playerMovement_Home.PlayerPosition.y)
                        {
                            tempFlag = true;
                        }
                        break;
                }

                if(tempFlag)
                {
                    flag = true;

                    if (firstEnter == false)
                    {
                        firstEnter = true;
                    }
                    else
                    {
                        elseEnter = true;
                    }

                    textBoxBarImage.enabled = true;
                    mainText.enabled = true;
                    titleText.enabled = true;

                    for (int index = 0; index < charactorImotTransforms.Length; index++)
                    {
                        switch (charactorImotDirections[index])
                        {
                            case ImotDirection.Left:
                                charactorImotTransforms[index].localPosition = new Vector3()
                                {
                                    x = -0.8f,
                                    y = 0.8f,
                                    z = 0.0f
                                };
                                break;

                            case ImotDirection.Right:
                                charactorImotTransforms[index].localPosition = new Vector3()
                                {
                                    x = 0.8f,
                                    y = 0.8f,
                                    z = 0.0f
                                };
                                break;
                        }
                    }

                    curIndex = 0;

                    StateUpdate();
                }
            }
        }

        public void Update()
        {
            if(flag && Input.GetKeyDown(KeyCode.Return))
            {
                foreach (var spriteRenderer in charactorImotSpriteRenderers)
                {
                    spriteRenderer.sprite = null;
                }

                StateUpdate();
            }
        }

        private void StateUpdate()
        {
            if (firstEnter && elseEnter == false)
            {
                if (curIndex == textIndex_First.Length)
                {
                    flag = false;

                    textBoxBarImage.enabled = false;
                    mainText.enabled = false;
                    titleText.enabled = false;

                    foreach (var spriteRenderer in charactorImotSpriteRenderers)
                    {
                        spriteRenderer.sprite = null;
                    }

                    return;
                }

                //  Title
                titleText.text = charactorTitles[textIndex_First[curIndex]];

                //  Main Text
                mainText.text = textToDisplay_MainText_First[curIndex];

                //  Imot
                switch (charactorImotDirections[textIndex_First[curIndex]])
                {
                    case ImotDirection.Left:
                        charactorImotSpriteRenderers[textIndex_First[curIndex]].sprite = imotSprites_Left[(int)imotTypes_First[curIndex] - 1];
                        break;

                    case ImotDirection.Right:
                        charactorImotSpriteRenderers[textIndex_First[curIndex]].sprite = imotSprites_Right[(int)imotTypes_First[curIndex] - 1];
                        break;
                }
            }
            else if (elseEnter)
            {
                if (curIndex == textIndex_Else.Length)
                {
                    flag = false;

                    textBoxBarImage.enabled = false;
                    mainText.enabled = false;
                    titleText.enabled = false;

                    foreach(var spriteRenderer in charactorImotSpriteRenderers)
                    {
                        spriteRenderer.sprite = null;
                    }

                    return;
                }

                //  Title
                titleText.text = charactorTitles[textIndex_Else[curIndex]];

                //  Main Text
                mainText.text = textToDisplay_MainText_Else[curIndex];

                //  Imot
                switch (charactorImotDirections[textIndex_Else[curIndex]])
                {
                    case ImotDirection.Left:
                        charactorImotSpriteRenderers[textIndex_Else[curIndex]].sprite = imotSprites_Left[(int)imotTypes_Else[curIndex] - 1];
                        break;

                    case ImotDirection.Right:
                        charactorImotSpriteRenderers[textIndex_Else[curIndex]].sprite = imotSprites_Right[(int)imotTypes_Else[curIndex] - 1];
                        break;
                }
            }

            curIndex++;
        }
    }
}