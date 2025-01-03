using UnityEngine;

namespace GM.CookWare
{
    public class CookingTable : SingleTableEntity
    {
        public Animation CookAnimation => _cookAnimation;
        [SerializeField] protected Animation _cookAnimation;
    }
}
