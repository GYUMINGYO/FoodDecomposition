using UnityEngine;
using System.Collections.Generic;
using MKDir;

namespace GM.Manager
{
    public class RecipeManager : MonoSingleton<RecipeManager>
    {
        public List<Recipe> recipeList;

        protected override void Awake()
        {
            base.Awake();
            recipeList = new List<Recipe>();
        }

        public void AddRecipe(Recipe recipe)
        {
            recipeList.Add(recipe);
        }

        public Recipe GetRecipe()
        {
            return recipeList[Random.Range(0, recipeList.Count)];
        }
    }
}
