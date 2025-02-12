using UnityEngine;

namespace GM.Maps
{
    public abstract class MapObject : MonoBehaviour
    {
        [Header("Visual Value")]
        public Transform Visual => _visual;
        [SerializeField] private Transform _visual;

        public Vector3 Size => _size;
        protected Vector3 _size;

        protected MeshRenderer _meshRenderer;

        protected void Awake()
        {
            _meshRenderer = _visual.GetComponent<MeshRenderer>();
            _size = _meshRenderer.bounds.size;
        }

        public void OutLine()
        {

        }
    }
}
