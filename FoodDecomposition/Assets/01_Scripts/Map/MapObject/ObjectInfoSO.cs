using UnityEngine;

namespace GM.Maps
{
    [CreateAssetMenu(fileName = "ObjectInfoSO", menuName = "SO/Map/Object")]
    public class ObjectInfoSO : ScriptableObject
    {
        [Header("Information")]
        public PoolTypeSO poolType;
        public string objectName;
        public string displayName;
        public Vector3 objectSize;
        public Vector2Int cellSize;
        public bool unLock;
    }
}
