using GM.Entities;
using UnityEngine;

namespace GM.Players.Pickers
{
    public abstract class Picker : MonoBehaviour, IEntityComponent
    {
        protected Player _player;

        [SerializeField] protected LayerMask _pickLayer;

        protected RaycastHit _hit;

        public void Initialize(Entity entity)
        {
            _player = entity as Player;
            _player.Input.OnPickEvent += HandlePick;
        }

        protected virtual void HandlePick()
        {
            if (PickRaycast() == false) return;
            PickEntity();
        }

        protected virtual bool PickRaycast()
        {
            Ray ray = Camera.main.ScreenPointToRay(_player.Input.MousePosition);
            return Physics.Raycast(ray, out _hit, Camera.main.farClipPlane, _pickLayer);
        }

        protected abstract void PickEntity();

        protected void OnDestroy()
        {
            _player.Input.OnPickEvent -= HandlePick;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(Camera.main.ScreenPointToRay(_player.Input.MousePosition));
        }
    }
}
