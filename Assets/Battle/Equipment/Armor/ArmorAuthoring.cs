using Battle.Equipment;
using Unity.Entities;
using UnityEngine;

[DisallowMultipleComponent]
public class ArmorAuthoring : MonoBehaviour
{
    public float HealthPercentBonus;
}

public class ArmorBaker : Baker<ArmorAuthoring>
{
    public override void Bake(ArmorAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new Armor { HealthFractionBonus = authoring.HealthPercentBonus / 100f });
    }
}
