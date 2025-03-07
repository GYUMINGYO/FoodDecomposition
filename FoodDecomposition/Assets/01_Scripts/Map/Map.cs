using System.Collections.Generic;
using GM.Managers;
using Unity.AI.Navigation;
using UnityEngine;

namespace GM.Maps
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private Grid _grid;

        private NavMeshSurface _navMeshSurface;
        private Tile[,] _tileArr;
        private List<Tile> _tempTileList;

        public uint CurrentTileCount => _tileCount;
        private uint _tileCount = 0;

        private void Awake()
        {
            _navMeshSurface = GetComponent<NavMeshSurface>();
            _tileArr = new Tile[3000, 3000];
            _tempTileList = new List<Tile>();
        }

        // TODO : 구조 개선
        // 지금은 그냥 타일 단일 구조임

        //! 지금은 유저가 직접 사용하지 않음 (유저가 마음대로 맵을 넓히는게 아님) / 돈을 주면 특정 부분이 확장되는 방식으로 변경함

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
                        _tileArr[x, z].ShowOutLine(true);
                        _tileArr[x, z].ColorTransparent(true);
                        tempTileList.Add(_tileArr[x, z]);
                        continue;
                    }

                    Vector3 mapObjPosition = _grid.CellToWorld(new Vector3Int(x, 0, z));
                    mapObjPosition.x += mapObjectInfo.objectSize.x;
                    mapObjPosition.y += mapObjectInfo.objectSize.y;
                    mapObjPosition.z += mapObjectInfo.objectSize.z;

                    Tile tile = ManagerHub.Instance.Pool.Pop(mapObjectInfo.poolType) as Tile;
                    tile.transform.position = mapObjPosition;
                    tile.transform.SetParent(transform);

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
            _navMeshSurface.BuildNavMesh();
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
                tile.ShowOutLine(false);
                --_tileCount;
            }

            _navMeshSurface.BuildNavMesh();
        }
    }
}
