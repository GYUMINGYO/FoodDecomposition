using System.Collections.Generic;
using UnityEngine;

namespace GM.Managers
{
    public class RecipeManager : MonoBehaviour, IManagerable
    {
        private List<Recipe> recipeList = new();
        private List<Recipe> salesList = new();

        [SerializeField] private Transform content;
        [SerializeField] private GameObject linePrefab;
        [SerializeField] private GameObject recipeCardPrefab;

        private GameObject lineObj;
        int cnt = 3;

        public void Initialized()
        {
        }

        public void Clear()
        {
        }

        public void AddRecipe(Recipe recipe)
        {
            recipeList.Add(recipe);
            if(!recipe.isLock)
                salesList.Add(recipe);
            CardSpawn(recipe);
        }

        public void CardSpawn(Recipe recipe)
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

        public Recipe GetRecipe()
        {
            int idx = Random.Range(0, salesList.Count);
            return salesList[idx];
        }

        public bool SetSalesRecipe(Recipe recipe, bool isSale)
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
    }
}
