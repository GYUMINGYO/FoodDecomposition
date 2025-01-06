using GM.Entities;
using System;
using GM.Staffs;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetChefVariable", story: "get variable from [entity]", category: "Action", id: "ce61abf8e550898a062cf8a41765baf1")]
public partial class GetChefVariableAction : Action
{
    [SerializeReference] public BlackboardVariable<Entity> Entity;

    protected override Status OnStart()
    {
        Chef chef = Entity.Value as Chef;
        chef.SetVariable("AnimTrigger", chef.GetCompo<EntityAnimatorTrigger>());
        chef.SetVariable("EntityAnimator", chef.GetCompo<EntityAnimator>());
        
        return Status.Running;
    }
}

