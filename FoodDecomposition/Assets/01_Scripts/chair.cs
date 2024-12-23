using UnityEngine;

namespace GM
{
    public class Chair : MonoBehaviour
    {
        //test
        [ContextMenu("dir")]
        public void Dir()
        {
            Debug.Log(transform.localRotation * Vector3.forward);
        }
    }
}
