using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using Battle.Movement;

namespace Battle.Combat
{
    /// <summary>
    /// Checks if projectiles have reached their target.
    /// </summary>
    [UpdateInGroup(typeof(WeaponSystemsGroup))]
    public class ProjectileHitTargetSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float dT = UnityEngine.Time.fixedDeltaTime;
            var commandBufferSystem = World.GetOrCreateSystemManaged<WeaponEntityBufferSystem>();
            var commands = commandBufferSystem.CreateCommandBuffer().AsParallelWriter();

            Entities
                .ForEach( (
                    Entity e, int entityInQueryIndex,
                    ref Projectile projectile,
                    in Target target,
                    in LocalToWorld transform,
                    in Speed speed,
                    in TurnSpeed turnSpeed
                ) =>
                {
                    if (!SystemAPI.HasComponent<LocalToWorld>(target.Value))
                        return; // Target does not exist?

                    var tPos = SystemAPI.GetComponent<LocalToWorld>(target.Value).Position;
                    var delta = (tPos - transform.Position);

                    var projectileDistance = speed.Value * dT;

                    float radius = (SystemAPI.HasComponent<SizeRadius>(target.Value)) ? SystemAPI.GetComponent<SizeRadius>(target.Value).Value : 0f;

                    // If target out of range, return
                    if (math.length(delta) - radius > projectileDistance)
                        return;

                    // Projectile has reached the target.
                    projectile.ReachedTarget = true;

                    commands.AddComponent(entityInQueryIndex, e, new Delete());
                    var effect = commands.Instantiate(entityInQueryIndex, projectile.AttackEntity);
                    commands.AddComponent(entityInQueryIndex, effect, target);
                    commands.AddComponent(entityInQueryIndex, effect, new Instigator { Value = e });
                })
                .WithBurst()
                .ScheduleParallel();

            commandBufferSystem.AddJobHandleForProducer(Dependency);
        }
    }
}