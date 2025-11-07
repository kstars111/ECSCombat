using Battle.Combat;
using Unity.Entities;
using Unity.Transforms;

namespace Battle.Equipment
{
    /// <summary>
    /// Modifies max health as armor is added or removed.
    /// </summary>
    [UpdateInGroup(typeof(EquipmentUpdateGroup))]
    public class ArmorSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities
                .WithAll<Enabling>()
                .ForEach( ( in Armor armor , in Parent parent ) =>
                {
                    ref var health = ref SystemAPI.GetComponentRW<Health>(parent.Value).ValueRW;
                    ref var maxHealth = ref SystemAPI.GetComponentRW<MaxHealth>(parent.Value).ValueRW;
                    float fraction = health.Value / maxHealth.Value;

                    float bonusHealth = armor.HealthFractionBonus * maxHealth.Base;
                    maxHealth.Value += bonusHealth;

                    health.Value = fraction * maxHealth.Value;
                } )
                .WithBurst()
                .Schedule();

            // Currently no plans to remove armor, so removal not implemented.
        }
    }
}