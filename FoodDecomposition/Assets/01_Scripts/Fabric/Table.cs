using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public struct SeatData
{
    public NavMeshObstacle obstacle;
    public Transform foodPos;
    public bool isSit;
}

namespace GM
{
    public class Table : MonoBehaviour
    {
        private Dictionary<Transform, SeatData> chairDictionary;

        private Food foodObj;

        private void Awake()
        {
            chairDictionary = new Dictionary<Transform, SeatData>();

            Transform[] chairs = GetComponentsInChildren<Transform>().Where(x => x.CompareTag("Chair")).ToArray();
            for (int i = 0; i < chairs.Length; i++)
            {
                SeatData seatData = new SeatData
                {
                    obstacle = chairs[i].GetComponentInParent<NavMeshObstacle>(),
                    foodPos = chairs[i].GetChild(0),
                    isSit = false
                };

                chairDictionary[chairs[i]] = seatData;
            }
        }

        public void ChangeChairState(Transform chairTrm, bool isSit)
        {
            if (chairDictionary.ContainsKey(chairTrm))
            {
                SeatData seatData = chairDictionary[chairTrm];

                seatData.obstacle.enabled = !isSit;
                seatData.isSit = isSit;

                chairDictionary[chairTrm] = seatData;
            }
        }

        public void CreateFood(Transform chairTrm, PoolTypeSO poolType)
        {
            Vector3 foodPos = chairDictionary[chairTrm].foodPos.position;

            foodObj = SingletonePoolManager.Instance.Pop(poolType) as Food;
            foodObj.transform.position = foodPos;
        }

        public void DestroyFood()
        {
            if (foodObj != null)
            {
                SingletonePoolManager.Instance.Push(foodObj);
            }
        }

        public Transform GetChair()
        {
            List<Transform> chairList = chairDictionary
                .Where(kv => !kv.Value.isSit)
                .Select(kv => kv.Key)
                .ToList();

            if (chairList.Count == 0) return null;

            return chairList[Random.Range(0, chairList.Count)];
        }
    }
}
