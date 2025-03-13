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

        public void SetMapObject(MapObject mapObject, Vector3Int position, bool isInstallation)
        {
            mapObject.IsObjectLock = true;

            int sizeX = mapObject.Info.cellSize.x;
            int sizeZ = mapObject.Info.cellSize.y;
            Vector2Int signVec = Vector2Int.one;
            signVec.x = (mapObject.ObjDirection == ObjectDirection.Left) ||
                        (mapObject.ObjDirection == ObjectDirection.Up) ? -1 : 1;

            signVec.y = (mapObject.ObjDirection == ObjectDirection.Left) ||
                        (mapObject.ObjDirection == ObjectDirection.Down) ? -1 : 1;

            // Calculate Direction
            if (Mathf.Abs(mapObject.GetNormalizedRotationY()) == 90)
            {
                int temp = sizeX;
                sizeX = sizeZ;
                sizeZ = temp;
            }

            // Check if it can Install is possible
            for (int x = 0; x < sizeX; ++x)
            {
                for (int z = 0; z < sizeZ; ++z)
                {
                    int posX = position.x + (x * signVec.x);
                    int posZ = position.z + (z * signVec.y);
                    if (_mapArray[posX, posZ].tile == null || _mapArray[posX, posZ].mapObject != null)
                    {
                        mapObject.SetColor(false, true);
                        mapObject.IsObjectLock = false;
                        return;
                    }
                }
            }

            mapObject.SetColor();

            // Installation
            if (isInstallation)
            {
                Vector3 installObjectPos = _grid.CellToWorld(new Vector3Int(position.x, 0, position.z));
                installObjectPos.x += mapObject.Info.objectSize.x;
                installObjectPos.y += mapObject.transform.position.y;
                installObjectPos.z += mapObject.Info.objectSize.z;

                MapObject installMapObject = ManagerHub.Instance.Pool.Pop(mapObject.Info.poolType) as MapObject;
                installMapObject.name = mapObject.Info.name;
                installMapObject.transform.position = installObjectPos;
                installMapObject.transform.rotation = mapObject.transform.rotation;

                for (int x = 0; x < sizeX; ++x)
                {
                    for (int z = 0; z < sizeZ; ++z)
                    {
                        int posX = position.x + (x * signVec.x);
                        int posZ = position.z + (z * signVec.y);
                        _mapArray[posX, posZ].SetMapObject(installMapObject);
                    }
                }
                installMapObject.transform.SetParent(_mapObjectsTrm);
            }

            mapObject.IsObjectLock = false;
        }

        public bool SetMapObject(ref MapObject mapObject, Vector3Int position, bool isInstallation)
        {
            int sizeX = mapObject.Info.cellSize.x;
            int sizeZ = mapObject.Info.cellSize.y;
            Vector2Int signVec = Vector2Int.one;
            signVec.x = (mapObject.ObjDirection == ObjectDirection.Left) ||
                        (mapObject.ObjDirection == ObjectDirection.Up) ? -1 : 1;

            signVec.y = (mapObject.ObjDirection == ObjectDirection.Left) ||
                        (mapObject.ObjDirection == ObjectDirection.Down) ? -1 : 1;

            // Calculate Direction
            if (Mathf.Abs(mapObject.GetNormalizedRotationY()) == 90)
            {
                int temp = sizeX;
                sizeX = sizeZ;
                sizeZ = temp;
            }

            // Check if it can Install is possible
            for (int x = 0; x < sizeX; ++x)
            {
                for (int z = 0; z < sizeZ; ++z)
                {
                    int posX = position.x + (x * signVec.x);
                    int posZ = position.z + (z * signVec.y);
                    if (_mapArray[posX, posZ].tile == null || _mapArray[posX, posZ].mapObject != null)
                    {
                        mapObject.SetColor(false, true);
                        mapObject.IsObjectLock = false;
                        return false;
                    }
                }
            }

            mapObject.SetColor();

            // Installation
            if (isInstallation)
            {
                Vector3 installObjectPos = _grid.CellToWorld(new Vector3Int(position.x, 0, position.z));
                installObjectPos.x += mapObject.Info.objectSize.x;
                installObjectPos.y += mapObject.transform.position.y;
                installObjectPos.z += mapObject.Info.objectSize.z;

                MapObject installMapObject = ManagerHub.Instance.Pool.Pop(mapObject.Info.poolType) as MapObject;
                installMapObject.name = mapObject.Info.name;
                installMapObject.transform.position = installObjectPos;
                installMapObject.transform.rotation = mapObject.transform.rotation;

                for (int x = 0; x < sizeX; ++x)
                {
                    for (int z = 0; z < sizeZ; ++z)
                    {
                        int posX = position.x + (x * signVec.x);
                        int posZ = position.z + (z * signVec.y);
                        _mapArray[posX, posZ].SetMapObject(installMapObject);
                    }
                }
                installMapObject.transform.SetParent(_mapObjectsTrm);
            }

            mapObject.IsObjectLock = false;

            ManagerHub.Instance.Pool.Push(mapObject);
            mapObject = null;
            return true;
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
                        _mapArray[x, z].tile.SetTransparentColor(true);
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
                    tile.SetTransparentColor(true);
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
                tile.SetTransparentColor(false);
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

        public void InitDeleteObject()
        {
            if (_beforeDeleteObject != null)
            {
                _beforeDeleteObject.SetTransparentColor();
                _beforeDeleteObject.SetColor();
            }
        }

        public void DeleteObject(Vector3Int position, bool isConfirmed)
        {
            InitDeleteObject();

            if (_mapArray[position.x, position.z].tile == null || _mapArray[position.x, position.z].mapObject == null) return;

            MapObject currentDeleteObject = _mapArray[position.x, position.z].mapObject;
            currentDeleteObject.SetColor(false, true);
            currentDeleteObject.SetTransparentColor(true, 0.9f);
            _beforeDeleteObject = currentDeleteObject;

            if (isConfirmed)
            {
                _mapArray[position.x, position.z].mapObject.DestoryObject();
            }
        }

        public void DeleteObject(Vector3Int position, bool isConfirmed, out MapObject destoryObject)
        {
            InitDeleteObject();
            destoryObject = null;

            if (_mapArray[position.x, position.z].tile == null || _mapArray[position.x, position.z].mapObject == null) return;

            MapObject currentDeleteObject = _mapArray[position.x, position.z].mapObject;
            currentDeleteObject.SetColor(true, false);
            currentDeleteObject.SetTransparentColor(true, 0.9f);
            _beforeDeleteObject = currentDeleteObject;

            if (isConfirmed)
            {
                destoryObject = _mapArray[position.x, position.z].mapObject;
                destoryObject = ManagerHub.Instance.Pool.Pop(destoryObject.Info.poolType) as MapObject;
                _mapArray[position.x, position.z].mapObject.DestoryObject();
            }
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
