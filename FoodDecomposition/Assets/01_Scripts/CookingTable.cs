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

        // TODO : 이거 애니메이션으로 따로 빼기
        //! 임시 변수
        public float CookingTime = 0f;
    }
}
