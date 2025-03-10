using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GM.Managers
{
    public class RecipeManager : MonoBehaviour, IManagerable
    {
        public List<RecipeSO> recipeList;
        private List<RecipeSO> salesList = new();

        [SerializeField] private Transform content;
        [SerializeField] private GameObject linePrefab;
        [SerializeField] private GameObject recipeCardPrefab;

        private GameObject lineObj;
        int cnt = 3;

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

        public void Initialized()
        {
        }

        public void Clear()
        {
        }

        public void AddRecipe(RecipeSO recipe)
        {
            recipeList.Add(recipe);
            if(!recipe.isLock)
                salesList.Add(recipe);
            CardSpawn(recipe);
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
    }
}
