using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GM
{
    public class SpeakBubble : MonoBehaviour
    {
        [SerializeField] private Image speakBubbleImage;

        [Header("OrderWait")]
        [SerializeField] private Image orderIconImage;
        [SerializeField] private Image backOrderIconImage;

        [Header("FoodWait")]
        [SerializeField] private GameObject slider;
        [SerializeField] private Image foodWaitIconImage;
        [SerializeField] private Image foodWaitFillImage;

        [Header("")]
        private Customer customer;

        private bool isWaiting = false;
        public bool IsWaiting => isWaiting;

        private void Awake()
        {
            customer = GetComponentInParent<Customer>();

            Hide();
        }

        public void OrderWaitShow()
        {
            Show();

            orderIconImage.enabled = true;
            backOrderIconImage.enabled = true;
            orderIconImage.sprite = customer.GetOrderData().recipe.icon;
            backOrderIconImage.sprite = customer.GetOrderData().recipe.icon;

            StartCoroutine(OrderWaitGauge());
        }

        private IEnumerator OrderWaitGauge()
        {
            float elapsedTime = 0;

            while (elapsedTime < customer.OrderWaitTime)
            {
                elapsedTime += Time.deltaTime;
                orderIconImage.fillAmount = Mathf.Lerp(1f, 0.1f, elapsedTime / customer.OrderWaitTime);

                yield return null;
            }

            orderIconImage.fillAmount = 0;
            isWaiting = true;

            customer.OrderData.isCustomerOut = true;
        }

        public void FoodWaitShow()
        {
            Show();

            foodWaitIconImage.enabled = true;
            slider.SetActive(true);

            foodWaitIconImage.sprite = customer.GetOrderData().recipe.icon;
            foodWaitFillImage.transform.localPosition = Vector3.zero;
            foodWaitFillImage.color = Color.green;

            StartCoroutine(FoodWaitGauge());
        }

        private IEnumerator FoodWaitGauge()
        {
            float elapsedTime = 0;

            while (elapsedTime < customer.FoodWaitTime)
            {
                elapsedTime += Time.deltaTime;
                float duration = elapsedTime / customer.FoodWaitTime;
                foodWaitFillImage.transform.localPosition = new Vector3(0, Mathf.Lerp(0, -153, duration), 0);


                if (duration >= 0.75f)
                {
                    foodWaitFillImage.color = Color.red;
                }
                else if (duration >= 0.5f)
                {
                    foodWaitFillImage.color = new Color(1f, 0.5f, 0f);
                }
                yield return null;
            }

            foodWaitFillImage.transform.localPosition = new Vector3(0, -153, 0);
            isWaiting = true;

            customer.OrderData.isCustomerOut = true;
        }

        private void Show()
        {
            Hide();

            transform.LookAt(Camera.main.transform);
            speakBubbleImage.enabled = true;
        }

        public void Hide()
        {
            speakBubbleImage.enabled = false;
            orderIconImage.enabled = false;
            backOrderIconImage.enabled = false;
            foodWaitIconImage.enabled = false;
            slider.SetActive(false);
            isWaiting = false;
            StopAllCoroutines();
        }
    }
}
