using UnityEngine;

using NATAKER_DLL.StageSpecific.Home.Actor;



namespace Assets.Scripts.StageSpecificScripts.Manager
{
    public sealed class GameObjectPriortyManager : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private SpriteRenderer playerSpriteRenderer;

        [SerializeField] private GameObject[] objects;
        private Transform[] objectTransforms;
        private SpriteRenderer[] objectSpriteRenderers;


        
        public void Awake()
        {
            objectTransforms = new Transform[objects.Length];
            objectSpriteRenderers = new SpriteRenderer[objects.Length];

            for(int index = 0; index < objects.Length; index++)
            {
                objectTransforms[index] = objects[index].GetComponent<Transform>();
                objectSpriteRenderers[index] = objects[index].GetComponent<SpriteRenderer>();
            }
        }
        public void FixedUpdate()
        {
            for(int index = 0; index < objects.Length; index++)
            {
                if(playerTransform.position.y > objectTransforms[index].position.y)
                {
                    objectSpriteRenderers[index].sortingOrder = playerSpriteRenderer.sortingOrder + 1;
                }
                else
                {
                    objectSpriteRenderers[index].sortingOrder = playerSpriteRenderer.sortingOrder - 1;
                }
            }
        }
    }
}