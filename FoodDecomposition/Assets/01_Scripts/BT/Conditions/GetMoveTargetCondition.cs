using GM;
using System;
using Unity.Behavior;
using UnityEngine;

[BlackboardEnum]
public enum MoveTargetType
{
    Counter,
    Exit
}

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "GetMoveTarget", story: "get [target] for [type]", category: "Conditions", id: "0d5316b5fcbe73dd4830370e6a551abc")]
public partial class GetMoveTargetCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<MoveTargetType> Type;

    public override bool IsTrue()
    {
        switch (this.Type.Value)
        {
            case MoveTargetType.Counter:
                {
                    Target.Value = MapManager.Instance.GetCountTrm();
                }
                break;
            case MoveTargetType.Exit:
                Target.Value = MapManager.Instance.ExtrenceAndExitTrm;
                break;
        }

        return !(Target.Value == null);
    }
}
