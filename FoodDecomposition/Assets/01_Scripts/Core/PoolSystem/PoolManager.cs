using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Pool/Manager")]
public class PoolManagerSO : ScriptableObject
{
    public List<PoolTypeSO> poolList = new();
    private Dictionary<string, Pool> _pools; // PoolTypeSO를 string으로 변경
    private Transform _rootTrm;

    public event Action<int> LoadCountEvent;
    public event Action<int, string> LoadMessageEvent;

    public void InitializePool(Transform root)
    {
        _rootTrm = root;
        _pools = new Dictionary<string, Pool>();

        foreach (var poolType in poolList)
        {
            var pool = new Pool(poolType, _rootTrm, poolType.initCount);
            LoadCountEvent?.Invoke(poolType.initCount);
            pool.LoadCompleteEvent += () =>
            {
                LoadMessageEvent?.Invoke(poolType.initCount, $"{poolType.typeName} is loaded");
            };

            _pools.Add(poolType.name, pool); // name을 키로 사용
        }
    }

    public IPoolable Pop(PoolTypeSO type)
    {
        if (_pools.TryGetValue(type.name, out Pool pool)) // name으로 검색
        {
            return pool.Pop();
        }
        return null;
    }

    public void Push(IPoolable item)
    {
        Debug.Assert(item != null, "Item is null");

        if (_pools.TryGetValue(item.PoolType.name, out Pool pool))
        {
            pool.Push(item);
        }
    }
}