using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/ChefStateChannel")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "ChefStateChannel", message: "change to [state]", category: "Events", id: "b9be6e6459167f2c81923142388525af")]
public partial class ChefStateChannel : EventChannelBase, ICloneable
{
    public delegate void ChefStateChannelEventHandler(ChefState state);
    public event ChefStateChannelEventHandler Event;

    public void SendEventMessage(ChefState state)
    {
        Event?.Invoke(state);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<ChefState> stateBlackboardVariable = messageData[0] as BlackboardVariable<ChefState>;
        var state = stateBlackboardVariable != null ? stateBlackboardVariable.Value : default(ChefState);

        Event?.Invoke(state);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        ChefStateChannelEventHandler del = (state) =>
        {
            BlackboardVariable<ChefState> var0 = vars[0] as BlackboardVariable<ChefState>;
            if (var0 != null)
                var0.Value = state;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as ChefStateChannelEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as ChefStateChannelEventHandler;
    }

    public object Clone()
    {
        return Instantiate(this);
    }
}

