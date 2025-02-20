using System;
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

        private Vector3 _cellPosition;
        [SerializeField] private Tile _tilePrefab;

        public override void Initialize(Entity entity)
        {
            _pickMapObjectList = new List<MapObject>();
            base.Initialize(entity);
            _player.Input.OnMapClickEvent += HandlePick;
            _player.Input.OnMapDragEvent += HandleDrag;
            _player.Input.OnMapObjectDeleteEvent += HandleMapDelete;
        }

        private void HandleMapDelete()
        {
            if (_pickMapObjectList.Count > 0)
            {
                _pickMapObjectList.ForEach(obj => Destroy(obj.gameObject));
                Debug.Log("맵 삭제");
            }
            _pickMapObjectList.Clear();
        }

        private void HandleDrag(bool isDrag)
        {
            _isDrag = isDrag;
            if (isDrag == true)
            {
                //HandlePick();
            }
        }

        void Update()
        {
            PickRaycast();
            if (_isRay == false) return;

            Vector3 mousePosition = _hit.point;
            Vector3Int gridPosition = _grid.WorldToCell(mousePosition);
            _cellPosition = _grid.CellToWorld(gridPosition);
            _cellIndicator.transform.position = _cellPosition;
        }

        void OnDestroy()
        {
            _player.Input.OnMapClickEvent -= HandlePick;
            _player.Input.OnMapDragEvent -= HandleDrag;
            _player.Input.OnMapObjectDeleteEvent -= HandleMapDelete;
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

            // TODO : 특정 조건일 때 오브젝트가 생성되게 해야 함

            if (_hit.transform.TryGetComponent(out Map map))
            {
                Debug.Log("맵 생성");
                // TODO : Offset 계산
                Vector3 tilePostion = new Vector3(_cellPosition.x + 1, _cellPosition.y + 0.5f, _cellPosition.z + 1);
                Instantiate(_tilePrefab, tilePostion, Quaternion.identity);
            }
            else if (_hit.transform.TryGetComponent(out MapObject mapObject))
            {
                Debug.Log("맵 Outlien");
                if (_pickMapObjectList.Contains(mapObject)) return;
                mapObject.ShowOutLine(true);
                _pickMapObjectList.Add(mapObject);
            }
        }

        private void DeleteOutline()
        {
            if (_isDrag == false)
            {
                if (_pickMapObjectList.Count > 0)
                {
                    _pickMapObjectList.ForEach(obj => obj.ShowOutLine(false));
                }
                _pickMapObjectList.Clear();
            }
        }
    }
}
