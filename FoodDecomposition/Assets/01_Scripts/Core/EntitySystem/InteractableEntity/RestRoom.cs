using GM.Staffs;
using UnityEngine;

namespace GM.InteractableEntities
{
    public class RestRoom : SingleTableEntity
    {
        public StaffType StaffType => _staffType;
        [SerializeField] private StaffType _staffType;
    }
}
