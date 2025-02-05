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

        private Recipe recipe;

        public void Initialize(Recipe recipe)
        {
            this.recipe = recipe;

            nameText.text = recipe.type.ToString();
            icon.sprite = recipe.icon;

            SetLock(recipe.isLock);
        }

        public void SetLock(bool value)
        {
            lockPanel.color = new Color(0, 0, 0, value ? 0.8f : 0);
        }

        public Recipe GetRecipe()
        {
            return recipe;
        }
    }
}
