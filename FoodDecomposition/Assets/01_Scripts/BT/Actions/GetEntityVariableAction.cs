using GM.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using GM.Staffs;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetEntityVariable", story: "get variable from [entity]", category: "Action", id: "b70d03331eac8d8dc36488ff752ca813")]
public partial class GetEntityVariableAction : Action
{
    [SerializeReference] public BlackboardVariable<Entity> Entity;

    protected override Status OnStart()
    {
        // TODO : 이게 맞지는 않는데 이거 방식 어떻게 해야 할까 상의좀
        //* 문제는 나는 BT를 변수로 들고 있으며 사용하고 있고 Customer는 구조가 다르다는 거임

        Staff staff = Entity.Value as Staff;
        staff.SetVariable("AnimTrigger", staff.GetCompo<EntityAnimatorTrigger>());
        staff.SetVariable("EntityAnimator", staff.GetCompo<EntityAnimator>());

        return Status.Running;
    }
}

