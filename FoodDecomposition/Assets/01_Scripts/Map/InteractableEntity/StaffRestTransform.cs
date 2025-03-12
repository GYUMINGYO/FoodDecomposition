using GM.Staffs;
using UnityEngine;

namespace GM.InteractableEntities
{
    public class StaffRestTransform : MonoBehaviour
    {
        public bool IsFull => _isFull;
        private bool _isFull = false;

        private Staff _staff;

        public void SetStaff(Staff staff)
        {
            _staff = staff;
        }
    }
}
