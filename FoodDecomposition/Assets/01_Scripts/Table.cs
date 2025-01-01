using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GM
{
    public class Table : MonoBehaviour
    {
        private Dictionary<Transform, NavMeshObstacle> chairDictionary;

        [SerializeField] private Transform foodPos;
        private GameObject foodObj;

        private void Awake()
        {
            chairDictionary = new Dictionary<Transform, NavMeshObstacle>();

            Transform[] chairs = GetComponentsInChildren<Transform>();
            for (int i = 1; i < chairs.Length; i++)
                chairDictionary[chairs[i]] = chairs[i].GetComponent<NavMeshObstacle>();
        }

        public void OffObstacle(Transform chairTrm)
            => chairDictionary[chairTrm].enabled = false;

        public void CreateFood(GameObject foodPrefab)
            => foodObj = Instantiate(foodPrefab, foodPos.position, Quaternion.identity);

        public void StandChair(Transform chairTrm)
        {
            chairDictionary[chairTrm].enabled = true;
            Destroy(foodObj);
        }
    }
}