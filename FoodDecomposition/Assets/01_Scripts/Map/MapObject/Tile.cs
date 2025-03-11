using UnityEngine;

namespace GM.Maps
{
    public class Tile : MapObject
    {
        public bool IsFull { get => _isFull; set => _isFull = value; }
        private bool _isFull = false;

        [SerializeField] private Material _black, _white;

        protected override void Awake()
        {
            base.Awake();
        }

        public void SetColor(Color color)
        {
            _meshRenderer.material = color == Color.black ? _black : _white;
        }

        public override void PoolInitalize(Pool pool)
        {
        }

        public override void ResetPoolItem()
        {
            _isFull = false;
            ColorTransparent(true);
            SetCollider(false);
        }
    }
}
