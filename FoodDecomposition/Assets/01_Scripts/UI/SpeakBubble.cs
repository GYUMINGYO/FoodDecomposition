using GM.Managers;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum WaitState
{
    Positive,
    Neutral,
    Negative
}

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
        private WaitState curWaitState = WaitState.Positive;
        private bool isWaiting = false;

        public bool IsWaiting => isWaiting;
        
        private void Awake()
        {
            customer = GetComponentInParent<Customer>();
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

                float duration = elapsedTime / customer.OrderWaitTime;
                orderIconImage.fillAmount = Mathf.Lerp(1f, 0.1f, duration);

                if (duration >= 0.75f)
                {
                    curWaitState = WaitState.Negative;
                }
                else if (duration >= 0.5f)
                {
                    curWaitState = WaitState.Neutral;
                }
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
                    curWaitState = WaitState.Negative;
                }
                else if (duration >= 0.5f)
                {
                    foodWaitFillImage.color = new Color(1f, 0.5f, 0f);
                    curWaitState = WaitState.Neutral;
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

        public void CalculatePreferenceRate()
        {
            Hide();

            float rate = 0;
            switch (curWaitState)
            {
                case WaitState.Positive:
                    rate = 10;
                    break;
                case WaitState.Negative:
                    rate = -10;
                    break;
            }

            ManagerHub.Instance.GetManager<PreferenceManager>().ModifyPreferenceRate(customer, rate);
            curWaitState = WaitState.Positive;
        }
    }
}
