using System.Collections;
using UnityEngine;

namespace GM
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private PoolTypeSO customerPoolType;
        [SerializeField] private Transform ExtrenceTrm;

        [SerializeField] private float minSpawnTime = 3f;
        [SerializeField] private float maxSpawnTime = 7f;

        private void Start()
        {
            StartCoroutine(CustomerSpawnCoroutine());
        }
        
        private IEnumerator CustomerSpawnCoroutine()
        {
            while (true)
            {
                float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
                yield return new WaitForSeconds(spawnTime);

                Customer customer = SingletonePoolManager.Instance.Pop(customerPoolType) as Customer;
                customer.transform.position = ExtrenceTrm.position;
            }
        }
    }
}