using MKDir;
using System;
using UnityEngine.SceneManagement;

namespace GM
{
    public enum PoolEnumType
    {
        AttackLoad, InteractiveObject, Axe, RanageAttack, Enemy, Environment, Effect
    }

    public class SingletonePoolManager : MonoSingleton<SingletonePoolManager>
    {
        public event Action OnAllPushEvent;
        public PoolManagerSO poolManager;

        protected override void Awake()
        {
            base.Awake();

            poolManager.InitializePool(this.transform);
        }

        public IPoolable Pop(PoolTypeSO type)
        {
            return poolManager.Pop(type);
        }

        public void Push(IPoolable item)
        {
            poolManager.Push(item);
        }

        public void AllPush()
        {
            OnAllPushEvent?.Invoke();
        }

    }
}
