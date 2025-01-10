using GM.Data;
using GM.Entities;
using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = System.Action;
using Random = UnityEngine.Random;

[Serializable]
public struct MeshData
{
    public Mesh head;
    public Mesh body;
}

namespace GM
{
    [BlackboardEnum]
    public enum CustomerState
    {
        Order,
        Food,
        Counter,
        Exit
    }

    public class Customer : Entity, IPoolable
    {
        public Action customerExitEvent;

        [SerializeField] private PoolTypeSO poolType;
        [SerializeField] private List<MeshData> meshList;

        [SerializeField] private SkinnedMeshRenderer headRenderer;
        [SerializeField] private SkinnedMeshRenderer bodyRenderer;

        public OrderData OrderData => _orderData;
        private OrderData _orderData;
        private bool isWait = false;
        private bool isLine = false;

        public bool IsWait => isWait;
        public PoolTypeSO PoolType => poolType;
        public GameObject GameObject => gameObject;

        public bool isTest = false;

        protected override void Awake()
        {
            base.Awake();

            if (!isTest)
                gameObject.SetActive(false);
        }

        public void SetOrderData(OrderData orderData) => _orderData = orderData;

        public float GetSellPrice() =>
            _orderData.Equals(default(OrderData)) ? 0 : _orderData.recipe.sellPrice;

        public OrderData GetOrderData() =>
            _orderData.Equals(default(OrderData)) ? default : _orderData;

        public void SetWait(bool value) => isWait = value;

        public void SetUpPool(Pool pool) { }

        public void ResetItem()
        {
            SetMesh();
            SetWait(false);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        private void SetMesh()
        {
            int randIdx = Random.Range(0, meshList.Count);

            headRenderer.sharedMesh = meshList[randIdx].head;
            bodyRenderer.sharedMesh = meshList[randIdx].body;
        }

        public void SetIsLine(bool value) => isLine = value;
    }
}