using GM.Entities;
using GM.Managers;
using GM.Maps;
using UnityEngine;

namespace GM.Players.Pickers
{
    public class MapPicker : Picker
    {
        [SerializeField] private Map _map;
        [SerializeField] private Grid _grid;
        [SerializeField] private GameObject _cellIndicator;
        [SerializeField] private ObjectInfoSO _mapObjectInfo;

        private EditType _editType;
        private MapObject _mapObject;
        protected MapObject _moveAndRotateObject;
        private Vector3 _cellPosition;
        private Vector3Int _gridPosition;
        private bool _isClick = false;
        private bool _isCellEdit = false;
        private bool _isPick = false;

        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);

            _player.Input.OnMapClickEvent += HandleMapClick;
            _player.Input.OnMapObjectDeleteEvent += HandleMapDelete;
            _player.Input.OnEditTypeChangeEvent += HandleEditTypeChange;
            _player.Input.OnInputTypeChangeEvent += HandleInputTypeChange;
            _player.Input.OnMapDragEvent += HandleMapDrag;
            _player.Input.OnRotateObjectEvent += HandleRotateObject;
        }

        public void SetMapObjectInfo(ObjectInfoSO mapObjectInfo)
        {
            if (mapObjectInfo == null)
            {
                _mapObject = null;
                _cellIndicator.SetActive(true);
                return;
            }

            _mapObjectInfo = mapObjectInfo;
            _mapObject = ManagerHub.Instance.Pool.Pop(_mapObjectInfo.poolType) as MapObject;
            _mapObject.transform.position = new Vector3(_mapObject.transform.position.x, mapObjectInfo.objectSize.y, _mapObject.transform.position.z);
            _mapObject.transform.eulerAngles = new Vector3(0, 180, 0);
            _mapObject.SetTransparentColor(true);
            _cellIndicator.SetActive(false);
            _isCellEdit = false;
        }

        private void OnDestroy()
        {
            _player.Input.OnMapClickEvent -= HandleMapClick;
            _player.Input.OnMapObjectDeleteEvent -= HandleMapDelete;
            _player.Input.OnEditTypeChangeEvent -= HandleEditTypeChange;
            _player.Input.OnInputTypeChangeEvent -= HandleInputTypeChange;
            _player.Input.OnMapDragEvent -= HandleMapDrag;
            _player.Input.OnRotateObjectEvent -= HandleRotateObject;
        }

        private void HandleMapClick(bool isClick)
        {
            _gridPosition = _grid.WorldToCell(_hit.point);
            _isClick = isClick;
            _isPick = false;
            if (_isClick)
            {
                HandlePick();
            }
        }

        private void HandleRotateObject()
        {
            if (_mapObject == null) return;

            _mapObject.RotateObject();
            _moveAndRotateObject?.RotateObject();
        }

        private void HandleMapDrag(bool isDrag)
        {
            if (isDrag)
            {
                HandlePick();
            }
        }

        private void HandleInputTypeChange(InputType type)
        {
            Debug.Log(type);
            if (type != InputType.MapEdit)
            {
                _map.DeleteOutline();
            }
            HandlePick();
            _map.InitDeleteObject();
        }

        private void HandleEditTypeChange(EditType type)
        {
            Debug.Log(type);
            _editType = type;

            if (_editType == EditType.Create)
            {
                _mapObject?.gameObject.SetActive(true);
                _moveAndRotateObject?.gameObject.SetActive(false);
                _isCellEdit = false;
            }
            else if (_editType == EditType.MoveAndRotate)
            {
                _cellIndicator.SetActive(true);
                _mapObject?.gameObject.SetActive(false);
                _moveAndRotateObject?.gameObject.SetActive(true);
                _isCellEdit = true;
            }
            else
            {
                _cellIndicator.SetActive(true);
                _moveAndRotateObject?.gameObject.SetActive(false);
                _mapObject?.gameObject.SetActive(false);
                _isCellEdit = true;
            }
            HandlePick();
            _map.InitDeleteObject();
        }

        private void HandleMapDelete()
        {
            _map.DeleteTile();
        }

        private void LateUpdate()
        {
            PickRaycast();
            if (_isRay == false) return;

            Vector3 mousePosition = _hit.point;
            Vector3Int currentGridPos = _grid.WorldToCell(mousePosition);

            if (_gridPosition != currentGridPos)
            {
                _isPick = false;
                HandlePick();
            }

            _gridPosition = currentGridPos;
            _cellPosition = _grid.CellToWorld(_gridPosition);
            _cellPosition.y = 0.5f;

            if (_mapObject == null ||
                _isCellEdit ||
                _mapObject.IsObjectLock)
            {
                // Cell
                _cellIndicator.transform.position = _cellPosition;
            }
            else
            {
                Vector3 objPos = new Vector3(_cellPosition.x + _mapObjectInfo.objectSize.x, _cellPosition.y, _cellPosition.z + _mapObjectInfo.objectSize.z);
                if (_editType == EditType.MoveAndRotate && _moveAndRotateObject != null) _moveAndRotateObject.transform.position = objPos;
                _mapObject.transform.position = objPos;
            }
        }

        protected override void PickEntity()
        {
            if (_isPick) return;

            _gridPosition = _grid.WorldToCell(_hit.point);

            _isPick = true;
            if (_editType == EditType.Create && _mapObject != null)
            {
                _map.SetMapObject(_mapObject, _gridPosition, _isClick);
            }
            else if (_editType == EditType.Delete)
            {
                _map.DeleteObject(_gridPosition, _isClick);
            }
            else if (_editType == EditType.MoveAndRotate)
            {
                if (_isMoveAndRotateMap)
                {
                    if (_isClick)
                    {
                        if (_map.SetMapObject(ref _moveAndRotateObject, _gridPosition, _isClick))
                        {
                            _isMoveAndRotateMap = false;
                            _cellIndicator.SetActive(true);
                            _isCellEdit = true;
                        }
                    }
                    return;
                }

                _map.DeleteObject(_gridPosition, _isClick, out _moveAndRotateObject);
                if (_moveAndRotateObject != null)
                {
                    _cellIndicator.SetActive(false);
                    _isCellEdit = false;
                    _isMoveAndRotateMap = true;
                }
            }
        }

        private bool _isMoveAndRotateMap;
    }
}
