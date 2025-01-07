using GM.Managers;
using TMPro;
using UnityEngine;

namespace GM
{
    public class UIManager : MonoBehaviour, IManagerable
    {
        [SerializeField] private TextMeshProUGUI moneyText;

        public void Clear()
        {
        }

        public void Initialized()
        {
            SetMoneyUI(0);
        }

        public void SetMoneyUI(float money)
        {
            moneyText.text = $"${money}";
        }
    }
}