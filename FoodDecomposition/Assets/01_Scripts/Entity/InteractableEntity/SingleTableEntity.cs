using GM.InteractableEntitys;
using UnityEngine;

namespace GM
{
    public class SingleTableEntity : InteractableEntity
    {
        public Transform EntityTransform => _entityTransform;
        [SerializeField] protected Transform _entityTransform;
    }
}
