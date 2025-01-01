using System.Collections.Generic;
using GM.Data;
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
    public class Recipe : ScriptableObject
    {
        public FoodType type;
        public float sellPrice;
        public float researchPrice;
        public bool unLock = false;
        public GameObject foodPrefab;
        public List<CookingTableType> CookingPathList = new List<CookingTableType>();
    }
}
