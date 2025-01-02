using System;
using System.Collections.Generic;
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

        [SerializeField] private List<CookingTableType> _cookingPathList = new List<CookingTableType>();

        private int _index = 0;

        public CookingTable GetNextCookingTable()
        {
            if (_index >= _cookingPathList.Count)
            {
                return default;
            }

            CookingTable nextTable = ManagerHub.Instance.GetManager<RestourantManager>().GetCookingTable(_cookingPathList[_index]);
            _index++;

            return nextTable;
        }

        public bool IsCookingPathComplete()
        {
            return _index > _cookingPathList.Count;
        }

        public object Clone()
        {
            return Instantiate(this);
        }
    }
}
