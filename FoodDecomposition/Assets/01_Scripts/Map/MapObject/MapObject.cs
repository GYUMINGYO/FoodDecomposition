using System;
using GM.Managers;
using UnityEngine;

namespace GM.Maps
{
    public enum ObjectDirection
    {
        Down,
        Left,
        Up,
        Right
    }

    [RequireComponent(typeof(Outline))]
    public abstract class MapObject : MonoBehaviour, IPoolable
    {
        public event Action OnDestoryObjectEvent;

        [Header("Setting Value")]
        public ObjectInfoSO Info => _info;
        [SerializeField] protected ObjectInfoSO _info;
        public PoolTypeSO PoolType => _poolType;
        [SerializeField] protected PoolTypeSO _poolType;
        public GameObject GameObject => gameObject;

        [Header("Visual Value")]
        public Transform Visual => _visual;
        [SerializeField] protected Transform _visual;

        public ObjectDirection ObjDirection => _objectDirection;
        protected ObjectDirection _objectDirection = ObjectDirection.Down;

        public bool IsObjectLock { get => _isObjectLock; set => _isObjectLock = value; }
        protected bool _isObjectLock = false;

        protected MeshRenderer[] _meshRenderers;
        protected Color[] _originColors;
        protected Collider _collider;
        protected Outline _outline;

        protected virtual void Awake()
        {
            _outline = GetComponent<Outline>();
            _meshRenderers = _visual.GetComponentsInChildren<MeshRenderer>();
            _collider = GetComponent<Collider>();

            _originColors = new Color[_meshRenderers.Length];
            for (int i = 0; i < _meshRenderers.Length; ++i)
            {
                _originColors[i] = _meshRenderers[i].material.color;
            }

            ShowOutLine(false);
        }

        //! Must be called when destory object
        public void DestoryObject()
        {
            OnDestoryObjectEvent?.Invoke();
            OnDestoryObjectEvent = null;
            ManagerHub.Instance.Pool.Push(this);
        }

        /// <summary>
        /// Change Current Meterial Transparent
        /// </summary>
        /// <param name="isTransparent"></param>
        /// <param name="transparencyValue"></param>
        public void SetTransparentColor(bool isTransparent = false, float transparencyValue = 0.8f)
        {
            if (_originColors.Length <= 0) return;

            for (int i = 0; i < _meshRenderers.Length; ++i)
            {
                Color newColor = _meshRenderers[i].material.color;
                newColor.a = isTransparent ? transparencyValue : 1f;
                _meshRenderers[i].material.color = newColor;
            }
        }

        public void SetColor(bool isMoveAndRotate = false, bool isDestory = false)
        {
            for (int i = 0; i < _meshRenderers.Length; ++i)
            {
                Color newColor;
                if (isMoveAndRotate) newColor = Color.green;
                else if (isDestory) newColor = Color.red;
                else newColor = _originColors[i];
                _meshRenderers[i].material.color = newColor;
            }
        }

        public void SetCollider(bool isActive)
        {
            _collider.enabled = isActive;
        }

        public void ShowOutLine(bool enabled)
        {
            _outline.enabled = enabled;
        }

        public void RotateObject()
        {
            if (_isObjectLock) return;

            transform.Rotate(0f, 90f, 0f);

            // TODO : Uility로 enum 함수 분리하기(재사용성 up, 가독성 up)
            _objectDirection = (ObjectDirection)(((int)_objectDirection + 1) % Enum.GetValues(typeof(ObjectDirection)).Length);
        }

        /// <summary>
        /// Limit Y angle to 360 degrees with int value
        /// </summary>
        /// <returns></returns>
        public int GetNormalizedRotationY()
        {
            float angle = transform.localEulerAngles.y;
            angle = angle % 360;
            if (angle >= 180)
                angle -= 360;
            return (int)angle;
        }

        #region Pool

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

        #endregion
    }
}
