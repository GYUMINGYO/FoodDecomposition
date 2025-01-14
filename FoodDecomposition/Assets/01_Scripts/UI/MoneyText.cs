using DG.Tweening;
using TMPro;
using UnityEngine;

namespace GM
{
    public class MoneyText : MonoBehaviour
    {
        [SerializeField] private float duration = 0.5f;
        private TextMeshPro moneyText;

        private void Awake()
        {
            moneyText = GetComponent<TextMeshPro>();
        }

        public void UpText(float price)
        {
            Transform parent = transform.parent;
            transform.SetParent(null);
            transform.position = new Vector3(parent.position.x, transform.position.y, parent.position.z);

            moneyText.text = "+" + price.ToString();
            moneyText.enabled = true;

            moneyText.transform
                .DOMoveY(transform.position.y + 2, duration)
                .SetEase(Ease.OutCubic)
                .OnComplete(() =>
                {
                    moneyText.enabled = false;
                    transform.SetParent(parent);
                    transform.localPosition = new Vector3(0, 3, 0);
                    transform.localRotation = Quaternion.identity;
                });
        }
    }
}
