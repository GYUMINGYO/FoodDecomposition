using GM.InteractableEntities;
using UnityEngine;

namespace GM
{
    public class SharedTableEntity : InteractableEntity
    {
        public Transform SenderTransform => _senderTransform;
        [SerializeField] protected Transform _senderTransform;

        public Transform ReceiverTransform => _receiverTransform;
        [SerializeField] protected Transform _receiverTransform;
    }
}
