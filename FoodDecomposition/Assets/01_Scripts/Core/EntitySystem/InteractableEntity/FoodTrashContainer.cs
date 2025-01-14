using UnityEngine;

namespace GM.InteractableEntitys
{
    public class FoodTrashContainer : SingleTableEntity
    {
        public Transform FoodTrashTransform => _foodTrashTransform;
        [SerializeField] private Transform _foodTrashTransform;
    }
}
