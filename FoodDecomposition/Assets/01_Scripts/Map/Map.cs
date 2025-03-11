using System.Collections.Generic;
using GM.Managers;
using Unity.AI.Navigation;
using UnityEngine;

namespace GM.Maps
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private Grid _grid;
        [SerializeField] private Transform _tilesTrm;
        [SerializeField] private Transform _mapObjectsTrm;

        private MapArray[,] _mapArray;
        private NavMeshSurface _navMeshSurface;
        private List<Tile> _tempTileList;
        private MapObject _beforeDeleteObject;

        private void Awake()
        {
            _navMeshSurface = GetComponent<NavMeshSurface>();
            _mapArray = new MapArray[3000, 3000];
            for (int x = 0; x < 3000; x++)
            {
                for (int z = 0; z < 3000; z++)
                {
                    _mapArray[x, z] = new MapArray();
                }
            }
            _tempTileList = new List<Tile>();
        }

        //! 지금은 유저가 직접 사용하지 않음 (유저가 마음대로 맵을 넓히는게 아님) / 돈을 주면 특정 부분이 확장되는 방식으로 변경함

        public MapObject GetMapObject(Vector3Int position) => _mapArray[position.x, position.z].mapObject;
        public Tile GetTileObject(Vector3Int position) => _mapArray[position.x, position.z].tile;

        public List<Tile> GetTileObjects(List<Vector3Int> positionList)
        {
            List<Tile> mapObjects = new List<Tile>();
            foreach (Vector3Int position in positionList)
            {
                if (_mapArray[position.x, position.z] == null) continue;

                mapObjects.Add(_mapArray[position.x, position.z].tile);
            }
            return mapObjects;
        }

        public void SetMapObject(ObjectInfoSO mapObjectInfo, MapObject mapObject, Vector3Int position, bool isInstallation)
        {
            if (_mapArray[position.x, position.z].tile == null || _mapArray[position.x, position.z].mapObject != null)
            {
                // TODO : Object Red 처리
                return;
            }

            // Installation
            if (isInstallation)
            {
                MapObject installMapObject = ManagerHub.Instance.Pool.Pop(mapObjectInfo.poolType) as MapObject;
                installMapObject.transform.position = mapObject.transform.position;
                installMapObject.transform.rotation = mapObject.transform.rotation;
                _mapArray[position.x, position.z].mapObject = installMapObject;
                installMapObject.transform.SetParent(_mapObjectsTrm);
            }
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
                    if (_mapArray[x, z].tile != null)
                    {
                        if (_mapArray[x, z].tile.IsFull) continue;

                        _mapArray[x, z].tile.gameObject.SetActive(true);
                        _mapArray[x, z].tile.ShowOutLine(true);
                        _mapArray[x, z].tile.ColorTransparent(true);
                        tempTileList.Add(_mapArray[x, z].tile);
                        continue;
                    }

                    Vector3 mapObjPosition = _grid.CellToWorld(new Vector3Int(x, 0, z));
                    mapObjPosition.x += mapObjectInfo.objectSize.x;
                    mapObjPosition.y += mapObjectInfo.objectSize.y;
                    mapObjPosition.z += mapObjectInfo.objectSize.z;

                    Tile tile = ManagerHub.Instance.Pool.Pop(mapObjectInfo.poolType) as Tile;
                    tile.transform.position = mapObjPosition;
                    tile.transform.SetParent(_tilesTrm);

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
                    _mapArray[x, z].tile = tile;
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
                    if (_mapArray[x, z] == null) continue;

                    _mapArray[x, z].tile.ShowOutLine(true);
                    _tempTileList.Add(_mapArray[x, z].tile);
                }
            }
        }

        public void DeleteObject(Vector3Int position, bool isTemp)
        {
            if (_beforeDeleteObject != null)
            {
                // TODO : 오브젝트 빨간색으로 삭제 예정 표시 삭제하기
            }

            if (_mapArray[position.x, position.z].tile == null || _mapArray[position.x, position.z].mapObject == null) return;

            // TODO : 오브젝트 빨간색으로 삭제 예정 표시하기

            if (!isTemp) return;

            //ManagerHub.Instance.Pool.Push(_mapArray[position.x, position.z].mapObject);
            _mapArray[position.x, position.z].mapObject = null;
        }

        public void DeleteTile()
        {
            if (_tempTileList.Count < 0) return;

            foreach (Tile tile in _tempTileList)
            {
                tile.IsFull = false;
                tile.gameObject.SetActive(false);
                tile.ShowOutLine(false);
            }

            _navMeshSurface.BuildNavMesh();
        }

        public void DeleteOutline()
        {
            if (_tempTileList.Count <= 0) return;

            foreach (Tile tile in _tempTileList)
            {
                tile.ShowOutLine(false);
            }
        }
    }
}
