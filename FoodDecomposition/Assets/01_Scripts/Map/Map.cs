using System.Collections.Generic;
using UnityEngine;

namespace GM.Maps
{
    public class Map : MonoBehaviour
    {
        // TODO : 이거 맵 사이즈 어케 할지 생각 좀
        [SerializeField] private Grid _grid;

        private Tile[,] _tileArr;
        private List<Tile> _tempTileList;

        public uint CurrentTileCount => _tileCount;
        private uint _tileCount = 0;

        private void Awake()
        {
            _tileArr = new Tile[3000, 3000];
            _tempTileList = new List<Tile>();
        }

        // TODO : 구조 개선
        // 지금은 그냥 타일 단일 구조임
        public Tile GetTileObject(Vector3Int position)
        {
            return _tileArr[position.x, position.z];
        }

        public List<Tile> GetTileObjects(List<Vector3Int> positionList)
        {
            List<Tile> mapObjects = new List<Tile>();
            foreach (Vector3Int position in positionList)
            {
                if (_tileArr[position.x, position.z] == null) continue;

                mapObjects.Add(_tileArr[position.x, position.z]);
            }
            return mapObjects;
        }

        // TODO : 그전 타일 저장 해 놔서 껐다 키기기

        public void SetTemporaryTile(ObjectInfoSO mapObjectInfo, Vector3Int startPos, Vector3Int endPos)
        {
            // Initalize
            if (_tempTileList.Count > 0)
            {
                foreach (Tile tile in _tempTileList)
                {
                    tile.gameObject.SetActive(false);
                }
                _tempTileList.Clear();
            }

            List<Tile> tempTileList = new List<Tile>();

            // Calculate Distance
            int maxX = Mathf.Max(startPos.x, endPos.x);
            int minX = Mathf.Min(startPos.x, endPos.x);

            int maxZ = Mathf.Max(startPos.z, endPos.z);
            int minZ = Mathf.Min(startPos.z, endPos.z);

            for (int x = minX; x <= maxX; ++x)
            {
                for (int z = minZ; z <= maxZ; ++z)
                {
                    if (_tileArr[x, z] != null)
                    {
                        if (_tileArr[x, z].IsFull) continue;

                        _tileArr[x, z].gameObject.SetActive(true);
                        tempTileList.Add(_tileArr[x, z]);
                        continue;
                    }

                    Vector3 mapObjPosition = _grid.CellToWorld(new Vector3Int(x, 0, z));
                    mapObjPosition.x += mapObjectInfo.objectSize.x;
                    mapObjPosition.y += mapObjectInfo.objectSize.y;
                    mapObjPosition.z += mapObjectInfo.objectSize.z;

                    Tile tile = Instantiate(mapObjectInfo.mapObject, mapObjPosition, Quaternion.identity).GetComponent<Tile>();

                    // even number
                    if (((x + z) & 1) == 0)
                    {
                        tile.SetColor(Color.white);
                    }
                    else
                    {
                        tile.SetColor(Color.black);
                    }
                    tile.SetCollider(false);
                    tile.ColorTransparent(true);
                    tile.ShowOutLine(true);
                    _tileArr[x, z] = tile;
                    tempTileList.Add(tile);
                }
            }
            _tempTileList = tempTileList;
        }

        public void SetTileObject()
        {
            DeleteOutline();

            foreach (Tile tile in _tempTileList)
            {
                tile.IsFull = true;
                tile.SetCollider(true);
                tile.ColorTransparent(false);
                ++_tileCount;
            }
            _tempTileList.Clear();
        }

        public void TileShowOutlie(Vector3Int startPos, Vector3Int endPos)
        {
            DeleteOutline();
            _tempTileList.Clear();

            // Calculate Distance
            int maxX = Mathf.Max(startPos.x, endPos.x);
            int minX = Mathf.Min(startPos.x, endPos.x);

            int maxZ = Mathf.Max(startPos.z, endPos.z);
            int minZ = Mathf.Min(startPos.z, endPos.z);

            for (int x = minX; x <= maxX; ++x)
            {
                for (int z = minZ; z <= maxZ; ++z)
                {
                    if (_tileArr[x, z] == null) continue;

                    _tileArr[x, z].ShowOutLine(true);
                    _tempTileList.Add(_tileArr[x, z]);
                }
            }
        }

        public void DeleteOutline()
        {
            if (_tempTileList.Count < 0) return;

            foreach (Tile tile in _tempTileList)
            {
                tile.ShowOutLine(false);
            }
        }

        public void DeleteTile()
        {
            // TODO : 삭제 방법 바꾸기 (Pool)
            if (_tempTileList.Count < 0) return;

            foreach (Tile tile in _tempTileList)
            {
                tile.IsFull = false;
                tile.gameObject.SetActive(false);
                --_tileCount;
            }
        }
    }
}
