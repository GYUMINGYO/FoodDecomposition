using GM.Entities;
using GM.Staffs;
using UnityEngine;
using UnityEngine.UI;

namespace GM.UI
{
    public class PriorityChangeUI : MonoBehaviour, IDescriptableUI
    {
        private VerticalLayoutGroup _layoutGroup;
        private Waiter _waiter;

        private void Awake()
        {
            _layoutGroup = GetComponent<VerticalLayoutGroup>();
        }

        public void InitializeUI(Unit unit)
        {
            _waiter = unit as Waiter;
        }
    }
}
