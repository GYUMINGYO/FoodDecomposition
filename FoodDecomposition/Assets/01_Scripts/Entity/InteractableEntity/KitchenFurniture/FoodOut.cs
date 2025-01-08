using UnityEngine;

namespace GM
{
    public class FoodOut : SharedTableEntity
    {
        public Transform FoodTrm => _foodTrm;
        [SerializeField] private Transform _foodTrm;
    }
}
