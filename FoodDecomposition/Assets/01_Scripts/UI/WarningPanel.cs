using DG.Tweening;
using TMPro;
using UnityEngine;

namespace GM
{
    public class WarningPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        public void ShowText(string msg)
        {
            WarningPanel warningPanel = Instantiate(gameObject, transform.parent).GetComponent<WarningPanel>();
            warningPanel.ShowWarning(msg);
        }

        private void ShowWarning(string msg)
        {
            text.text = msg;
            text.enabled = true;

            transform.DOLocalMoveY(100, 1f)
                .OnComplete(() => Destroy(gameObject));
        }
    }
}
