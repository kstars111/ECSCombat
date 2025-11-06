using Battle.AI;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Battle.Combat
{
    /// <summary>
    /// Spawns ships from the aircraft hangar.
    /// </summary>
    [
        UpdateInGroup(typeof(WeaponSystemsGroup))
        ]
    public class AircraftHangarSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var entityCommandBufferSystem = World.GetOrCreateSystemManaged<WeaponEntityBufferSystem>();
            var buffer = entityCommandBufferSystem.CreateCommandBuffer().AsParallelWriter();

            Entities
                .ForEach(
                (
                Entity entity,
                int entityInQueryIndex,
                ref LocalToWorld localToWorld,
                ref AircraftHangar hangar,
                ref Team team,
                ref Cooldown cooldown
                ) =>
                {
                    if (!cooldown.IsReady())
                        return;

                    cooldown.Timer = cooldown.Duration;

                    var ship = buffer.Instantiate(entityInQueryIndex, hangar.Archetype);
                    buffer.SetComponent(entityInQueryIndex, ship, new Translation { Value = localToWorld.Position - new float3(0f, -0.1f, 0f) });
                    buffer.SetComponent(entityInQueryIndex, ship, new Rotation { Value = new quaternion(localToWorld.Value) });
                    buffer.SetComponent(entityInQueryIndex, ship, team);
                    buffer.AddComponent(entityInQueryIndex, ship, new Escort { Target = entity });
                })
                .ScheduleParallel();

            entityCommandBufferSystem.AddJobHandleForProducer(Dependency);
        }
    }
}