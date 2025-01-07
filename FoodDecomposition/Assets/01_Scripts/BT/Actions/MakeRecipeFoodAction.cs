using GM.Staffs;
using System;
using GM;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MakeRecipeFood", story: "[chef] make recipe food", category: "Action", id: "19fd70b04d08a57a7e3e6ebe4704bf3c")]
public partial class MakeRecipeFoodAction : Action
{
    [SerializeReference] public BlackboardVariable<Chef> Chef;

    protected override Status OnStart()
    {
        Food food = SingletonePoolManager.Instance.Pop(Chef.Value.CurrentData.recipe.poolType) as Food;
        food.transform.position = Chef.Value.FoodTrm.position;
        food.transform.parent = Chef.Value.FoodTrm;
        return Status.Running;
    }
}

