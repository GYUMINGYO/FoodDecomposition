using UnityEngine;
using System.Collections.Generic;
using MKDir;

namespace GM.Managers
{
    // TODO : 이게 필요 할까?
    public class RecipeManager : MonoSingleton<RecipeManager>
    {
        public List<Recipe> recipeList;

        public void AddRecipe(Recipe recipe)
        {
            recipeList.Add(recipe);
        }

        public Recipe GetRecipe()
        {
            int idx = Random.Range(0, recipeList.Count);
            return recipeList[idx];
        }
    }
}
