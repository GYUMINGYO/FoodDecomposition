using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GM
{
    public class DebugBubble : MonoBehaviour
    {
        [SerializeField] private TextMeshPro text;
        [SerializeField] private Image image;

        private void Update()
        {
            transform.LookAt(Camera.main.transform);
        }

        public void Show(string msg)
        {
            image.enabled = true;
            text.text = msg;
        }

        public void Hide()
        {
            image.enabled = false;
            text.text = string.Empty;
        }
    }
}
