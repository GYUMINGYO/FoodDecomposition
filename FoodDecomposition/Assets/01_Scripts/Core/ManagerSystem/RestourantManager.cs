using System.Collections.Generic;
using System.Linq;
using GM.Entities;
using GM.InteractableEntitys;
using GM.Staffs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GM.Managers
{
    public class RestourantManager : IManagerable
    {
        public List<Table> TableList;

        private Dictionary<Enums.InteractableEntityType, List<InteractableEntity>> _interactableEntityDictionary;

        // TODO :  데이터를 분산하지 말고 하나로 뭉치는 DataManger를 사용할까?
        private float money = 0;

        public void Initialized()
        {
            TableList = new();
            _interactableEntityDictionary = new Dictionary<Enums.InteractableEntityType, List<InteractableEntity>>();

            foreach (Table Table in GameObject.FindObjectsByType<Table>(FindObjectsSortMode.None))
            {
                TableList.Add(Table);
            }

            foreach (var table in GameObject.FindObjectsByType<InteractableEntity>(FindObjectsSortMode.None))
            {
                if (_interactableEntityDictionary.ContainsKey(table.Type) == false)
                {
                    _interactableEntityDictionary[table.Type] = new List<InteractableEntity>();
                }
                _interactableEntityDictionary[table.Type].Add(table);
            }
        }

        public void Clear()
        {
            TableList.Clear();
            _interactableEntityDictionary.Clear();
        }

        public Table GetTable()
        {
            List<Table> nullValueList = new();
            foreach (Table table in TableList)
            {
                if (table.HasEmptyChair())
                {
                    nullValueList.Add(table);
                }
            }

            if (nullValueList.Count == 0)
                return default;

            int randIdx = Random.Range(0, nullValueList.Count);
            return nullValueList[randIdx];
        }

        // TODO : 레스트 랑 인터렉티브 함수가 너무 같다

        /// <summary>
        /// Get the nearest interactable entity
        /// </summary>
        /// <param name="type">InteractableEntity type</param>
        /// <param name="tableEntity">InteractableEntity</param>
        /// <param name="owner"></param>
        /// <returns>The nearest interactable entity</returns>
        public bool GetInteractableEntity(Enums.InteractableEntityType type, out InteractableEntity tableEntity, Entity owner)
        {
            float minimumDistance = float.MaxValue;
            tableEntity = null;

            if (_interactableEntityDictionary.TryGetValue(type, out List<InteractableEntity> tables) && tables.Count > 0)
            {
                foreach (var table in tables)
                {
                    if (table.InUse == false)
                    {
                        float distance = Vector3.Distance(owner.transform.position, table.transform.position);
                        if (distance < minimumDistance)
                        {
                            minimumDistance = distance;
                            tableEntity = table;
                        }
                    }
                }

                if (tableEntity != null)
                {
                    return true;
                }
            }

            return false;
        }

        public bool GetRestEntity(Enums.InteractableEntityType type, out RestRoom tableEntity, Entity owner, StaffType staffType)
        {
            float minimumDistance = float.MaxValue;
            tableEntity = default;

            if (_interactableEntityDictionary.TryGetValue(type, out List<InteractableEntity> tables) && tables.Count > 0)
            {
                foreach (RestRoom table in tables)
                {
                    if (table.InUse == false && table.StaffType == staffType)
                    {
                        float distance = Vector3.Distance(owner.transform.position, table.transform.position);
                        if (distance < minimumDistance)
                        {
                            minimumDistance = distance;
                            tableEntity = table;
                        }
                    }
                }

                if (tableEntity != null)
                {
                    return true;
                }
            }

            return false;
        }

        public bool GetFirstInteractableEntity(Enums.InteractableEntityType type, out InteractableEntity tableEntity)
        {
            tableEntity = null;

            if (_interactableEntityDictionary.TryGetValue(type, out List<InteractableEntity> tables) && tables.Count > 0)
            {
                tableEntity = tables.First();
                return true;
            }

            return false;
        }

        public float GetMoney()
        {
            return money;
        }

        public void AddMoney(float money)
        {
            this.money += money;
            ManagerHub.Instance.GetManager<UIManager>().SetMoneyUI(this.money);
        }

        public void SubtractMoney(float money)
        {
            this.money -= money;
            ManagerHub.Instance.GetManager<UIManager>().SetMoneyUI(this.money);
        }
    }
}