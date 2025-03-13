using GM;
using GM.InteractableEntities;
using GM.Managers;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IfCanFindTable", story: "find [table] [chair] [exit] for [customer]", category: "Conditions", id: "feee94772bb63b8b9926912882f12980")]
public partial class IfCanFindTableCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Table> Table;
    [SerializeReference] public BlackboardVariable<Transform> Chair;
    [SerializeReference] public BlackboardVariable<Transform> Exit;
    [SerializeReference] public BlackboardVariable<Customer> Customer;

    private bool isEnd;
    private float time;

    public override bool IsTrue()
    {
        if (isEnd) return false;

        Table.Value = ManagerHub.Instance.GetManager<RestourantManager>().GetTable();

        if (Table.Value != null)
        {
            Chair.Value = Table.Value.GetChair();
            ManagerHub.Instance.GetManager<RestourantManager>().SetIsSeatFull(false);
            isEnd = true;

            int amount = (int)time / 5;
            if (amount > 0)
            {
                ManagerHub.Instance.GetManager<PreferenceManager>().ModifyPreferenceRate(Customer.Value, amount);
                Debug.Log(amount);
            }
            return true;
        }
        else
        {
            time += Time.deltaTime;

            return false;
        }
    }

    public override void OnStart()
    {
        isEnd = false;
        time = 0;

        if (Exit.Value == null)
        {
            InteractableEntity target;
            ManagerHub.Instance.GetManager<RestourantManager>().GetStaticFirstInteractableEntity(Enums.InteractableEntityType.Exit, out target);
            SingleTableEntity exitEntity = target as SingleTableEntity;
            Exit.Value = exitEntity.EntityTransform;
        }

        ManagerHub.Instance.GetManager<RestourantManager>().SetIsSeatFull(true);
    }
}
