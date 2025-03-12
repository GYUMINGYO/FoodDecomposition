using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GM.Managers
{
    public class RecipeManager : MonoBehaviour, IManagerable
    {
        public Action<RecipeSO> CardSpawnEvent;

        public List<RecipeSO> recipeList;
        private List<RecipeSO> salesList = new();

        private void Start()
        {
            List<RecipeSO> compareList = new List<RecipeSO>(recipeList);
            recipeList.Clear();

            foreach (RecipeSO recipe in compareList)
            {
                RecipeSO clone = recipe.Clone() as RecipeSO;
                AddRecipe(clone);
            }
        }

        public void AddRecipe(RecipeSO recipe)
        {
            recipeList.Add(recipe);
            if(!recipe.isLock)
                salesList.Add(recipe);
            CardSpawnEvent?.Invoke(recipe);
        }

        public RecipeSO GetRecipe()
        {
            int idx = Random.Range(0, salesList.Count);
            return salesList[idx].Clone() as RecipeSO;
        }

        public bool SetSalesRecipe(RecipeSO recipe, bool isSale)
        {
            if(isSale)
            {
                salesList.Add(recipe);
            }
            else
            {
                if(salesList.Count == 1)
                {
                    return false;
                }

                salesList.Remove(recipe);
            }
            return true;
        }

        public void Initialized()
        {
        }

        public void Clear()
        {
        }
    }
}
