using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/OrderEvent")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "OrderEvent", message: "order", category: "Events", id: "bdf760912359be39a55b95bc6de0911c")]
public partial class OrderEvent : EventChannelBase
{
    public delegate void OrderEventEventHandler();
    public event OrderEventEventHandler Event; 

    public void SendEventMessage()
    {
        Event?.Invoke();
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        Event?.Invoke();
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        OrderEventEventHandler del = () =>
        {
            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as OrderEventEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as OrderEventEventHandler;
    }
}

