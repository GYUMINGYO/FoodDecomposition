using System.Collections.Generic;
using UnityEngine;

namespace GM.Maps
{
    public class Map : MonoBehaviour
    {
        // TODO : 이거 맵 사이즈 어케 할지 생각 좀
        private MapObject[,] _tileArr;
        [SerializeField] private Grid _grid;

        private void Awake()
        {
            _tileArr = new MapObject[3000, 3000];
        }

        public MapObject GetTileObject(Vector3Int position)
        {
            return _tileArr[position.x, position.z];
        }

        public List<MapObject> GetTileObjects(List<Vector3Int> positionList)
        {
            List<MapObject> mapObjects = new List<MapObject>();
            foreach (Vector3Int position in positionList)
            {
                if (_tileArr[position.x, position.z] == null) continue;

                mapObjects.Add(_tileArr[position.x, position.z]);
            }
            return mapObjects;
        }

        public void SetTileObject(ObjectInfoSO mapObjectInfo, Vector3Int position)
        {
            if (_tileArr[position.x, position.z] != null) return;

            Vector3 mapObjPosition = _grid.CellToWorld(position);
            mapObjPosition.x += mapObjectInfo.objectSize.x;
            mapObjPosition.y += mapObjectInfo.objectSize.y;
            mapObjPosition.z += mapObjectInfo.objectSize.z;

            Tile tile = Instantiate(mapObjectInfo.mapObject, mapObjPosition, Quaternion.identity).GetComponent<Tile>();

            // even number
            if (((position.x + position.z) & 1) == 0)
            {
                tile.SetColor(Color.white);
            }
            else
            {
                tile.SetColor(Color.black);
            }

            tile.ShowOutLine(true);
            _tileArr[position.x, position.z] = tile;
        }

        public void DeleteTileObject(Vector3Int position)
        {
            // TODO : 삭제 방법 바꾸기 (Pool)
            Destroy(_tileArr[position.x, position.z]);
            _tileArr[position.x, position.z] = null;
        }

        public void DeleteTileObjects(List<Vector3Int> positionList)
        {
            // TODO : 삭제 방법 바꾸기 (Pool)
            foreach (var pos in positionList)
            {
                if (_tileArr[pos.x, pos.z] == null) continue;

                Destroy(_tileArr[pos.x, pos.z].gameObject);
                _tileArr[pos.x, pos.z] = null;
            }
        }
    }
}
