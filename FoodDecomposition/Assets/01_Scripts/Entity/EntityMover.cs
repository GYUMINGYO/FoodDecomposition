using System;
using RPG.StatSystem;
using UnityEngine;

namespace RPG.Entities
{
    public class EntityMover : MonoBehaviour, IEntityComponent, IAfterInitable
    {
        [Header("Move stats")] 
        [SerializeField] private StatSO _moveStat;
        [SerializeField] private float _moveSpeed = 5f;
        
        [SerializeField] private Transform _groundChecker, _wallChacker;
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private float _groundCheckWidth, _checkDistance, _wallCheckWidth;
        
        [field: SerializeField] public bool IsGrounded { get; private set; }
        public event Action<bool> OnGroundStateChanged;

        public Vector2 Velocity => _rbCompo.linearVelocity;
        public bool CanManualMove { get; set; } = true; // 키보드로 움직임 가능
        public float SpeedMultiplier { get; set; } = 1f;
        public float LimitYSpeed { get; set; } = 40f;
        
        private float _originalGravity;
        
        private Rigidbody2D _rbCompo;
        private Entity _entity;
        private EntityRenderer _renderer;
        private EntityStat _stat;
        
        private float _xMovement;
        
        private Collider2D _collider;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _rbCompo = entity.GetComponent<Rigidbody2D>();
            _renderer = entity.GetCompo<EntityRenderer>();
            _stat = entity.GetCompo<EntityStat>();
            
            _originalGravity = _rbCompo.gravityScale;

            _collider = entity.GetComponent<Collider2D>();
        }
        
        public void AfterInit()
        {
            _stat.MoveSpeedStat.OnValueChange += HandleMoveSpeedChange;
            _moveSpeed = _stat.MoveSpeedStat.Value;
        }

        private void OnDestroy()
        {
            _stat.MoveSpeedStat.OnValueChange -= HandleMoveSpeedChange;
        }

        private void HandleMoveSpeedChange(StatSO stat, float current, float previous)
        {
            _moveSpeed = current;
        }

        public void AddForceToEntity(Vector2 force, ForceMode2D mode = ForceMode2D.Impulse)
        {
            _rbCompo.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }

        public void StopImmediately(bool isYAxisToo = false)
        {
            if (isYAxisToo)
                _rbCompo.linearVelocity = Vector2.zero;
            else
                _rbCompo.linearVelocityX = 0;
            _xMovement = 0;
        }

        public void SetMovement(float xMovement) => _xMovement = xMovement;
        
        private void FixedUpdate()
        {
            CheckGround();
            MoveCharacter();

            _rbCompo.linearVelocityY = Mathf.Clamp(_rbCompo.linearVelocityY, -LimitYSpeed, +LimitYSpeed);
        }

        private void CheckGround()
        {
            bool before = IsGrounded;
            Vector2 boxSize = new Vector2(_groundCheckWidth, 0.05f);
            IsGrounded = Physics2D.BoxCast(_groundChecker.position, boxSize, 0f,
                Vector2.down, _checkDistance, _whatIsGround);

            if (IsGrounded != before)
            {
                OnGroundStateChanged?.Invoke(IsGrounded);
            }
        }
        
        private void MoveCharacter()
        {
            if(CanManualMove)
                _rbCompo.linearVelocityX = _xMovement * _moveSpeed * SpeedMultiplier;
            
            _renderer.FlipController(_xMovement);
        }

        public void SetGravityMultiplier(float value) => _rbCompo.gravityScale = _originalGravity * value;

        public bool IsWallDetected() => Physics2D.Raycast(_wallChacker.position,
            Vector2.right * _renderer.FacingDirection, _wallCheckWidth, _whatIsGround);

        public bool CheckColliderInFront(Vector2 direction, ref float distance)
        {
            Vector2 center = _collider.bounds.center;
            Vector2 size = _collider.bounds.size;
            size.y -= 0.3f; // 안 빼면 바닥하고 충돌함

            var hit = Physics2D.BoxCast(center, size, 0f, direction, distance, _whatIsGround);

            if (hit)
                distance = hit.distance;
            
            return hit;
        }
        
        private void OnDrawGizmos()
        {
            if (_groundChecker == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_groundChecker.position - new Vector3(0, _checkDistance * 0.5f), 
                new Vector3(_groundCheckWidth, _checkDistance, 1f));

            if (_wallChacker == null) return;
            Gizmos.DrawLine(_wallChacker.position, _wallChacker.position + new Vector3(_wallCheckWidth, 0, 0));
        }
    }
}