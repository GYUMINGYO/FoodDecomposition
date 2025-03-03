using System;
using UnityEngine;

namespace GM.Maps
{
    [CreateAssetMenu(fileName = "ObjectInfoSO", menuName = "SO/Map/Object")]
    public class ObjectInfoSO : ScriptableObject, ICloneable
    {
        [Header("Information")]
        public string objectName;
        public string displayName;
        public Vector3 objectSize;
        public Vector2 size;
        public bool unLock;

        [Header("Object Value")]
        public GameObject mapObject;

        public object Clone()
        {
            return Instantiate(this);
        }
    }
}
