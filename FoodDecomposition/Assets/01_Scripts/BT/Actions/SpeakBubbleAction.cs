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
[NodeDescription(name: "DebugBubble", story: "show [speakBubble] with a [message] of [type]", category: "Action", id: "1362a4e3ae67b3f9f2b3cedc92268f2d")]
public partial class SpeakBubbleAction : Action
{
    [SerializeReference] public BlackboardVariable<SpeakBubble> SpeakBubble;
    [SerializeReference] public BlackboardVariable<string> Message;
    [SerializeReference] public BlackboardVariable<SpeakBubbleType> Type;

    protected override Status OnStart()
    {
        switch (this.Type.Value)
        {
            case SpeakBubbleType.message:
                SpeakBubble.Value.TextShow(Message.Value);
                break;
            case SpeakBubbleType.order:
                SpeakBubble.Value.OrderShow();
                break;
            case SpeakBubbleType.wait:
                break;
            case SpeakBubbleType.count:
                break;
            case SpeakBubbleType.hide:
                SpeakBubble.Value.Hide();
                break;
        }

        return Status.Success;
    }
}

