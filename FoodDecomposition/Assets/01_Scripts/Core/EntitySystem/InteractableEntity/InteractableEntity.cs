using UnityEngine;

namespace GM.InteractableEntitys
{
    public class InteractableEntity : MonoBehaviour
    {
        public bool InUse { get => _inUse; set => _inUse = value; }
        protected bool _inUse = false;

        public Enums.InteractableEntityType Type => _type;
        [SerializeField] protected Enums.InteractableEntityType _type;
    }
}
