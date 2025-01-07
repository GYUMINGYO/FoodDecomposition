using GM;
using GM.Staffs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public struct SeatData
{
    public Customer customer;
    public NavMeshObstacle obstacle;
    public Transform foodPos;
    public bool isSit;
}

namespace GM
{
    public class Table : MonoBehaviour
    {
        private Dictionary<Transform, SeatData> chairDictionary;

        [SerializeField] private List<Transform> standTrmList;
        private Food foodObj;

        private void Awake()
        {
            chairDictionary = new Dictionary<Transform, SeatData>();

            Transform[] chairs = GetComponentsInChildren<Transform>().Where(x => x.CompareTag("Chair")).ToArray();
            for (int i = 0; i < chairs.Length; i++)
            {
                SeatData seatData = new SeatData
                {
                    customer = null,
                    obstacle = chairs[i].GetComponentInParent<NavMeshObstacle>(),
                    foodPos = chairs[i].GetChild(0),
                    isSit = false
                };

                chairDictionary[chairs[i]] = seatData;
            }
        }

        public void ChangeChairState(Transform chairTrm, bool isSit, Customer customer = null)
        {
            if (chairDictionary.ContainsKey(chairTrm))
            {
                SeatData seatData = chairDictionary[chairTrm];

                seatData.obstacle.enabled = !isSit;
                seatData.isSit = isSit;
                if(isSit)
                    seatData.customer = customer;

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

        public bool HasEmptyChair() => chairDictionary.Any(x => !x.Value.isSit);

        public Transform GetChair()
        {
            List<Transform> chairList = chairDictionary
                .Where(kv => !kv.Value.isSit)
                .Select(kv => kv.Key)
                .ToList();

            if (chairList.Count == 0) return null;

            Transform chair = chairList[Random.Range(0, chairList.Count)];
            SeatData seatData = chairDictionary[chair];
            seatData.isSit = true;
            chairDictionary[chair] = seatData;
            return chair;
        }

        public Transform GetWaiterStandTrm(Transform waiterTrm)
        {
            if (standTrmList.Count == 1)
                return standTrmList[0];

            Transform closeTrm = null;
            float shorTime = Mathf.Infinity;

            foreach (Transform standTrm in standTrmList)
            {
                NavMeshPath path = new NavMeshPath();
                if (NavMesh.CalculatePath(waiterTrm.position, standTrm.position, NavMesh.AllAreas, path))
                {
                    float pathLength = 0;
                    for (int i = 1; i < path.corners.Length; i++)
                    {
                        pathLength += Vector3.Distance(path.corners[i - 1], path.corners[i]);
                    }

                    if (pathLength < shorTime)
                    {
                        shorTime = pathLength;
                        closeTrm = standTrm;
                    }
                }
            }

            return closeTrm;
        }

        public Transform GetFoodPos(Customer customer)
        {
            foreach(var pair in chairDictionary)
            {
                if(pair.Value.customer == customer)
                {
                    return pair.Value.foodPos;
                }
            }
            return null;
        }
    }
}
