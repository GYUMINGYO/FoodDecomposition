using GM.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetAnimationSpeed", story: "[entityAnimator] set [parameterName] with [animSpeed]", category: "Action", id: "8bf0a6012aa18767ddb67954335adf46")]
public partial class SetAnimationSpeedAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityAnimator> EntityAnimator;
    [SerializeReference] public BlackboardVariable<string> ParameterName;
    [SerializeReference] public BlackboardVariable<float> AnimSpeed = new BlackboardVariable<float>(1f);

    protected override Status OnStart()
    {
        EntityAnimator.Value.SetAnimationSpeed(ParameterName.Value, AnimSpeed.Value);
        return Status.Success;
    }
}

