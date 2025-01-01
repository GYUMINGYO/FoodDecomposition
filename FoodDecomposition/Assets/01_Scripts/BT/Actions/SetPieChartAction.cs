using GM;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using GM.Staffs;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetPieChart", story: "set [pieChart] of the [chef]", category: "Action", id: "4647d81cf6b8b66330e90bdb6cc10014")]
public partial class SetPieChartAction : Action
{
    [SerializeReference] public BlackboardVariable<PieChart> PieChart;
    [SerializeReference] public BlackboardVariable<Chef> Chef;

    protected override Status OnStart()
    {
        PieChart.Value.SetPieChart(Chef.Value.CurrentData.recipe.GetCookingTableCount());
        return Status.Success;
    }
}

