using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GM
{
    public class SpeakBubble : MonoBehaviour
    {
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image orderIconImage;

        [Header("WaitSlider")]
        [SerializeField] private Image waitIconImage;
        [SerializeField] private Slider waitSlider;
        [SerializeField] private Image waitFillImage;

        [SerializeField] private float waitTime = 15f;

        private Customer customer;

        private bool isWaiting = false;
        public bool IsWaiting => isWaiting;

        private void Awake()
        {
            customer = GetComponentInParent<Customer>();
        }

        [ContextMenu("lookCamera")]
        private void Update()
        {
            transform.LookAt(Camera.main.transform);
        }

        public void OrderShow()
        {
            Show();

            orderIconImage.enabled = true;
            orderIconImage.sprite = customer.GetOrderData().recipe.icon;
        }

        public void WaitShow()
        {
            Show();

            waitIconImage.enabled = true;
            waitSlider.gameObject.SetActive(true);

            waitIconImage.sprite = customer.GetOrderData().recipe.icon;
            waitSlider.value = 1;
            waitFillImage.color = Color.green;

            StartCoroutine(WaitGauge());
        }

        private IEnumerator WaitGauge()
        {
                float elapsedTimee = 0;

            while (elapsedTimee < waitTime) 
            {
                elapsedTimee += Time.deltaTime;
                waitSlider.value = Mathf.Lerp(1, 0, elapsedTimee /  waitTime);

                
                if(waitSlider.value <= 0.25f)
                {
                    waitFillImage.color = Color.red;
                }
                else if (waitSlider.value <= 0.5f)
                {
                    waitFillImage.color = new Color(1f, 0.5f, 0f);
                }
                yield return null;
            }

            waitSlider.value = 0;
            isWaiting = true;

            customer.customerExitEvent?.Invoke();
        }

        private void Show()
        {
            Hide();
            backgroundImage.enabled = true;
        }

        public void Hide()
        {
            backgroundImage.enabled = false;
            orderIconImage.enabled = false;
            waitIconImage.enabled = false;
            waitSlider.gameObject.SetActive(false);
            isWaiting = false;
            StopAllCoroutines();
        }
    }
}
