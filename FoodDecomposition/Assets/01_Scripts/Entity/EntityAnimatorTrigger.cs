using System;
using UnityEngine;

namespace GM.Entities
{
    public class EntityAnimatorTrigger : MonoBehaviour, IEntityComponent
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
