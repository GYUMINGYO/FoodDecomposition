using System.Collections.Generic;
using System.Linq;
using GM.Data;
using GM.Managers;
using GM.Staffs;
using UnityEngine;

namespace GM.Manager
{
    public class ChefManager : IManagerable, IManagerUpdateable
    {
        // TODO : 이거 너무 겹치니까 waiter랑 합친 StaffManager로 만드는게 날 듯
        public List<Chef> chefList;
        private Queue<OrderData> _recipeList;

        public void Initialized()
        {
            chefList = new List<Chef>();
            _recipeList = new Queue<OrderData>();

            foreach (var chef in MonoBehaviour.FindObjectsByType<Chef>(FindObjectsSortMode.None))
            {
                chefList.Add(chef);
            }
        }

        public void Update()
        {
            if (_recipeList.Count > 0)
            {
                CheckWorking()?.StartWork(ChefState.COOK, DequeueRecipeData());
            }
        }

        public void AddOrderData(OrderData data)
        {
            Debug.Assert(data.type != OrderType.Null, "OrderData Type is Null");

            _recipeList.Enqueue(data);
        }

        private OrderData DequeueRecipeData()
        {
            if (_recipeList.Count <= 0)
            {
                return default;
            }

            return _recipeList.Dequeue();
        }

        private Chef CheckWorking() =>
            chefList.FirstOrDefault(x => x.IsWorking == false);

        public void Clear()
        {
            chefList.Clear();
            _recipeList.Clear();
        }
    }
}
