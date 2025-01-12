using System.Collections.Generic;
using GM.Managers;
using MKDir;
using UnityEngine;

namespace GM
{
    public class MapManager : MonoBehaviour, IManagerable
    {
        [SerializeField] private Transform exitTrm;
        public Transform ExitTrm => exitTrm;

        private bool isSeatFull = false;
        public bool IsSeatFull => isSeatFull;

        public void SetIsSeatFull(bool value) => isSeatFull = value;

        public void Initialized()
        {
        }

        public void Clear()
        {
        }
    }
}
