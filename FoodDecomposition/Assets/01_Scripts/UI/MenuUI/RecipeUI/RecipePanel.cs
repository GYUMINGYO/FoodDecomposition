using DG.Tweening;
using GM.Managers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GM
{
    public class RecipePanel : MenuUI
    {
        [SerializeField] private RecipeInfo recipeInfo;
        [SerializeField] private float duration = 0.5f;

        [SerializeField] private Transform content;
        [SerializeField] private GameObject linePrefab;
        [SerializeField] private GameObject recipeCardPrefab;

        private CanvasGroup group;
        private GraphicRaycaster raycaster;
        private EventSystem eventSystem;

        private GameObject lineObj;
        int cnt = 3;

        public override void Awake()
        {
            base.Awake();

            group = GetComponent<CanvasGroup>();
            raycaster = transform.root.GetComponent<GraphicRaycaster>();
            eventSystem = EventSystem.current;
        }

        private void Start()
        {
            ManagerHub.Instance.GetManager<RecipeManager>().CardSpawnEvent += CardSpawn;
        }

        public override void Open()
        {
            if (isOpen)
                return;

            isOpen = true;
            transform.DOKill();

            group.alpha = 1;
            transform.DOLocalMoveY(0, duration)
                .OnComplete(() => group.blocksRaycasts = true);
        }

        public override void Close()
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

        public void CardSpawn(RecipeSO recipe)
        {
            if (cnt == 0 || lineObj == null)
            {
                lineObj = Instantiate(linePrefab, content.transform);
                cnt = 3;
            }

            RecipeCard card = Instantiate(recipeCardPrefab, lineObj.transform).GetComponent<RecipeCard>();
            card.Initialize(recipe);
            cnt--;
        }

        private void OnDestroy()
        {
            ManagerHub.Instance.GetManager<RecipeManager>().CardSpawnEvent -= CardSpawn;
        }
    }
}
