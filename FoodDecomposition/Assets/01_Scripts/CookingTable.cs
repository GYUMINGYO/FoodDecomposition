using UnityEngine;

namespace GM
{
    public enum CookingTableType
    {
        Stove,
        CuttingBoard,
        Sink
    }
    public class CookingTable : MonoBehaviour
    {
        public CookingTableType Type => _type;
        [SerializeField] private CookingTableType _type;
    }
}
