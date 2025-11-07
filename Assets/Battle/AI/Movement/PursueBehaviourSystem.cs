using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Battle.Combat;
using Unity.Transforms;
using Unity.Mathematics;

namespace Battle.AI
{
    /// <summary>
    /// Fighter behaviour when in pursuit of a target.
    /// </summary>
    [UpdateInGroup(typeof(AISystemGroup))]
    public class PursueBehaviourSystem : SystemBase
    {
        public const float PROXIMITY_RADIUS = 4f;

        protected override void OnUpdate()
        {
            var aiStateBuffer = World.GetOrCreateSystemManaged<AIStateChangeBufferSystem>();
            var buffer = aiStateBuffer.CreateCommandBuffer().AsParallelWriter();

            Entities
                .ForEach(
                (
                Entity e,
                int entityInQueryIndex,
                ref TurnToDestinationBehaviour destination,
                in PursueBehaviour pursue,
                in Target target,
                in Translation pos
                ) =>
                {
                    if (target.Value == Entity.Null || !SystemAPI.HasComponent<Translation>(target.Value))
                    {
                        // Go to idle state
                        buffer.RemoveComponent<PursueBehaviour>(entityInQueryIndex, e);
                        buffer.AddComponent(entityInQueryIndex, e, new IdleBehaviour());
                        return;
                    }

                    // Set entity destination to target position
                    destination.Destination = SystemAPI.GetComponent<Translation>(target.Value).Value;

                    // if too close to target, evasive manoeuvre
                    if (math.lengthsq(destination.Destination - pos.Value) < PROXIMITY_RADIUS * PROXIMITY_RADIUS)
                    {
                        buffer.RemoveComponent<PursueBehaviour>(entityInQueryIndex, e);
                        buffer.RemoveComponent<TurnToDestinationBehaviour>(entityInQueryIndex, e);
                        buffer.AddComponent(entityInQueryIndex, e, new PeelManoeuvre());
                    }
                }
                )
                .ScheduleParallel();

            aiStateBuffer.AddJobHandleForProducer(Dependency);
        }
    }
}