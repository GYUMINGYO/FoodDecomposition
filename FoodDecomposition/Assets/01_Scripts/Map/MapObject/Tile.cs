using UnityEngine;

namespace GM.Maps
{
    public class Tile : MapObject
    {
        public bool IsFull { get => _isFull; set => _isFull = value; }
        private bool _isFull = false;

        [SerializeField] private Material _black, _white;

        private Collider _collider;

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<Collider>();
        }

        public void SetColor(Color color)
        {
            _meshRenderer.material = color == Color.black ? _black : _white;
        }

        public void ColorTransparent(bool isTransparent)
        {
            Color newColor = _meshRenderer.material.color;
            newColor.a = isTransparent == true ? 0.8f : 1f;
            _meshRenderer.material.color = newColor;
        }

        public void SetCollider(bool isActive)
        {
            _collider.enabled = isActive;
        }
    }
}
