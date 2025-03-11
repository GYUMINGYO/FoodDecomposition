using UnityEngine;

namespace GM.Maps
{
    [RequireComponent(typeof(Outline))]
    public abstract class MapObject : MonoBehaviour
    {
        [SerializeField] protected PoolTypeSO _poolType;
        public PoolTypeSO PoolType => _poolType;
        public GameObject GameObject => gameObject;

        [Header("Visual Value")]
        public Transform Visual => _visual;
        [SerializeField] protected Transform _visual;

        protected MeshRenderer _meshRenderer;
        protected Collider _collider;
        protected Outline _outline;

        protected virtual void Awake()
        {
            _outline = GetComponent<Outline>();
            //_meshRenderer = _visual.GetComponentInChildren<MeshRenderer>();
            _collider = GetComponent<Collider>();

            ShowOutLine(false);
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

        public void ShowOutLine(bool enabled)
        {
            _outline.enabled = enabled;
        }

        public void SetUpPool(Pool pool)
        {
            PoolInitalize(pool);
        }

        public void ResetItem()
        {
            ResetPoolItem();
        }

        public abstract void PoolInitalize(Pool pool);
        public abstract void ResetPoolItem();
    }
}
