using Battle.Equipment;
using Unity.Entities;
using UnityEngine;

[DisallowMultipleComponent]
public class ShieldEquipmentAuthoring : MonoBehaviour
{
    public float HealthPercentBonus;
}

public class ShieldEquipmentBaker : Baker<ShieldEquipmentAuthoring>
{
    public override void Bake(ShieldEquipmentAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new ShieldEquipment { HealthFractionBonus = authoring.HealthPercentBonus / 100f });
    }
}
