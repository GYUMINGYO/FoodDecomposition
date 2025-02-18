using GM.Data;
using GM.Managers;
using GM.Staffs;
using UnityEngine;

namespace GM
{
    public class StaffCreator : MonoBehaviour
    {
        [SerializeField] private StaffInfoGeneratorSO _staffInfoGenerator;
        [SerializeField] private StaffHandler _staffHandlerPrefab;

        private void Update()
        {
            //! Test Code
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StaffInfo staffInfo = _staffInfoGenerator.GetRandomStaffInfo();
                StaffHandler staffHandler = Instantiate(_staffHandlerPrefab, transform.position, Quaternion.identity);
                staffHandler.Initialize(staffInfo);
                ManagerHub.Instance.GetManager<StaffManager>().AddStaff(staffHandler);
            }
        }
    }
}
