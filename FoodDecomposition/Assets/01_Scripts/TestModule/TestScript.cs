using GM.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    public class TestScript : MonoBehaviour
    {
        public List<Recipe> recipeList;

        private void Start()
        {
            foreach (Recipe recipe in recipeList)
            {
                Recipe clone = recipe.Clone() as Recipe;
                ManagerHub.Instance.GetManager<RecipeManager>().AddRecipe(clone);
            }
        }
    }
}
