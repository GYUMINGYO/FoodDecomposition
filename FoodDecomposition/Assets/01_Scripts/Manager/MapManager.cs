using System.Collections.Generic;
using MKDir;
using UnityEngine;

namespace GM
{
    public class MapManager : MonoSingleton<MapManager>
    {
        [SerializeField] private Transform extrenceAndExitTrm;
        [SerializeField] private List<Transform> counterTrmList;

        public Transform ExtrenceAndExitTrm => extrenceAndExitTrm;

        private int counterCnt = 0;
        
        public Transform GetCountTrm()
        {
            if(counterTrmList.Count >= counterCnt)
                return null;

            return counterTrmList[counterCnt++];
        }

        public void ReleaseCount() => counterCnt--;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
                ReleaseCount();
        }
    }
}
