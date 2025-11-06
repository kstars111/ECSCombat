using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace Battle.Combat
{
    /// <summary>
    /// Destroys any entity whose Parent transform has died
    /// </summary>
    [UpdateInGroup(typeof(AttackResultSystemsGroup)), UpdateAfter(typeof(DealAttackDamageSystem))]
    public class KillChildrenOnParentDeathSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var entityBufferSystem = World.GetOrCreateSystemManaged<PostAttackEntityBuffer>();
            var buffer = entityBufferSystem.CreateCommandBuffer().AsParallelWriter();

            Entities
                .ForEach(
                (Entity e, int entityInQueryIndex, in Parent parent) =>
                {
                    if (!SystemAPI.HasComponent<Health>(parent.Value))
                        return;

                    if (SystemAPI.GetComponent<Health>(parent.Value).Value < 0f)
                        buffer.DestroyEntity(entityInQueryIndex, e);
                }
                )
                .ScheduleParallel();

            entityBufferSystem.AddJobHandleForProducer(Dependency);
        }
    }
}