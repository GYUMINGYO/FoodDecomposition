using GM.Staffs;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CheckStaffIsChange", story: "[staff] is Change", category: "Conditions", id: "a3f8fc2235bf846c05354337a90a7025")]
public partial class CheckStaffIsChangeCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Staff> Staff;

    public override bool IsTrue()
    {
        return Staff.Value.IsChange ? true : false;
    }
}
