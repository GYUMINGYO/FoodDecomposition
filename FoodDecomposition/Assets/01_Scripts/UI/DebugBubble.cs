using TMPro;
using UnityEngine;

namespace GM
{
    public class DebugBubble : MonoBehaviour
    {
        [SerializeField] private GameObject container;
        [SerializeField] private TextMeshPro text;
        [SerializeField] private Transform background;

        private void Update()
        {
            transform.LookAt(Camera.main.transform);
        }

        public void Show(string msg)
        {
            container.SetActive(true);
            text.text = msg;

            float size = (8f * text.preferredWidth) / 0.33f;

            Vector3 localScale = background.localScale;
            localScale.x = size;
            background.localScale = localScale;
        }

        public void Hide()
        {
            container.SetActive(false);
        }
    }
}
