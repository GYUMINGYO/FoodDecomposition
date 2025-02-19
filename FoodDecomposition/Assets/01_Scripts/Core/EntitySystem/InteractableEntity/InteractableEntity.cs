using UnityEngine;

namespace GM.InteractableEntities
{
    public class InteractableEntity : MonoBehaviour
    {
        public bool InUse { get => _inUse; set => _inUse = value; }
        protected bool _inUse = false;

        public Enums.InteractableEntityType Type => _type;
        [SerializeField] protected Enums.InteractableEntityType _type;

        public bool IsShared => _isShared;
        [SerializeField] private bool _isShared;
    }
}
