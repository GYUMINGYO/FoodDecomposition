using UnityEngine;

namespace GM.CookWare
{
    public class InteractableEntity : MonoBehaviour
    {
        public bool InUse => _inUse;
        protected bool _inUse = false;

        public Enums.InteractableEntityType Type => _type;
        [SerializeField] protected Enums.InteractableEntityType _type;
    }
}
