using GM;
using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/WaiterStateChange")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "WaiterStateChange", message: "change to [state]", category: "Events", id: "4a450d351caf103bb71687852b05cb37")]
public partial class WaiterStateChange : EventChannelBase, ICloneable
{
    public delegate void WaiterStateChangeEventHandler(WaiterState state);
    public event WaiterStateChangeEventHandler Event;

    public void SendEventMessage(WaiterState state)
    {
        Event?.Invoke(state);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<WaiterState> stateBlackboardVariable = messageData[0] as BlackboardVariable<WaiterState>;
        var state = stateBlackboardVariable != null ? stateBlackboardVariable.Value : default(WaiterState);

        Event?.Invoke(state);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        WaiterStateChangeEventHandler del = (state) =>
        {
            BlackboardVariable<WaiterState> var0 = vars[0] as BlackboardVariable<WaiterState>;
            if (var0 != null)
                var0.Value = state;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as WaiterStateChangeEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as WaiterStateChangeEventHandler;
    }

    public object Clone()
    {
        return Instantiate(this);
    }
}

