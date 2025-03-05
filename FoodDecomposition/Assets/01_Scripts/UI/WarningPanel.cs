using DG.Tweening;
using TMPro;
using UnityEngine;

namespace GM
{
    public class WarningPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        public void ShowText()
        {
            WarningPanel warningPanel = Instantiate(gameObject, transform.parent).GetComponent<WarningPanel>();
            warningPanel.ShowWarning();
        }

        private void ShowWarning()
        {
            text.enabled = true;

            transform.DOLocalMoveY(100, 1f)
                .OnComplete(() => Destroy(gameObject));
        }
    }
}
