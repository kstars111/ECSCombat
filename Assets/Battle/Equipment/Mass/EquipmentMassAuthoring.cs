using Battle.Equipment;
using Unity.Entities;
using UnityEngine;

[DisallowMultipleComponent]
public class EquipmentMassAuthoring : MonoBehaviour
{
    [Tooltip("Percentage increase the entitie's mass when the equipment is added.")]
    public float MassPercentageIncrease;
}

public class EquipmentMassBaker : Baker<EquipmentMassAuthoring>
{
    public override void Bake(EquipmentMassAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new EquipmentMass { MassFractionalIncrease = authoring.MassPercentageIncrease / 100f });
    }
}
