using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GM
{
    public class RecipeCard : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Image icon;
        [SerializeField] private Image lockPanel;

        private RecipeSO recipe;

        public void Initialize(RecipeSO recipe)
        {
            this.recipe = recipe;

            nameText.text = recipe.foodName;
            icon.sprite = recipe.icon;

            SetLock(recipe.isLock);
        }

        public void SetLock(bool value)
        {
            lockPanel.color = new Color(0, 0, 0, value ? 0.8f : 0);
        }

        public RecipeSO GetRecipe()
        {
            return recipe;
        }
    }
}
