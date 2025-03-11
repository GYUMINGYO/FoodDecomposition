using System;
using System.Collections.Generic;
using GM.CookWare;
using GM.Entities;
using GM.InteractableEntities;
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
    public class RecipeSO : ScriptableObject, ICloneable
    {
        public string foodName;
        public FoodType type;
        public float tasty;
        public float sellPrice;
        public float researchFunds;
        public float materialCost;
        public bool isLock = true;
        public bool isSale = true;
        public Sprite icon;
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

            if (ManagerHub.Instance.GetManager<RestourantManager>().GetStaticFirstInteractableEntity(_cookingPathList[_index], out interactableEntity))
            {
                CookingTable cookingTable = interactableEntity as CookingTable;
                return cookingTable.CookAnimation.length;
            }

            return default;
        }

        public CookingTable GetCurrentCookingTable()
        {
            InteractableEntity interactableEntity;

            if (ManagerHub.Instance.GetManager<RestourantManager>().GetStaticFirstInteractableEntity(_cookingPathList[_index], out interactableEntity))
            {
                CookingTable cookingTable = interactableEntity as CookingTable;
                return cookingTable;
            }

            return default;
        }


        public bool IsCookingPathComplete()
        {
            if (_index >= _cookingPathList.Count)
            {
                _index = 0;
                return true;
            }
            else
            {
                return false;
            }
        }

        public object Clone()
        {
            return Instantiate(this);
        }

        public void SetPrice(float price) => sellPrice = price;
    }
}
