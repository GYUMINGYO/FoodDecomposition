using GM.Staffs;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetTarget", story: "[staff] set to [type] [Target]", category: "Action", id: "1281637283cb66ce82308e635cc1e1e2")]
public partial class SetTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<Staff> Staff;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<Enums.InteractableEntityType> Type;

    protected override Status OnStart()
    {
        Target.Value = Staff.Value.GetTarget(Type.Value);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // TODO : 대기 장소 만들기
        if (Target.Value != null)
        {
            return Status.Success;
        }

        Target.Value = Staff.Value.GetTarget(Type.Value);

        return Status.Running;
    }
}

