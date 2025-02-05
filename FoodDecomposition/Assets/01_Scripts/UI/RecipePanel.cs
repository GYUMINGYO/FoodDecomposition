using DG.Tweening;
using GM.Managers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GM
{
    public class RecipePanel : MonoBehaviour, IMenuUI
    {
        [SerializeField] private RecipeInfo recipeInfo;
        [SerializeField] private float duration = 0.5f;

        private CanvasGroup group;
        private GraphicRaycaster raycaster;
        private EventSystem eventSystem;

        bool isOpen = false;

        private void Awake()
        {
            group = GetComponent<CanvasGroup>();
            raycaster = transform.root.GetComponent<GraphicRaycaster>();
            eventSystem = EventSystem.current;
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
            if (Input.GetMouseButtonDown(0))
            {
                if (eventSystem.IsPointerOverGameObject())
                {
                    PointerEventData pointerData = new PointerEventData(eventSystem)
                    {
                        position = Input.mousePosition
                    };

                    List<RaycastResult> results = new List<RaycastResult>();
                    raycaster.Raycast(pointerData, results);

                    if (results[0].gameObject.transform.parent.TryGetComponent(out RecipeCard recipeCard))
                    {
                        recipeInfo.Show(recipeCard);
                    }
                    else if (results[0].gameObject.CompareTag("UIBackgrround"))
                    {
                        recipeInfo.Hide();
                    }
                }
                else
                {
                    Close();
                }
            }
        }
    }
}
