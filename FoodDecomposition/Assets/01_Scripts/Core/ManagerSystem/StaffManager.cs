using GM.Data;
using GM.GameEventSystem;
using GM.Staffs;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GM.Managers
{
    public class StaffManager : MonoBehaviour, IManagerable, IManagerUpdateable
    {
        [SerializeField] private GameEventChannelSO _gameCycleChannel;
        [SerializeField] private Transform _entranceTransform;

        private List<StaffHandler> _staffList;
        private LinkedList<OrderData> _orderList;
        private Queue<OrderData> _recipeList;

        void Awake()
        {
            _gameCycleChannel.AddListener<RestourantCycleEvent>(HandleRestourantCycleEvent);
        }

        public void Initialized()
        {
            _staffList = new List<StaffHandler>();
            _orderList = new LinkedList<OrderData>();
            _recipeList = new Queue<OrderData>();

            foreach (var staff in MonoBehaviour.FindObjectsByType<StaffHandler>(FindObjectsSortMode.None))
            {
                _staffList.Add(staff);
            }
        }

        public void Clear()
        {
            _gameCycleChannel.RemoveListener<RestourantCycleEvent>(HandleRestourantCycleEvent);

            _staffList.Clear();
            _orderList.Clear();
            _recipeList.Clear();
        }

        private void HandleRestourantCycleEvent(RestourantCycleEvent evt)
        {
            if (evt.open)
            {
                // GenerateStaff
                StartCoroutine(GenerateStaff());
            }
            else
            {
                // EndStaff
                for (int i = 0; i < _staffList.Count; ++i)
                {
                    _staffList[i].LeaveWork();
                }
            }
        }

        private IEnumerator GenerateStaff()
        {
            for (int i = 0; i < _staffList.Count; ++i)
            {
                _staffList[i].gameObject.SetActive(true);
                Staff staff = _staffList[i].GetStaff(_staffList[i].Type);
                staff.transform.position = _entranceTransform.position;
                staff.transform.rotation = _entranceTransform.rotation;

                yield return new WaitForSeconds(0.7f);
            }

            _gameCycleChannel.RaiseEvent(GameCycleEvents.ReadyToRestourant);
            yield return null;
        }

        public void Update()
        {
            if (_staffList.Count <= 0) return;

            ChefGiveWork();
            WaiterGiveWork();
        }

        /// <summary>
        /// Add Data for WaiterManager Order
        /// </summary>
        /// <param name="data">Order data</param>
        public void AddOrderData(OrderData data)
        {
            Debug.Assert(data.type != OrderType.Null, "OrderData Type is Null");

            if (data.type == OrderType.Cook)
            {
                _recipeList.Enqueue(data);
                return;
            }

            _orderList.AddLast(data);
        }

        public void AddStaff(StaffHandler staff)
        {
            _staffList.Add(staff);
        }

        public void RemoveStaff(StaffHandler staff)
        {
            _staffList.Remove(staff);
        }

        private void ChefGiveWork()
        {
            if (_recipeList.Count <= 0) return;

            Chef chef = CheckWorking(StaffType.Chef) as Chef;
            chef?.StartWork(ChefState.COOK, _recipeList.Dequeue());
        }

        private void WaiterGiveWork()
        {
            if (_orderList.Count <= 0) return;

            Waiter waiter = CheckWorking(StaffType.Waiter) as Waiter;
            if (waiter == default) return;

            foreach (OrderType workType in waiter.WorkPriority)
            {
                if (waiter.IsWorking == true) break;

                // Continuous calculation processing 
                if (workType == OrderType.Count)
                {
                    waiter = _staffList.OfType<Waiter>().FirstOrDefault(x => x.IsWorking == false && x.currentWaiterState == WaiterState.COUNT) ?? waiter;
                }

                OrderData data = _orderList.FirstOrDefault(x => x.type == workType);

                if (data != default)
                {
                    switch (data.type)
                    {
                        case OrderType.Order:
                            waiter.StartWork(WaiterState.ORDER, data);
                            break;
                        case OrderType.Count:
                            waiter.StartWork(WaiterState.COUNT, data);
                            break;
                        case OrderType.Serving:
                            waiter.StartWork(WaiterState.SERVING, data);
                            break;
                    }
                    _orderList.Remove(data);
                }
            }
        }

        private Staff CheckWorking(StaffType type)
        {
            return _staffList.Where(x => x.Type == type && x.IsChange == false).ToList().FirstOrDefault(x => x.GetStaff(type).IsWorking == false)?.GetStaff(type);
        }

        public int GetStaffCount()
        {
            return _staffList.Count;
        }
    }
}
