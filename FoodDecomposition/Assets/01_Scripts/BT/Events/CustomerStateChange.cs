using GM;
using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/CustomerStateChange")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "CustomerStateChange", message: "change to [state]", category: "Events", id: "3957ff74acc2080e298d6f751f664d92")]
public partial class CustomerStateChange : EventChannelBase
{
    public delegate void CustomerStateChangeEventHandler(CustomerState state);
    public event CustomerStateChangeEventHandler Event; 

    public void SendEventMessage(CustomerState state)
    {
        Event?.Invoke(state);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<CustomerState> stateBlackboardVariable = messageData[0] as BlackboardVariable<CustomerState>;
        var state = stateBlackboardVariable != null ? stateBlackboardVariable.Value : default(CustomerState);

        Event?.Invoke(state);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        CustomerStateChangeEventHandler del = (state) =>
        {
            BlackboardVariable<CustomerState> var0 = vars[0] as BlackboardVariable<CustomerState>;
            if(var0 != null)
                var0.Value = state;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as CustomerStateChangeEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as CustomerStateChangeEventHandler;
    }
}

