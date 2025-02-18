using UnityEngine;

namespace GM.Maps
{
    public class Map : MonoBehaviour
    {
        // TODO :
        // 

        [SerializeField] private ObjectInfoSO _cell;
        [SerializeField] private Vector2Int _size;

        // TODO : 이거 맵 사이즈 어케 할지 생각 좀
        private MapObject[,] _gridCellArr;

        private void Awake()
        {
            _gridCellArr = new MapObject[1000, 1000];
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CreateGrid();
            }
        }

        public void CreateGrid()
        {
            for (int x = 0; x < _size.x; ++x)
            {
                for (int y = 0; y < _size.y; ++y)
                {
                    float xPos = x * _cell.size.x + transform.position.x;
                    float yPos = y * _cell.size.z + transform.position.y;
                    Vector3 position = new Vector3(xPos, 0, yPos);
                    _gridCellArr[x, y] = CreateCell(position);
                }
            }
        }

        private MapObject CreateCell(Vector3 position)
        {
            MapObject mapObject = Instantiate(_cell.mapObject, position, Quaternion.identity);
            mapObject.transform.parent = transform;

            return mapObject;
        }
    }
}
