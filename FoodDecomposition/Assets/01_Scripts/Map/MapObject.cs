using UnityEngine;

namespace GM.Maps
{
    [RequireComponent(typeof(Outline))]
    public abstract class MapObject : MonoBehaviour, IPoolable
    {
        [SerializeField] protected PoolTypeSO _poolType;
        public PoolTypeSO PoolType => _poolType;
        public GameObject GameObject => gameObject;

        [Header("Visual Value")]
        public Transform Visual => _visual;
        [SerializeField] protected Transform _visual;

        public Vector3 Size => _size;
        protected Vector3 _size;

        protected MeshRenderer _meshRenderer;
        protected Outline _outline;

        protected virtual void Awake()
        {
            _outline = GetComponent<Outline>();
            _meshRenderer = _visual.GetComponent<MeshRenderer>();

            ShowOutLine(false);
            _size = _meshRenderer.bounds.size;
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
