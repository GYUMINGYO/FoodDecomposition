using GM;
using GM.Managers;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[BlackboardEnum]
public enum SpeakBubbleType
{
    order,
    wait,
    hide
}

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DebugBubble", story: "show [speakBubble] of [type]", category: "Action", id: "1362a4e3ae67b3f9f2b3cedc92268f2d")]
public partial class SpeakBubbleAction : Action
{
    [SerializeReference] public BlackboardVariable<SpeakBubble> SpeakBubble;
    [SerializeReference] public BlackboardVariable<SpeakBubbleType> Type;

    protected override Status OnStart()
    {
        switch (this.Type.Value)
        {
            case SpeakBubbleType.order:
                SpeakBubble.Value.OrderWaitShow();
                break;
            case SpeakBubbleType.wait:
                SpeakBubble.Value.FoodWaitShow();
                break;
            case SpeakBubbleType.hide:
                SpeakBubble.Value.CalculatePreferenceRate();
                break;
        }

        return Status.Success;
    }
}

