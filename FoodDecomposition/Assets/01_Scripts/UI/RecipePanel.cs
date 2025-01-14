using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GM
{
    public class RecipePanel : MonoBehaviour, IMenuUI
    {
        [SerializeField] private float duration = 0.5f;

        private CanvasGroup group;

        bool isOpen = false;

        private void Awake()
        {
            group = GetComponent<CanvasGroup>();
        }

        public void Open()
        {
            if (isOpen)
                return;

            isOpen = true;
            transform.DOKill();

            group.alpha = 1;
            transform.DOLocalMoveY(0, duration + 0.2f)
                .SetEase(Ease.OutBounce)
                .OnComplete(() => group.blocksRaycasts = true);
        }

        public void Close()
        {
            if (!isOpen)
                return;

            isOpen = false;
            transform.DOKill();

            transform.DOLocalMoveY(900, duration)
                .OnComplete(() =>
                {
                    group.alpha = 0;
                    group.blocksRaycasts = false;
                });
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Close();
            }
        }
    }
}
