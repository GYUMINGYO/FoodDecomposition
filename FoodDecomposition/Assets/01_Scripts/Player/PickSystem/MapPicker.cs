using System;
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
        private Vector3 _cellPosition;
        private Vector3Int _gridPosition;
        private Vector3Int _startDragPosition;
        private bool _isClick = false;
        private bool _isMoveCell = false;
        private bool _isObjectSet = false;

        // TODO : 특정 칸이 설치가 안되는 칸이면 빨간색으로 표시하기

        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);

            _player.Input.OnMapClickEvent += HandleMapClick;
            _player.Input.OnMapObjectDeleteEvent += HandleMapDelete;
            _player.Input.OnEditTypeChangeEvent += HandleEditTypeChange;
            _player.Input.OnInputTypeChangeEvent += HandleInputTypeChange;
            _player.Input.OnMapDragEvent += HandleMapDrag;
            _player.Input.OnRotateObjectEvent += HnadleRotateObject;
        }

        private void OnDestroy()
        {
            _player.Input.OnMapClickEvent -= HandleMapClick;
            _player.Input.OnMapObjectDeleteEvent -= HandleMapDelete;
            _player.Input.OnEditTypeChangeEvent -= HandleEditTypeChange;
            _player.Input.OnInputTypeChangeEvent -= HandleInputTypeChange;
            _player.Input.OnMapDragEvent -= HandleMapDrag;
            _player.Input.OnRotateObjectEvent -= HnadleRotateObject;
        }

        private void HnadleRotateObject()
        {
            if (_mapObject == null) return;

            var mapObjectRot = _mapObject.transform.eulerAngles;
            mapObjectRot.y += 90f;
            _mapObject.transform.eulerAngles = mapObjectRot;
        }

        public void SetMapObject(ObjectInfoSO mapObjectInfo)
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
            _mapObject.ColorTransparent(true);
            _cellIndicator.SetActive(false);
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
        }

        private void HandleEditTypeChange(EditType type)
        {
            Debug.Log(type);
            _editType = type;
        }

        private void HandleMapDelete()
        {
            _map.DeleteTile();
        }

        private void Update()
        {
            PickRaycast();
            if (_isRay == false) return;

            Vector3 mousePosition = _hit.point;
            Vector3Int currentGridPos = _grid.WorldToCell(mousePosition);
            if (_map.GetTileObject(currentGridPos) == null)
            {
                //_cellIndicator.SetActive(false);
            }

            if (_gridPosition != currentGridPos) _isMoveCell = true;

            _gridPosition = currentGridPos;
            _cellPosition = _grid.CellToWorld(_gridPosition);
            _cellPosition.y = 0.5f;

            if (_mapObject == null)
            {
                // Cell
                _cellIndicator.transform.position = _cellPosition;
            }
            else
            {
                if (_isObjectSet == false)
                {
                    Vector3 objPos = new Vector3(_cellPosition.x + _mapObjectInfo.objectSize.x, _cellPosition.y, _cellPosition.z + _mapObjectInfo.objectSize.z);
                    // MapObject
                    _mapObject.transform.position = objPos;
                }
            }
        }

        private void HandleMapClick(bool isClick)
        {
            _gridPosition = _grid.WorldToCell(_hit.point);
            _isClick = isClick;
            if (_isClick)
            {
                _startDragPosition = _gridPosition;
                HandlePick();
            }
            else
            {
                // Real Set Tile
                if (_editType == EditType.Delete) return;

                _map.SetTileObject();
            }
        }

        protected override void PickEntity()
        {
            if (_isMoveCell == false) return;

            if (_hit.transform.CompareTag("Tile") && _mapObject != null)
            {
                _gridPosition = _grid.WorldToCell(_hit.point);
                if (_editType == EditType.Create)
                {
                    _isObjectSet = true;
                    _map.SetMapObject(_mapObjectInfo, _mapObject, _gridPosition, _isClick);
                    _isObjectSet = false;
                }
                else if (_editType == EditType.Delete)
                {
                    _map.DeleteObject(_gridPosition, _isClick);
                }

                _isMoveCell = false;
            }
        }
    }
}
