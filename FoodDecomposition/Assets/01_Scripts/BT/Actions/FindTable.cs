using GM;
using GM.InteractableEntities;
using GM.Managers;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FindTable", story: "find [table] and [chair] and [exit]", category: "Action", id: "4d6b0381ab560da289de714085f5f4c7")]
public partial class FindChairAction : Action
{
    [SerializeReference] public BlackboardVariable<Table> Table;
    [SerializeReference] public BlackboardVariable<Transform> Chair;
    [SerializeReference] public BlackboardVariable<Transform> Exit;
    protected override Status OnStart()
    {
        if (Exit.Value == null)
        {
            InteractableEntity target;
            ManagerHub.Instance.GetManager<RestourantManager>().GetStaticFirstInteractableEntity(Enums.InteractableEntityType.Exit, out target);
            SingleTableEntity exitEntity = target as SingleTableEntity;
            Exit.Value = exitEntity.EntityTransform;
        }

        Table.Value = ManagerHub.Instance.GetManager<RestourantManager>().GetTable();
        if (Table.Value != null)
        {
            Chair.Value = Table.Value.GetChair();
            return Status.Success;
        }
        else
        {
            return Status.Failure;
        }
    }
}

