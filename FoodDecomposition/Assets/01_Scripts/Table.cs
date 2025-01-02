using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public struct SeatData
{
    public NavMeshObstacle obstacle;
    public Transform foodPos;
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
                    foodPos = chairs[i].GetChild(0)
                };

                chairDictionary[chairs[i]] = seatData;
            }
        }

        public void OffObstacle(Transform chairTrm)
        {
            if (chairDictionary.ContainsKey(chairTrm))
            {
                chairDictionary[chairTrm].obstacle.enabled = false;
            }
        }

        public void CreateFood(Transform chairTrm, PoolTypeSO poolType)
        {
            Vector3 foodPos = chairDictionary[chairTrm].foodPos.position;

            foodObj = SingletonePoolManager.Instance.Pop(poolType) as Food;
            foodObj.transform.position = foodPos;
        }

        public void StandChair(Transform chairTrm)
        {
            if (chairDictionary.ContainsKey(chairTrm))
            {
                chairDictionary[chairTrm].obstacle.enabled = true;
            }

            if (foodObj != null)
            {
                SingletonePoolManager.Instance.Push(foodObj);
            }
        }
    }
}
