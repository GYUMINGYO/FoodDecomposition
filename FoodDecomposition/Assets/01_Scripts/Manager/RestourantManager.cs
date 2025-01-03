using System.Collections.Generic;
using System.Linq;
using GM.CookWare;
using GM.Entities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GM.Managers
{
    public class RestourantManager : IManagerable
    {
        public Dictionary<Transform, Customer> chairDictionary;

        private Dictionary<Enums.InteractableEntityType, List<InteractableEntity>> _interactableEntityDictionary;

        public void Initialized()
        {
            chairDictionary = new Dictionary<Transform, Customer>();
            _interactableEntityDictionary = new Dictionary<Enums.InteractableEntityType, List<InteractableEntity>>();

            foreach (var chair in GameObject.FindGameObjectsWithTag("Chair"))
            {
                chairDictionary.Add(chair.transform, null);
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
            chairDictionary.Clear();
            _interactableEntityDictionary.Clear();
        }

        public Transform GetChiar()
        {
            List<Transform> nullValueList = new List<Transform>();
            foreach (var pair in chairDictionary)
            {
                if (pair.Value == null)
                {
                    nullValueList.Add(pair.Key);
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
    }
}
