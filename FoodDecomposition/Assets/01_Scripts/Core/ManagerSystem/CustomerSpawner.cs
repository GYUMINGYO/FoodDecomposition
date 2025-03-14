using System.Collections;
using GM.GameEventSystem;
using UnityEngine;

namespace GM.Managers
{
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private PoolTypeSO customerPoolType;
        [SerializeField] private Transform extrenceTrm;
        [SerializeField] private GameEventChannelSO gameCycleChannel;

        [SerializeField] private float minSpawnTime = 3f;
        [SerializeField] private float maxSpawnTime = 7f;

        private float time = 0;

        private void Start()
        {
            gameCycleChannel.AddListener<RestourantCycleEvent>(HandleStartSpawn);
            gameCycleChannel.AddListener<RestourantClosingTimeEvent>(HandleEndSpawn);
        }

        private void OnDestroy()
        {
            gameCycleChannel.RemoveListener<RestourantCycleEvent>(HandleStartSpawn);
            gameCycleChannel.RemoveListener<RestourantClosingTimeEvent>(HandleEndSpawn);
        }

        private void HandleStartSpawn(RestourantCycleEvent evt)
        {
            if (evt.open)
            {
                StartSpawn();
            }
        }

        private void HandleEndSpawn(RestourantClosingTimeEvent evt)
        {
            EndSpawn();
        }

        private void StartSpawn()
        {
            StartCoroutine(CustomerSpawnCoroutine());
        }

        private void EndSpawn()
        {
            StopAllCoroutines();
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
                    if (ManagerHub.Instance.GetManager<RestourantManager>().IsSeatFull)
                    {
                        yield return new WaitUntil(() => !ManagerHub.Instance.GetManager<RestourantManager>().IsSeatFull);
                        timer = 0;
                        time = Time.time;
                    }

                    yield return null;
                }

                Customer customer = ManagerHub.Instance.Pool.Pop(customerPoolType) as Customer;
                customer.transform.position = extrenceTrm.position;
                customer.transform.rotation = Quaternion.Euler(0, 0, 0);
                ManagerHub.Instance.GetManager<DataManager>().AddCustomerCnt();
                ManagerHub.Instance.GetManager<PreferenceManager>().AddDictionary(customer);

                time = Time.time;
            }
        }
    }
}