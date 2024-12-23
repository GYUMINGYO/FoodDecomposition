using GM;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DebugBubble", story: "show [debugBubble] with a [message] if [value]", category: "Action", id: "1362a4e3ae67b3f9f2b3cedc92268f2d")]
public partial class DebugBubbleAction : Action
{
    [SerializeReference] public BlackboardVariable<DebugBubble> DebugBubble;
    [SerializeReference] public BlackboardVariable<string> Message;
    [SerializeReference] public BlackboardVariable<bool> Value;

    protected override Status OnStart()
    {
        if (Value.Value)
            DebugBubble.Value.Show(Message.Value);
        else
            DebugBubble.Value.Hide();
        return Status.Success;
    }
}

