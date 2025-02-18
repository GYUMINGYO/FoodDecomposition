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

        private void Awake()
        {
            //! Test Code
            //if (Input.GetKeyDown(KeyCode.Space))
            for (int i = 0; i < 4; ++i)
            {
                StaffInfo staffInfo = _staffInfoGenerator.GetRandomStaffInfo();
                StaffHandler staffHandler = Instantiate(_staffHandlerPrefab, transform.position, Quaternion.identity);
                staffHandler.Initialize(staffInfo);
                ManagerHub.Instance.GetManager<StaffManager>().AddStaff(staffHandler);
            }
        }
    }
}
