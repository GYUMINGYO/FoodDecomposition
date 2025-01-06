using System.Collections.Generic;
using MKDir;
using UnityEngine;

namespace GM
{
    public class MapManager : MonoSingleton<MapManager>
    {
        [SerializeField] private Transform exitTrm;
        public Transform ExitTrm => exitTrm;
    }
}
