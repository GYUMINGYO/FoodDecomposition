using GM.Staffs;
using System;
using GM;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using GM.Managers;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MakeRecipeFood", story: "[chef] make recipe food", category: "Action", id: "19fd70b04d08a57a7e3e6ebe4704bf3c")]
public partial class MakeRecipeFoodAction : Action
{
    [SerializeReference] public BlackboardVariable<Chef> Chef;

    protected override Status OnStart()
    {
        Food food = ManagerHub.Instance.Pool.Pop(Chef.Value.CurrentData.recipe.poolType) as Food;
        food.transform.position = Chef.Value.FoodHandTrm.position;
        food.transform.parent = Chef.Value.FoodHandTrm;

        ManagerHub.Instance.GetManager<DataManager>().AddMaterialCost(Chef.Value.CurrentData.recipe.materialCost);

        return Status.Running;
    }
}

