using System;
using System.Collections.Generic;
using GM.CookWare;
using GM.Entities;
using GM.InteractableEntitys;
using GM.Managers;
using UnityEngine;

namespace GM
{
    public enum FoodType
    {
        Burger,
        Pizza,
        icecream,
        Meet,
        Supe
    }

    [CreateAssetMenu(menuName = "SO/Recipe")]
    public class Recipe : ScriptableObject, ICloneable
    {
        public FoodType type;
        public float sellPrice;
        public float researchPrice;
        public bool unLock = false;
        public PoolTypeSO poolType;

        [SerializeField] private List<Enums.InteractableEntityType> _cookingPathList = new List<Enums.InteractableEntityType>();

        private int _index = 0;

        public CookingTable GetNextCookingTable(Entity owner)
        {
            if (_index >= _cookingPathList.Count)
            {
                return null;
            }

            InteractableEntity interactableEntity;

            if (ManagerHub.Instance.GetManager<RestourantManager>().GetInteractableEntity(_cookingPathList[_index], out interactableEntity, owner))
            {
                CookingTable nextTable = interactableEntity as CookingTable;
                return nextTable;
            }

            return null;
        }

        public void NextCookingTable()
        {
            _index++;
        }

        public int GetCookingTableCount()
        {
            return _cookingPathList.Count;
        }

        public float GetCurrentCookingTableTime()
        {   
            InteractableEntity interactableEntity;

            if (ManagerHub.Instance.GetManager<RestourantManager>().GetFirstInteractableEntity(_cookingPathList[_index], out interactableEntity))
            {
                CookingTable cookingTable = interactableEntity as CookingTable;
                return cookingTable.CookAnimation.length;
            }

            return default;
        }

        public CookingTable GetCurrentCookingTable()
        {
            InteractableEntity interactableEntity;

            if (ManagerHub.Instance.GetManager<RestourantManager>().GetFirstInteractableEntity(_cookingPathList[_index], out interactableEntity))
            {
                CookingTable cookingTable = interactableEntity as CookingTable;
                return cookingTable;
            }

            return default;
        }
            

        public bool IsCookingPathComplete()
        {
            return _index >= _cookingPathList.Count;
        }

        public object Clone()
        {
            return Instantiate(this);
        }
    }
}
