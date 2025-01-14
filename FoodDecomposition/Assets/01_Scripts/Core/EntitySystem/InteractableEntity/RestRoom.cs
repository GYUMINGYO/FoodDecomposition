using GM.Staffs;
using UnityEngine;

namespace GM.InteractableEntitys
{
    public class RestRoom : SingleTableEntity
    {
        public StaffType StaffType => _staffType;
        [SerializeField] private StaffType _staffType;
    }
}
