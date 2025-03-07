using GM.Entities;
using GM.Maps;
using UnityEngine;

namespace GM.Players.Pickers
{
    public class MapPicker : Picker
    {
        [SerializeField] private Map _map;
        [SerializeField] private Grid _grid;
        [SerializeField] private GameObject _cellIndicator;

        private Vector3 _cellPosition;
        private Vector3Int _gridPosition;
        [SerializeField] private ObjectInfoSO _mapObjectInfo;


        private EditType editType;
        private bool _isMoveCell = false;

        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);

            _player.Input.OnMapClickEvent += HandleMapClick;
            _player.Input.OnMapObjectDeleteEvent += HandleMapDelete;
            _player.Input.OnEditTypeChangeEvent += HandleEditTypeChange;
            _player.Input.OnInputTypeChangeEvent += HandleInputTypeChange;
            _player.Input.OnMapDragEvent += HandleMapDrag;
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
            if (type != InputType.MapEdit)
            {
                _map.DeleteOutline();
            }
        }

        private void HandleEditTypeChange(EditType type)
        {
            editType = type;
        }

        private void HandleMapDelete()
        {
            _map.DeleteTile();
        }

        private Vector3Int _startDragPosition;

        private void Update()
        {
            PickRaycast();
            if (_isRay == false) return;

            Vector3 mousePosition = _hit.point;
            Vector3Int currentGridPos = _grid.WorldToCell(mousePosition);
            if (_gridPosition != currentGridPos) _isMoveCell = true;
            _gridPosition = currentGridPos;
            _cellPosition = _grid.CellToWorld(_gridPosition);
            _cellPosition.y = 0.5f;
            _cellIndicator.transform.position = _cellPosition;
        }

        private void OnDestroy()
        {
            _player.Input.OnMapClickEvent -= HandleMapClick;
            _player.Input.OnMapObjectDeleteEvent -= HandleMapDelete;
            _player.Input.OnEditTypeChangeEvent -= HandleEditTypeChange;
            _player.Input.OnInputTypeChangeEvent -= HandleInputTypeChange;
            _player.Input.OnMapDragEvent -= HandleMapDrag;
        }

        private bool _isClick = false;

        private void HandleMapClick(bool isClick)
        {
            _isClick = isClick;

            _gridPosition = _grid.WorldToCell(_hit.point);
            if (_isClick)
            {
                _startDragPosition = _gridPosition;
                HandlePick();
            }
            else
            {
                // Real Set Tile
                if (editType == EditType.Delete) return;

                _map.SetTileObject();
            }
        }

        protected override void PickEntity()
        {
            // TODO : Drag 네모 만들어서 처리하기

            if (_isMoveCell == false) return;

            if (_hit.transform.gameObject.layer == LayerMask.NameToLayer("Map"))
            {
                if (editType == EditType.Create)
                {
                    // Temporay Tile
                    _map.SetTemporaryTile(_mapObjectInfo, _startDragPosition, _gridPosition);
                }
                else
                {
                    _map.TileShowOutlie(_startDragPosition, _gridPosition);
                }

                _isMoveCell = false;
            }
            else if (_hit.transform.gameObject.layer == LayerMask.NameToLayer("MapObject"))
            {
                if (editType == EditType.Delete)

                {
                    _map.TileShowOutlie(_startDragPosition, _gridPosition);

                }
                else
                {
                    _map.SetTemporaryTile(_mapObjectInfo, _startDragPosition, _gridPosition);
                }

                _isMoveCell = false;
            }
        }
    }
}
