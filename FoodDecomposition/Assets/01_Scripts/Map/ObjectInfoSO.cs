using UnityEngine;

namespace GM.Maps
{
    [CreateAssetMenu(fileName = "ObjectInfoSO", menuName = "SO/Map/Object")]
    public class ObjectInfoSO : ScriptableObject
    {
        [Header("Information")]
        public string objectName;
        public string displayName;
        public Vector3 size;

        [Header("Object Value")]
        public MapObject mapObject;
    }
}
