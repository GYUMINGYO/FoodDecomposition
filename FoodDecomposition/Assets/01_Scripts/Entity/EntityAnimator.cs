using System;
using UnityEngine;

namespace GM.Entities
{
    public class EntityAnimator : MonoBehaviour, IEntityComponent
    {
        private Entity _entity;
        
        [SerializeField] private AnimationClip _originalChangeClip;
        
        private Animator _animator;
        private AnimatorOverrideController _animatorOverrideController;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _animator = GetComponent<Animator>();
            _animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
            _animator.runtimeAnimatorController = _animatorOverrideController;
        }

        public void SetCookingAnimation(AnimationClip nextClip)
        {
            _animatorOverrideController[_originalChangeClip.name] = nextClip;
        }
    }
}
