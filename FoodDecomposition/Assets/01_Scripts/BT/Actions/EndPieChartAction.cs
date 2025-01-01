using GM;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "EndPieChart", story: "end [pieChart]", category: "Action", id: "8656aba76f73d4440180952488603f01")]
public partial class EndPieChartAction : Action
{
    [SerializeReference] public BlackboardVariable<PieChart> PieChart;

    protected override Status OnStart()
    {
        PieChart.Value.EndPieChart();
        return Status.Success;
    }
}

