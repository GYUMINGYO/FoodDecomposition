using System.Collections.Generic;
using GM.Entities;
using GM.Maps;
using UnityEngine;

namespace GM.Players.Pickers
{
    public class MapPicker : Picker
    {
        private List<MapObject> _pickMapObjectList;
        private bool _isDrag;

        [SerializeField] private Grid _grid;
        [SerializeField] private GameObject _cellIndicator;

        public override void Initialize(Entity entity)
        {
            _pickMapObjectList = new List<MapObject>();
            base.Initialize(entity);
            _player.Input.OnMapClickEvent += HandlePick;
            _player.Input.OnMapDragEvent += HandleDrag;
        }

        private void HandleDrag(bool isDrag)
        {
            _isDrag = isDrag;
            if (isDrag == true)
            {
                HandlePick();
            }
        }

        void Update()
        {
            PickRaycast();
            if (_isRay == false) return;

            Vector3 mousePosition = _hit.point;
            Vector3Int gridPosition = _grid.WorldToCell(mousePosition);
            _cellIndicator.transform.position = _grid.CellToWorld(gridPosition);
        }

        void OnDestroy()
        {
            _player.Input.OnMapClickEvent -= HandlePick;
            _player.Input.OnMapDragEvent -= HandleDrag;
        }

        protected override void PickEntity()
        {
            // TODO : Drag 네모 만들어서 처리하기
            if (_isRay == false)
            {
                if (_isDrag == false)
                {
                    if (_pickMapObjectList.Count > 0)
                    {
                        _pickMapObjectList.ForEach(obj => obj.ShowOutLine(false));
                    }
                    _pickMapObjectList.Clear();
                }
                return;
            }

            if (_hit.transform.TryGetComponent(out MapObject mapObject))
            {
                if (_pickMapObjectList.Contains(mapObject)) return;
                mapObject.ShowOutLine(true);
                _pickMapObjectList.Add(mapObject);
            }
        }
    }
}
