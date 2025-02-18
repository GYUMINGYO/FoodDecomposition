using GM.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GM
{
    public class RecipeInfo : MonoBehaviour
    {
        [SerializeField] private CanvasGroup group;
        [SerializeField] private Image lockPanel;

        [Header("")]
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Image foodIcon;
        [SerializeField] private TextMeshProUGUI sellPriceText;
        [SerializeField] private TextMeshProUGUI materialCostText;
        [SerializeField] private TextMeshProUGUI researchFundsText;

        [Header("")]
        [SerializeField] private Button increaseBtn;
        [SerializeField] private Button decreaseBtn;
        [SerializeField] private Button researchBtn; 
        [SerializeField] private Button SetSalesBtn;
        [SerializeField] private Image salesImage;
        [SerializeField] private TextMeshProUGUI salesText;

        private Recipe recipe;
        private RecipeCard card;

        private void Start()
        {
            increaseBtn.onClick.AddListener(() => ApplySellPrice(1));
            decreaseBtn.onClick.AddListener(() => ApplySellPrice(-1));
            researchBtn.onClick.AddListener(ResearchRecipe);
            SetSalesBtn.onClick.AddListener(SetSales);

            Hide();
        }

        public void Show(RecipeCard card)
        {
            this.card = card;
            this.recipe = card.GetRecipe();
            SetLock(recipe.isLock);
            UpdateSalesBtn();

            nameText.text = recipe.type.ToString();
            foodIcon.sprite = recipe.icon;
            sellPriceText.text = $"가격:{recipe.sellPrice}";
            materialCostText.text = $"재료비:{recipe.materialCost}";
            researchFundsText.text = $"연구:{recipe.researchFunds}";

            group.alpha = 1;
            group.blocksRaycasts = true;
        }

        public void Hide()
        {
            group.alpha = 0;
            group.blocksRaycasts = false;
        }

        private void ApplySellPrice(float price)
        {
            recipe.sellPrice += price;
            sellPriceText.text = $"가격:{recipe.sellPrice}";
        }

        private void ResearchRecipe()
        {
            DataManager dataManager = ManagerHub.Instance.GetManager<DataManager>();
            if (dataManager.Money >= recipe.researchFunds)
                dataManager.SubtractMoney(recipe.researchFunds);
            else
                return;

            recipe.isLock = false;
            SetLock(false);
            card.SetLock(false);
            SetSales();
        }

        private void SetLock(bool value)
        {
            if (value)
            {
                lockPanel.color = new Color(0, 0, 0, 0.8f);
                researchBtn.gameObject.SetActive(true);
                SetSalesBtn.gameObject.SetActive(false);
                increaseBtn.gameObject.SetActive(false);
                decreaseBtn.gameObject.SetActive(false);
            }
            else
            {
                lockPanel.color = new Color(0, 0, 0, 0);
                researchBtn.gameObject.SetActive(false);
                SetSalesBtn.gameObject.SetActive(true);
                increaseBtn.gameObject.SetActive(true);
                decreaseBtn.gameObject.SetActive(true);
            }
        }

        private void SetSales()
        {
            if (ManagerHub.Instance.GetManager<RecipeManager>().SetSalesRecipe(recipe, !recipe.isSale))
            {
                recipe.isSale = !recipe.isSale;
                UpdateSalesBtn();
            }
        }

        private void UpdateSalesBtn()
        {
            if (recipe.isSale)
            {
                salesImage.color = Color.green;
                salesText.text = "판매중";
            }
            else
            {
                salesImage.color = Color.white;
                salesText.text = "판매하기";
            }
        }
    }
}
