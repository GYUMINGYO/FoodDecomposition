using UnityEngine;

namespace GM
{
    public class Food : MonoBehaviour, IPoolable
    {
        [SerializeField] private PoolTypeSO poolType;

        public PoolTypeSO PoolType => poolType;

        public GameObject GameObject => gameObject;

        public void ResetItem()
        {
        }

        public void SetUpPool(Pool pool)
        {
        }
    }
}
