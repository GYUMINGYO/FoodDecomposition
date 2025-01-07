using UnityEngine;

namespace GM
{
    public class FoodOut : SharedTableEntity
    {
        public Transform FoodTrm => _foodTrm;
        [SerializeField] protected Transform _foodTrm;
    }
}
