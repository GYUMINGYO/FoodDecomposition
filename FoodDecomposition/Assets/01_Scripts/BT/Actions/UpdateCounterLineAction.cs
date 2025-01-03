using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using GM;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UpdateCounterLine", story: "[customer] [counter] queue", category: "Action", id: "bfd25e7b0af84761b5ab26c36f182df0")]
public partial class UpdateCounterLineAction : Action
{
    [SerializeReference] public BlackboardVariable<Customer> Customer;
    [SerializeReference] public BlackboardVariable<Transform> Counter;

    protected override Status OnStart()
    {
        Customer.Value.updateLineEvent += UpdateLine;

        return Status.Running;
    }

    private void UpdateLine(Transform trm)
    {
        Counter.Value = trm;
    }

    protected override void OnEnd()
    {
        Customer.Value.updateLineEvent -= UpdateLine;
    }
}

