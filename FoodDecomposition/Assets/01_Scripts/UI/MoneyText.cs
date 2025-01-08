using DG.Tweening;
using TMPro;
using UnityEngine;

namespace GM
{
    public class MoneyText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private float duration = 0.5f;

        public void UpText(float price)
        {
            moneyText.text = price.ToString();

            moneyText.transform.localPosition = new Vector3(0, -100, 0);
            moneyText.enabled = true;

            moneyText.transform
                .DOLocalMoveY(100, duration)
                .SetEase(Ease.OutCubic)
                .OnComplete(() => moneyText.enabled = false);
        }
    }
}
