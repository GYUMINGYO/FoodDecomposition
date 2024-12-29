using GM.Staffs;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetFood", story: "[staff] get food", category: "Action", id: "ccb2e62da11c583649de765aa614dec9")]
public partial class GetFoodAction : Action
{
    [SerializeReference] public BlackboardVariable<Staff> Staff;

    protected override Status OnStart()
    {
        // TODO : 음식 줍기 함수 구현
        return Status.Success;
    }
}

