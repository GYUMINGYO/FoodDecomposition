using GM.Managers;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FindChair", story: "find [chair]", category: "Action", id: "4d6b0381ab560da289de714085f5f4c7")]
public partial class FindChairAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Chair;

    protected override Status OnStart()
    {
        Chair.Value = ManagerHub.Instance.GetManager<RestourantManager>().GetChiar();

        if(Chair.Value == null)
        {
            return Status.Failure;
        }
        else
        {
            return Status.Success;
        }
    }
}

