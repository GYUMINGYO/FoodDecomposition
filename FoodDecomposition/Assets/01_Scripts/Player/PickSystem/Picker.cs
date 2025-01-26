using GM.Entities;
using UnityEngine;

namespace GM.Players.Pickers
{
    public abstract class Picker : MonoBehaviour, IEntityComponent
    {
        protected Player _player;
        protected RaycastHit _hit;
        protected bool _isRay;
        [SerializeField] protected LayerMask _uiLayer;

        [SerializeField] protected LayerMask _pickLayer;

        public void Initialize(Entity entity)
        {
            _player = entity as Player;
            _player.Input.OnPickEvent += HandlePick;
        }

        protected virtual void HandlePick()
        {
            _isRay = PickRaycast();
            PickEntity();
        }

        protected virtual bool PickRaycast()
        {
            Ray ray = Camera.main.ScreenPointToRay(_player.Input.MousePosition);

            if (Physics.Raycast(ray, out _hit, Camera.main.farClipPlane, _uiLayer))
            {
                return true;
            }

            return Physics.Raycast(ray, out _hit, Camera.main.farClipPlane, _pickLayer);
        }

        protected abstract void PickEntity();

        protected void OnDestroy()
        {
            _player.Input.OnPickEvent -= HandlePick;
        }
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
