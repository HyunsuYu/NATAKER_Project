using UnityEngine;
using UnityEngine.Tilemaps;



namespace Assets.Scripts.StageSpecificScripts.Manager
{
    public sealed class WallPositionManager : MonoBehaviour
    {
        private bool[,] wallPosition;

        [SerializeField] private Tilemap tilemap;
        [SerializeField] private Vector2Int origin;
        public Vector2Int mapSize;



        public void Awake()
        {
            mapSize = new Vector2Int()
            {
                x = tilemap.size.x,
                y = tilemap.size.y
            };
            wallPosition = new bool[tilemap.size.y + 2, tilemap.size.x + 2];

            origin = new Vector2Int()
            {
                x = tilemap.origin.x - 1,
                y = tilemap.origin.y - 1
            };

            for (int coord_y = 0; coord_y < tilemap.size.y; coord_y++)
            {
                for (int coord_x = 0; coord_x < tilemap.size.x; coord_x++)
                {
                    wallPosition[coord_y + 1, coord_x + 1] = tilemap.HasTile(new Vector3Int(coord_x + tilemap.origin.x, coord_y + tilemap.origin.y, 0));
                    //Debug.Log((coord_x + tilemap.origin.x + 1) + " : " + (coord_y + tilemap.origin.y + 1) + " - " + wallPosition[coord_y, coord_x]);
                }
            }
        }

        public bool[,] WallPosition
        {
            get => wallPosition;
            set => wallPosition = value;
        }
        public Vector2Int Origin
        {
            get => origin;
        }
    }
}