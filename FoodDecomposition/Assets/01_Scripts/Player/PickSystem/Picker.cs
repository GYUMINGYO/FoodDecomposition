using System;
using GM.Entities;
using GM.GameEventSystem;
using UnityEngine;

namespace GM.Players.Pickers
{
    public abstract class Picker : MonoBehaviour, IEntityComponent
    {
        [SerializeField] protected GameEventChannelSO _uiDescriptionEventChannel;
        [SerializeField] protected LayerMask _pickLayer;

        protected Player _player;
        protected RaycastHit _hit;
        protected bool _isRay;

        private bool _uiState;

        public void Initialize(Entity entity)
        {
            _player = entity as Player;
            _player.Input.OnPickEvent += HandlePick;
            _uiDescriptionEventChannel.AddListener<UIDescriptionEvent>(HandleUIEvent);
        }

        protected void OnDestroy()
        {
            _player.Input.OnPickEvent -= HandlePick;
            _uiDescriptionEventChannel.RemoveListener<UIDescriptionEvent>(HandleUIEvent);
        }

        private void HandleUIEvent(UIDescriptionEvent evt)
        {
            _uiState = evt.UIState;
        }

        protected virtual void HandlePick()
        {
            if (_uiState) return;

            _isRay = PickRaycast();
            PickEntity();
        }

        protected virtual bool PickRaycast()
        {
            Ray ray = Camera.main.ScreenPointToRay(_player.Input.MousePosition);
            return Physics.Raycast(ray, out _hit, Camera.main.farClipPlane, _pickLayer);
        }

        protected abstract void PickEntity();

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            if (_player == null) return;
            Gizmos.DrawRay(Camera.main.ScreenPointToRay(_player.Input.MousePosition));
        }
    }
#endif
}
