using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GM
{
    public class DebugBubble : MonoBehaviour
    {
        [SerializeField] private TextMeshPro text;
        [SerializeField] private Image image;
        [SerializeField] private Image iconImage;

        private Customer customer;
        private void Awake()
        {
            customer = transform.root.GetComponent<Customer>();
        }

        [ContextMenu("lookCamera")]
        private void Update()
        {
            transform.LookAt(Camera.main.transform);
        }

        public void TextShow(string msg)
        {
            Show();

            text.text = msg;
        }

        public void OrderShow()
        {
            Show();

            iconImage.enabled = true;
            iconImage.sprite = customer.GetOrderData().recipe.icon;
        }

        private void Show()
        {
            Hide();
            image.enabled = true;
        }

        public void Hide()
        {
            image.enabled = false;
            iconImage.enabled = false;
            text.text = string.Empty;
        }
    }
}
