using GM;
using GM.InteractableEntities;
using GM.Managers;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FindTable", story: "find [table] [chair] [exit] for [customer]", category: "Action", id: "82f026d4ef14a695c07ec99879662be6")]
public partial class FindTableAction : Action
{
    [SerializeReference] public BlackboardVariable<Table> Table;
    [SerializeReference] public BlackboardVariable<Transform> Chair;
    [SerializeReference] public BlackboardVariable<Transform> Exit;
    [SerializeReference] public BlackboardVariable<Customer> Customer;

    private float time = 0;
    private Status status = Status.Running;

    protected override Status OnStart()
    {
        time = 0;
        status = Status.Running;

        if (Exit.Value == null)
        {
            InteractableEntity target;
            ManagerHub.Instance.GetManager<RestourantManager>().GetStaticFirstInteractableEntity(Enums.InteractableEntityType.Exit, out target);
            SingleTableEntity exitEntity = target as SingleTableEntity;
            Exit.Value = exitEntity.EntityTransform;
        }

        ManagerHub.Instance.GetManager<RestourantManager>().SetIsSeatFull(true);

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        time += Time.deltaTime;

        Table.Value = ManagerHub.Instance.GetManager<RestourantManager>().GetTable();
        if (Table.Value != null)
        {
            Chair.Value = Table.Value.GetChair();
            ManagerHub.Instance.GetManager<RestourantManager>().SetIsSeatFull(false);

            status = Status.Success;
        }

        return status;
    }

    protected override void OnEnd()
    {
        int amount = (int)time / 5;
        if (amount > 0)
        {
            ManagerHub.Instance.GetManager<PreferenceManager>().ModifyPreferenceRate(Customer.Value, amount);
        }
    }
}

