using GM.Managers;
using MKDir;
using UnityEngine;

namespace GM
{
    public class MapManager : MonoSingleton<MapManager>
    {
        [SerializeField] private Transform extrenceAndExitTrm;
        [SerializeField] private Transform counterTrm;

        public Transform ExtrenceAndExitTrm => extrenceAndExitTrm;
        public Transform CounterTrm => counterTrm;
    }
}
