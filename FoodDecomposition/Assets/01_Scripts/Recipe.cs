using GM._01_Scripts.Data;
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

    [CreateAssetMenu(menuName ="SO/Recipe")]
    public class Recipe : ScriptableObject
    {
        public FoodType type;
        public float sellPrice;
        public float researchPrice;
        public bool unLock = false;

        // todo : 요리 경로 지정
    }
}
