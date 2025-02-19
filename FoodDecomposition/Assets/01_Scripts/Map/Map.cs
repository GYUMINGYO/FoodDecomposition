using UnityEngine;

namespace GM.Maps
{
    public class Map : MonoBehaviour
    {
        // TODO : 이거 맵 사이즈 어케 할지 생각 좀
        private MapObject[,] _gridCellArr;

        private void Awake()
        {
            _gridCellArr = new MapObject[1000, 1000];
        }
    }
}
