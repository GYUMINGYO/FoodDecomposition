using UnityEngine;

namespace GM.Managers
{
    public class ManagerHub : MonoBehaviour
    {
        public static bool Initialized { get; set; } = false;

        private static ManagerHub s_instacne;
        public static ManagerHub Instance { get { Init(); return s_instacne; } }


        #region Contents 
        private WaiterManager _waiter = new WaiterManager();
        private RestourantManager _restourant = new RestourantManager();

        public static WaiterManager WaiterManager { get { return Instance?._waiter; } }
        public static RestourantManager RestourantManager { get { return Instance?._restourant; } }
        #endregion

        public static void Init()
        {
            if (s_instacne == null && Initialized == false)
            {
                Initialized = true;

                GameObject obj = GameObject.Find("@Managers");
                if (obj == null)
                {
                    obj = new GameObject { name = "@Managers" };
                    obj.AddComponent<ManagerHub>();
                }
                DontDestroyOnLoad(obj);

                s_instacne = obj.GetComponent<ManagerHub>();
                WaiterManager.Init();
                RestourantManager.Init();
            }
        }

        private void Update()
        {
            WaiterManager.Update();
        }

        public static void Clear()
        {
            WaiterManager.Clear();
            RestourantManager.Clear();
        }
    }
}
