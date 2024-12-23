using TMPro;
using UnityEngine;

namespace GM
{
    public class DebugBubble : MonoBehaviour
    {
        [SerializeField] private TextMeshPro text;

        private void Update()
        {
            transform.LookAt(Camera.main.transform);
        }

        public void Show(string msg)
        {
            text.text = msg;

            float size = (8f * text.preferredWidth) / 0.33f;
        }

        public void Hide()
        {
            text.text = string.Empty;
        }
    }
}
