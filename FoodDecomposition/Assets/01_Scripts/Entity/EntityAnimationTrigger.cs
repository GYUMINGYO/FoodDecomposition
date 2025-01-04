using System;
using GM.Entities;
using UnityEngine;

namespace GM.Animations
{
    public class EntityAnimationTrigger : MonoBehaviour, IEntityComponent
    {
        public event Action OnAnimationEnd;

        protected Entity _entity;

        public void Initialize(Entity entity)
        {
            _entity = entity;
        }

        protected virtual void AnimationEnd() => OnAnimationEnd?.Invoke();
    }
}
