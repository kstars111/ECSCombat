using Battle.Effects;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Battle.Combat
{
    [
        UpdateInGroup(typeof(AttackResultSystemsGroup)),
        UpdateBefore(typeof(DealAttackDamageSystem))
        ]
    public class ShieldsAbsorbDamageSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var commandBufferSystem = World.GetOrCreateSystemManaged<PostAttackEntityBuffer>();
            var commands = commandBufferSystem.CreateCommandBuffer().AsParallelWriter();

            Entities
                .WithName("enumerate_over_all_attacks_job")
                .WithNone<ShieldBypass>()
                .WithAll<Instigator>()
                .ForEach(
                (Entity entity,
                int entityInQueryIndex,
                ref Attack attack,
                ref Damage damage,
                ref SourceLocation sourceLocation,
                ref HitLocation hitLocation,
                ref Target target) =>
            {
                if( !SystemAPI.HasComponent<Shield>(target.Value) || !SystemAPI.HasComponent<LocalToWorld>(target.Value) )
                    return;
                ref var shield = ref SystemAPI.GetComponentRW<Shield>(target.Value).ValueRW;

                // depleted shields do not block attacks.
                if( shield.Health<=0f )
                    return;

                // if attack comes from within shield there is no shield protection.
                var targetPosition = SystemAPI.GetComponent<LocalToWorld>(target.Value);
                float3 delta = targetPosition.Position - sourceLocation.Position;
                if( math.lengthsq(delta)<math.pow(shield.Radius,2f) )
                    return;

                // Shield reduces incoming damage.
                float absorbed = math.min( shield.Health , damage.Value );
                shield.Health = shield.Health - absorbed;
                damage.Value = damage.Value - absorbed;
                hitLocation.Position = targetPosition.Position + shield.Radius * -math.normalize(delta);

                // generate aesthetic effect.
                bool blocked = absorbed > 0f;
                if( blocked )
                {
                    Entity effect = commands.CreateEntity( entityInQueryIndex );
                    commands.AddComponent( entityInQueryIndex, effect, new ShieldHitEffect{ HitDirection = -math.normalize(delta) } );
                    commands.AddComponent( entityInQueryIndex, effect, shield );
                    commands.AddComponent( entityInQueryIndex, effect, targetPosition );
                    commands.AddComponent( entityInQueryIndex, effect, new Delete() );
                }
            } )
            .WithBurst()
            .Schedule();

            commandBufferSystem.AddJobHandleForProducer( Dependency );
        }
    }
}
