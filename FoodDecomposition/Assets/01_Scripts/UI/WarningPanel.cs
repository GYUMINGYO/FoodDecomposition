using DG.Tweening;
using TMPro;
using UnityEngine;

namespace GM
{
    public class WarningPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        public void ShowText(string text)
        {
            WarningPanel warningPanel = Instantiate(gameObject, transform.parent).GetComponent<WarningPanel>();
            warningPanel.ShowWarning(text);
        }

        private void ShowWarning(string text)
        {
            this.text.text = text;
            transform.DOLocalMoveY(100, 1f)
                .OnComplete(() => Destroy(gameObject));
        }
    }
}
