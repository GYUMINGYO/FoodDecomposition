using GM.Data;
using GM.Entities;
using Unity.Behavior;
using UnityEngine;
using System.Collections.Generic;
using System;
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
        [SerializeField] private PoolTypeSO poolType;
        [SerializeField] private List<MeshData> meshList;

        [SerializeField] private SkinnedMeshRenderer headRenderer;
        [SerializeField] private SkinnedMeshRenderer bodyRenderer;

        private OrderData _orderData;
        private bool isWait = false;

        public bool IsWait => isWait;
        public PoolTypeSO PoolType => poolType;
        public GameObject GameObject => gameObject;

        protected override void Awake()
        {
            base.Awake();
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
    }
}