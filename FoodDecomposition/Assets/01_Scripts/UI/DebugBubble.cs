using TMPro;
using UnityEngine;

namespace GM
{
    public class DebugBubble : MonoBehaviour
    {
        [SerializeField] private TextMeshPro text;
        [SerializeField] private Transform background;

        private void Update()
        {
            transform.LookAt(Camera.main.transform);
        }

        public void Show(string msg)
        {
            text.text = msg;

            float size = (8f * text.preferredWidth) / 0.33f;
            SetScale(size);
        }

        public void Hide()
        {
            SetScale(0);
        }

        private void SetScale(float scale)
        {
            Vector3 localScale = background.localScale;
            localScale.x = scale;
            background.localScale = localScale;
        }
    }
}
