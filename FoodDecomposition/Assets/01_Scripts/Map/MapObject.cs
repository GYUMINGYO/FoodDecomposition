using UnityEngine;

namespace GM.Maps
{
    [RequireComponent(typeof(Outline))]
    public abstract class MapObject : MonoBehaviour
    {
        [Header("Visual Value")]
        public Transform Visual => _visual;
        [SerializeField] private Transform _visual;

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
    }
}
