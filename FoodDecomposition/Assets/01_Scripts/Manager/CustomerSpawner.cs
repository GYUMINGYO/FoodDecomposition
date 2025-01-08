using GM.Managers;
using System.Collections;
using UnityEngine;

namespace GM
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private PoolTypeSO customerPoolType;
        [SerializeField] private Transform extrenceTrm;

        [SerializeField] private float minSpawnTime = 3f;
        [SerializeField] private float maxSpawnTime = 7f;

        private float time = 0;

        private void Start()
        {
            StartCoroutine(CustomerSpawnCoroutine());
        }
        
        private IEnumerator CustomerSpawnCoroutine()
        {
            time = Time.time;
            while (true)
            {
                float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
                float timer = 0;
                while (timer <= spawnTime)
                {
                    timer += Time.deltaTime;
                    if(ManagerHub.Instance.GetManager<MapManager>().IsSeatFull)
                    {
                        yield return new WaitUntil(() => !ManagerHub.Instance.GetManager<MapManager>().IsSeatFull);
                        timer = 0;
                        time = Time.time;
                    }

                    yield return null;
                }

                Customer customer = SingletonePoolManager.Instance.Pop(customerPoolType) as Customer;
                customer.transform.position = extrenceTrm.position;

                time = Time.time;
            }
        }
    }
}