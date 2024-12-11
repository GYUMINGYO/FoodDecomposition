using GM._01_Scripts.Data;
using UnityEngine;

namespace GM
{
    public enum FoodType
    {
        Buger,
        Pizza,
        icecream,
        Meet,
        Supe
    }

    public class Recipe : MonoBehaviour
    {
        FoodType type;
        public float sellPrice;
        public float researchPrice;
        public bool unLock = false;

        // todo : 요리 경로 지정
    }
}
