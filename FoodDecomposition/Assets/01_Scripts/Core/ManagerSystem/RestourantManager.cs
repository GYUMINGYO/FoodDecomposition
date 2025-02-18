using GM.Entities;
using GM.InteractableEntities;
using GM.Staffs;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GM.Managers
{
    public class RestourantManager : IManagerable
    {
        public List<Table> TableList;

        public bool IsSeatFull => isSeatFull;
        private bool isSeatFull = false;

        private Dictionary<Enums.InteractableEntityType, List<InteractableEntity>> _interactableEntityDictionary;

        private int customerCnt;

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

        public void SetIsSeatFull(bool value) => isSeatFull = value;

        public void AddCustomerCnt() => customerCnt++;

        public void RemoveCustomerCnt() => customerCnt--;

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

        /// <summary>
        /// Get the nearest interactable entity
        /// </summary>
        /// <param name="type">InteractableEntity type</param>
        /// <param name="tableEntity">InteractableEntity</param>
        /// <param name="owner"></param>
        /// <returns>The nearest interactable entity</returns>
        public bool GetInteractableEntity(Enums.InteractableEntityType type, out InteractableEntity tableEntity, Entity owner)
        {
            tableEntity = null;

            var tables = GetInteractableEntitiesTryGetValue(type, table => table.InUse == false);
            if (tables != null)
            {
                tableEntity = CalculateMinimumDistanceEntity(tables, owner);
            }

            return tableEntity != null;
        }

        public bool GetStaticFirstInteractableEntity(Enums.InteractableEntityType type, out InteractableEntity tableEntity)
        {
            tableEntity = null;

            var tables = GetInteractableEntitiesTryGetValue(type);

            tableEntity = tables?.First();
            return tableEntity != null;
        }

        public bool GetRestEntity(Enums.InteractableEntityType type, out RestRoom tableEntity, Entity owner, StaffType staffType)
        {
            tableEntity = null;

            var tables = GetInteractableEntitiesTryGetValue(type, table => table.InUse == false);
            List<InteractableEntity> restRoomList = new List<InteractableEntity>();

            foreach (RestRoom restRoom in tables)
            {
                if (restRoom.StaffType == staffType)
                {
                    restRoomList.Add(restRoom);
                }
            }

            if (tables != null)
            {
                tableEntity = CalculateMinimumDistanceEntity(restRoomList, owner) as RestRoom;
            }

            return tableEntity != null;
        }

        private List<InteractableEntity> GetInteractableEntitiesTryGetValue(Enums.InteractableEntityType type, Func<InteractableEntity, bool> predicate = null)
        {
            if (_interactableEntityDictionary.TryGetValue(type, out List<InteractableEntity> tables) && tables.Count > 0)
            {
                if (predicate == null)
                {
                    return tables;
                }
                return tables.Where(predicate).ToList();
            }

            return null;
        }

        private InteractableEntity CalculateMinimumDistanceEntity(List<InteractableEntity> tables, Entity owner)
        {
            float minimumDistance = float.MaxValue;
            InteractableEntity minimumEntity = null;

            foreach (var table in tables)
            {
                float distance = Vector3.Distance(owner.transform.position, table.transform.position);
                if (distance < minimumDistance)
                {
                    minimumDistance = distance;
                    minimumEntity = table;
                }
            }

            return minimumEntity;
        }
    }
}