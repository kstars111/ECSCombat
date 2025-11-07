using Battle.Combat;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Battle.Equipment
{
    [UpdateInGroup(typeof(EquipmentUpdateGroup))]
    public class ShieldEquipmentSystem : SystemBase
    {
        protected override void OnUpdate ()
        {
            Entities
                .WithAll<Enabling>()
                .ForEach( ( in ShieldEquipment shieldEquipment , in Parent parent ) =>
                {
                    MaxHealth maxHealth = SystemAPI.GetComponent<MaxHealth>(parent.Value);
                    float shieldHP = shieldEquipment.HealthFractionBonus * maxHealth.Base;

                    ref var shield = ref SystemAPI.GetComponentRW<Shield>(parent.Value).ValueRW;
                    shield.Health += shieldHP;

                    ref var maxShield = ref SystemAPI.GetComponentRW<MaxShield>(parent.Value).ValueRW;
                    maxShield.Value += shieldHP;
                } )
                .WithBurst()
                .Schedule();

            Entities
                .WithAll<Disabling>()
                .ForEach( ( in ShieldEquipment shieldEquipment , in Parent parent ) =>
                {
                    MaxHealth maxHealth = SystemAPI.GetComponent<MaxHealth>(parent.Value);
                    float shieldHP = shieldEquipment.HealthFractionBonus * maxHealth.Base;

                    ref var shield = ref SystemAPI.GetComponentRW<Shield>(parent.Value).ValueRW;
                    shield.Health = math.max(0f, shield.Health - shieldHP);

                    ref var maxShield = ref SystemAPI.GetComponentRW<MaxShield>(parent.Value).ValueRW;
                    maxShield.Value = math.max(0f, maxShield.Value - shieldHP);
                } )
                .WithBurst()
                .Schedule();
        }
    }
}