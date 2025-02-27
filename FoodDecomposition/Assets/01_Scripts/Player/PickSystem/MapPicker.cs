using System;
using System.Collections.Generic;
using GM.Entities;
using GM.Maps;
using UnityEngine;

namespace GM.Players.Pickers
{
    public class MapPicker : Picker
    {
        private List<Vector3Int> _pickMapObjectList;
        private bool _isDrag;

        [SerializeField] private Map _map;
        [SerializeField] private Grid _grid;
        [SerializeField] private GameObject _cellIndicator;

        private Vector3 _cellPosition;
        private Vector3Int _gridPosition;
        [SerializeField] private ObjectInfoSO _mapObjectInfo;

        private bool _isCreateMode = true;

        public override void Initialize(Entity entity)
        {
            _pickMapObjectList = new List<Vector3Int>();
            base.Initialize(entity);

            _player.Input.OnMapClickEvent += HandlePick;
            _player.Input.OnMapDragEvent += HandleDrag;
            _player.Input.OnMapObjectDeleteEvent += HandleMapDelete;
            _player.Input.OnEditTypeChangeEvent += HandleEditTypeChange;
            _player.Input.OnInputTypeChangeEvent += HandleInputTypeChange;
        }

        private void HandleInputTypeChange(InputType type)
        {
            if (type != InputType.MapEdit)
            {
                DeleteOutline();
            }
        }
        private void HandleEditTypeChange(EditType type)
        {
            if (type == EditType.Delete)
            {
                _isCreateMode = false;
            }
            else
            {
                // Create Mode
                _isCreateMode = true;
            }
        }

        private void HandleMapDelete()
        {
            if (_pickMapObjectList.Count > 0 && _isCreateMode == false)
            {
                _map.DeleteTileObjects(_pickMapObjectList);
            }
            _pickMapObjectList.Clear();
        }

        private void HandleDrag(bool isDrag)
        {
            _isDrag = isDrag;
            if (_isDrag == true)
            {
                HandlePick();
            }
        }

        private void Update()
        {
            PickRaycast();
            if (_isRay == false) return;

            Vector3 mousePosition = _hit.point;
            _gridPosition = _grid.WorldToCell(mousePosition);
            _cellPosition = _grid.CellToWorld(_gridPosition);
            _cellIndicator.transform.position = _cellPosition;
        }

        private void OnDestroy()
        {
            _player.Input.OnMapClickEvent -= HandlePick;
            _player.Input.OnMapDragEvent -= HandleDrag;
            _player.Input.OnMapObjectDeleteEvent -= HandleMapDelete;
            _player.Input.OnEditTypeChangeEvent -= HandleEditTypeChange;
            _player.Input.OnInputTypeChangeEvent -= HandleInputTypeChange;
        }

        protected override void PickEntity()
        {
            // TODO : Drag 네모 만들어서 처리하기
            if (_isRay == false)
            {
                if (_isDrag == false)
                {
                    DeleteOutline();
                }
                return;
            }

            if (_hit.transform.GetComponent<Map>() && _isCreateMode)
            {
                DeleteOutline();
                _map.SetTileObject(_mapObjectInfo, _gridPosition);
                _pickMapObjectList.Add(_gridPosition);
            }
            else if (_hit.transform.TryGetComponent(out MapObject mapObject) && _isCreateMode == false)
            {
                _gridPosition = _grid.WorldToCell(_hit.point);
                if (_pickMapObjectList.Contains(_gridPosition)) return;
                mapObject.ShowOutLine(true);
                _pickMapObjectList.Add(_gridPosition);
            }
        }

        private void DeleteOutline()
        {
            if (_isDrag == false)
            {
                if (_pickMapObjectList.Count > 0)
                {
                    foreach (MapObject mapObject in _map.GetTileObjects(_pickMapObjectList))
                    {
                        if (mapObject == null) continue;

                        mapObject.ShowOutLine(false);
                    }
                }
                _pickMapObjectList.Clear();
            }
        }
    }
}
