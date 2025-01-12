using UnityEngine;

namespace GM.InteractableEntitys
{
    public class SingleTableEntity : InteractableEntity
    {
        public Transform EntityTransform => _entityTransform;
        [SerializeField] protected Transform _entityTransform;
    }
}
