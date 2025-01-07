using GM;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[BlackboardEnum]
public enum SpeakBubbleType
{
    message,
    order,
    wait,
    count,
    hide
}

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DebugBubble", story: "show [debugBubble] with a [message] of [type]", category: "Action", id: "1362a4e3ae67b3f9f2b3cedc92268f2d")]
public partial class DebugBubbleAction : Action
{
    [SerializeReference] public BlackboardVariable<DebugBubble> DebugBubble;
    [SerializeReference] public BlackboardVariable<string> Message;
    [SerializeReference] public BlackboardVariable<SpeakBubbleType> Type;

    protected override Status OnStart()
    {
        switch (this.Type.Value)
        {
            case SpeakBubbleType.message:
                DebugBubble.Value.TextShow(Message.Value);
                break;
            case SpeakBubbleType.order:
                DebugBubble.Value.OrderShow();
                break;
            case SpeakBubbleType.wait:
                break;
            case SpeakBubbleType.count:
                break;
            case SpeakBubbleType.hide:
                DebugBubble.Value.Hide();
                break;
        }

        return Status.Success;
    }
}

