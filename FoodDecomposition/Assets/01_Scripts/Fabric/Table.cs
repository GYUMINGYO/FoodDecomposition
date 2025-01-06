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
        public List<Transform> WaiterWaitTransform { get; }
        [SerializeField] private List<Transform> _waiterWaitTransform;

        private Dictionary<Transform, SeatData> _chairDictionary;
        private Food foodObj;

        private void Awake()
        {
            _chairDictionary = new Dictionary<Transform, SeatData>();
            _waiterWaitTransform = new List<Transform>();

            Transform[] chairs = GetComponentsInChildren<Transform>().Where(x => x.CompareTag("Chair")).ToArray();
            for (int i = 0; i < chairs.Length; i++)
            {
                SeatData seatData = new SeatData
                {
                    obstacle = chairs[i].GetComponentInParent<NavMeshObstacle>(),
                    foodPos = chairs[i].GetChild(0)
                };

                _chairDictionary[chairs[i]] = seatData;
            }
        }

        public void SetObstacle(Transform chairTrm, bool value)
        {
            if (_chairDictionary.ContainsKey(chairTrm))
            {
                _chairDictionary[chairTrm].obstacle.enabled = value;
            }
        }

        public void CreateFood(Transform chairTrm, PoolTypeSO poolType)
        {
            Vector3 foodPos = _chairDictionary[chairTrm].foodPos.position;

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
    }
}
