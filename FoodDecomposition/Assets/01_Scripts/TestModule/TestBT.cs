using Unity.Behavior;
using UnityEngine;

namespace GM
{
    public class TestBT : MonoBehaviour
    {
        [SerializeField] private BehaviorGraphAgent _agent;

        private void Start()
        {
            BlackboardVariable<float> bf;
            _agent.GetVariable("TestFloat", out bf);

            Debug.Log(bf.Value);

            //* 그니까 지금 이게 웨이터를 다 들고 있고 엄
        }
    }
}
