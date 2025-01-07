using GM;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CheckWaitingEnd", story: "check waiting end for [speakBubble]", category: "Conditions", id: "182bc5569b75837bf1917342edb74d94")]
public partial class CheckWaitingEndCondition : Condition
{
    [SerializeReference] public BlackboardVariable<SpeakBubble> SpeakBubble;

    public override bool IsTrue()
    {
        return SpeakBubble.Value.IsWaiting;
    }
}
