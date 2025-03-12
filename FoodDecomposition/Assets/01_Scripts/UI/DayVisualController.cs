using GM.Managers;
using UnityEngine;

namespace GM
{
    public class DayVisualController : MonoBehaviour
    {
        [SerializeField] private Transform timeGaugeFill;
        [SerializeField] private Transform dayLight;

        float curDuration = 0;

        private void Update()
        {
            float managerDuration = ManagerHub.Instance.GetManager<GameManager>().Duration;

            if (curDuration != managerDuration)
            {
                curDuration = managerDuration;

                timeGaugeFill.localPosition = new Vector3(-1255 + (1255 * curDuration), 0, 0);
                dayLight.localRotation = Quaternion.Euler(Mathf.Lerp(-155, -375, curDuration), -30, 0);
            }
        }
    }
}
