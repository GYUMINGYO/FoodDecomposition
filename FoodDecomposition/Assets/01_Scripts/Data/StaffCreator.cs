using GM.Data;
using GM.Managers;
using GM.Staffs;
using UnityEngine;

namespace GM
{
    public class StaffCreator : MonoBehaviour
    {
        [SerializeField] private StaffProfileGeneratorSO _staffInfoGenerator;
        [SerializeField] private StaffHandler _staffHandlerPrefab;

        private void Update()
        {
            //! Test Code
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StaffProfile staffInfo = _staffInfoGenerator.GetRandomStaffInfo();
                StaffHandler staffHandler = Instantiate(_staffHandlerPrefab, transform.position, Quaternion.identity);
                staffHandler.gameObject.SetActive(false);
                staffHandler.Initialize(staffInfo);
                ManagerHub.Instance.GetManager<StaffManager>().AddStaff(staffHandler);
            }
        }
    }
}
