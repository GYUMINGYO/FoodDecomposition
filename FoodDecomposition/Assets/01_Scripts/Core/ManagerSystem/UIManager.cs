using DG.Tweening;
using GM.EventSystem;
using GM.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum MenuType
{
    staff,
    fablic,
    client,
    recipe,
    setting
}

namespace GM
{
    public class UIManager : MonoBehaviour, IManagerable
    {
        [SerializeField] private TextMeshProUGUI moneyText;

        [Header("Panel")]
        [SerializeField] private RecipePanel recipePanel;

        [Header("Button")]
        [SerializeField] private Button menuBtn;
        [SerializeField] private Button staffBtn;
        [SerializeField] private Button fablicBtn;
        [SerializeField] private Button clientBtn;
        [SerializeField] private Button recipeBtn;
        [SerializeField] private Button settingBtn;

        [Header("setting")]
        [SerializeField] private float duration = 1f;

        private IMenuUI currentUI;
        private bool isMenuOpen = false;

        private void Start()
        {
            menuBtn.onClick.AddListener(SetMenuListVisible);

            staffBtn.onClick.AddListener(() => OpenMenu(MenuType.staff));
            fablicBtn.onClick.AddListener(() => OpenMenu(MenuType.fablic));
            clientBtn.onClick.AddListener(() => OpenMenu(MenuType.client));
            recipeBtn.onClick.AddListener(() => OpenMenu(MenuType.recipe));
            settingBtn.onClick.AddListener(() => OpenMenu(MenuType.setting));
        }

        private void SetMenuListVisible()
        {
            if (isMenuOpen)
            {
                staffBtn.transform.DOLocalMoveY(-490, duration);
                fablicBtn.transform.DOLocalMoveY(-490, duration);
                clientBtn.transform.DOLocalMoveY(-490, duration);
                recipeBtn.transform.DOLocalMoveY(-490, duration);
                settingBtn.transform.DOLocalMoveY(-490, duration);
            }
            else
            {
                staffBtn.transform.DOLocalMoveY(190, duration);
                fablicBtn.transform.DOLocalMoveY(80, duration);
                clientBtn.transform.DOLocalMoveY(-30, duration);
                recipeBtn.transform.DOLocalMoveY(-140, duration);
                settingBtn.transform.DOLocalMoveY(-250, duration);
            }

            isMenuOpen = !isMenuOpen;
        }

        private void OpenMenu(MenuType type)
        {
            if(currentUI != null)
                CloseMenu();

            switch (type)
            {
                case MenuType.staff:
                    break;
                case MenuType.fablic:
                    break;
                case MenuType.client:
                    break;
                case MenuType.recipe:
                    currentUI = recipePanel;
                    break;
                case MenuType.setting:
                    break;
            }

            currentUI.Open();
        }

        public void CloseMenu()
        {
            currentUI.Close();
            currentUI = null;
        }

        public void Clear()
        {
        }

        public void Initialized()
        {
            SetMoneyUI(0);
        }

        public void SetMoneyUI(float money)
        {
            moneyText.text = $"${money}";
        }

        private void OnDestroy()
        {
            menuBtn.onClick.RemoveListener(SetMenuListVisible);

            staffBtn.onClick.RemoveAllListeners();
            fablicBtn.onClick.RemoveAllListeners();
            clientBtn.onClick.RemoveAllListeners();
            recipeBtn.onClick.RemoveAllListeners();
            settingBtn.onClick.RemoveAllListeners();
        }
    }
}