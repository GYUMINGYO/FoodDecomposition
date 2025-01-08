using GM;
using GM.Managers;
using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.Assertions.Must;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IfCanFindTable", story: "find [table] and [chair] and [exit]", category: "Conditions", id: "feee94772bb63b8b9926912882f12980")]
public partial class IfCanFindTableCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Table> Table;
    [SerializeReference] public BlackboardVariable<Transform> Chair;
    [SerializeReference] public BlackboardVariable<Transform> Exit;

    public override bool IsTrue()
    {
        Table.Value = ManagerHub.Instance.GetManager<RestourantManager>().GetTable();

        if (Table.Value != null)
        {
            Chair.Value = Table.Value.GetChair();
            ManagerHub.Instance.GetManager<MapManager>().SetIsSeatFull(false);
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void OnStart()
    {
        if (Exit.Value == null)
        {
            Exit.Value = ManagerHub.Instance.GetManager<MapManager>().ExitTrm;
        }

        ManagerHub.Instance.GetManager<MapManager>().SetIsSeatFull(true);
    }
}
