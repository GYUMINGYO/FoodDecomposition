using GM.Entities;
using GM.InteractableEntities;
using GM.Staffs;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GM.Managers
{
    public class RestourantManager : IManagerable
    {
        public bool IsSeatFull => isSeatFull;
        private bool isSeatFull = false;

        private Dictionary<Enums.InteractableEntityType, List<InteractableEntity>> _interactableEntityDictionary;

        public void Initialized()
        {
            uint tableId = 0;
            _interactableEntityDictionary = new Dictionary<Enums.InteractableEntityType, List<InteractableEntity>>();

            foreach (var interactableTable in GameObject.FindObjectsByType<InteractableEntity>(FindObjectsSortMode.None))
            {
                if (_interactableEntityDictionary.ContainsKey(interactableTable.Type) == false)
                {
                    _interactableEntityDictionary[interactableTable.Type] = new List<InteractableEntity>();
                }

                if (interactableTable is Table table)
                {
                    table.SetID(tableId++);
                }
                _interactableEntityDictionary[interactableTable.Type].Add(interactableTable);
            }
        }

        public void Clear()
        {
            _interactableEntityDictionary.Clear();
        }

        public void SetIsSeatFull(bool value) => isSeatFull = value;

        public Table GetTable(uint id = uint.MaxValue)
        {
            List<InteractableEntity> tableList;
            _interactableEntityDictionary.TryGetValue(Enums.InteractableEntityType.Table, out tableList);

            if (id < uint.MaxValue)
            {
                foreach (Table table in tableList)
                {
                    if (table.ID == id)
                    {
                        return table;
                    }
                }
            }

            // any Table
            List<Table> nullValueList = new();
            foreach (Table table in tableList)
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