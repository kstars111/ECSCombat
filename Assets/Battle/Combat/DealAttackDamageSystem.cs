using Battle.Effects;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Jobs;

namespace Battle.Combat
{
    /// <summary>
    /// Deals damage for all Attack entities with a Damage component.
    /// </summary>
    [UpdateInGroup(typeof(AttackResultSystemsGroup))]
    public class DealAttackDamageSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities
                .ForEach( ( in Attack attack , in Target target , in Damage damage ) =>
                {
                    if( attack.Result==Attack.eResult.Miss )
                        return;

                    if( SystemAPI.HasComponent<LastHitTimer>(target.Value) && damage.Value>0f )
                        SystemAPI.SetComponent(target.Value, new LastHitTimer{ Value = 0f });
                    if( SystemAPI.HasComponent<LastHitColor>(target.Value) && damage.Value>0f )
                        SystemAPI.SetComponent(target.Value, new LastHitColor{ Value = new float4(1f,1f,1f,1f) });

                    if( SystemAPI.HasComponent<Health>(target.Value) )
                    {
                        ref var health = ref SystemAPI.GetComponentRW<Health>(target.Value).ValueRW;
                        health.Value -= damage.Value;
                    }
                } )
                .WithBurst()
                .Schedule();
        }
    }
}