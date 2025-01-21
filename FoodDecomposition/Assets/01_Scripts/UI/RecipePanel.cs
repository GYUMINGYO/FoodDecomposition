using DG.Tweening;
using UnityEngine;

namespace GM.EventSystem
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
                .OnComplete(() => group.interactable = true);
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
                    group.interactable = false; 
                });
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                Close();
            }
        }
    }
}
