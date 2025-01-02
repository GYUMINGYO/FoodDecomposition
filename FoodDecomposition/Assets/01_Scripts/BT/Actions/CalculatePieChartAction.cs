using GM;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using GM.Staffs;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CalculatePieChart", story: "calculate [pieChart] fill of the [chef]", category: "Action", id: "d3b0766878dcc19841cef16b61c56912")]
public partial class CalculatePieChartAction : Action
{
    [SerializeReference] public BlackboardVariable<PieChart> PieChart;
    [SerializeReference] public BlackboardVariable<Chef> Chef;

    protected override Status OnStart()
    {
        PieChart.Value.StartFillPieChart(Chef.Value.CurrentData.recipe.GetCookingTableTime());
        return Status.Success;
    }
}

