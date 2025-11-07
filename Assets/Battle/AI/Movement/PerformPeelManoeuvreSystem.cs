using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Battle.Combat;
using Unity.Transforms;
using Unity.Mathematics;
using Battle.Movement;

namespace Battle.AI
{
    /// <summary>
    /// Fighter behaviour when in pursuit of a target.
    /// </summary>
    [UpdateInGroup(typeof(AISystemGroup))]
    public class PerformPeelManoeuvreSystem : SystemBase
    {
        public const float ENGAGEMENT_RADIUS = 10f;

        protected override void OnUpdate()
        {
            var aiStateBuffer = World.GetOrCreateSystemManaged<AIStateChangeBufferSystem>();
            var buffer = aiStateBuffer.CreateCommandBuffer().AsParallelWriter();

            Entities
                .WithAll<PeelManoeuvre>()
                .ForEach(
                (
                Entity e,
                int entityInQueryIndex,
                ref TurnSpeed turnSpeed,
                in Target target,
                in Translation pos,
                in Heading heading,
                in MaxTurnSpeed maxTurnSpeed
                ) =>
                {
                    if (target.Value == Entity.Null || !SystemAPI.HasComponent<Translation>(target.Value))
                    {
                        buffer.RemoveComponent<PeelManoeuvre>(entityInQueryIndex, e);
                        buffer.AddComponent(entityInQueryIndex, e, new IdleBehaviour());
                        buffer.AddComponent(entityInQueryIndex, e, new TurnToDestinationBehaviour());
                        return;
                    }

                    //Target position
                    var targetPos = SystemAPI.GetComponent<Translation>(target.Value);

                    // Turn away from the enemy.
                    float angleDiff = MathUtil.GetAngleDifference(MathUtil.GetHeadingToPoint(targetPos.Value - pos.Value), heading.Value);
                    if (math.abs(angleDiff) < 0.3 * math.PI)
                        turnSpeed.RadiansPerSecond = -maxTurnSpeed.RadiansPerSecond * math.sign(angleDiff);
                    else
                        turnSpeed.RadiansPerSecond = 0f;

                    // Remain in evasive manoeuvre until a certain distance to target is reached.
                    if (math.lengthsq(targetPos.Value - pos.Value) > ENGAGEMENT_RADIUS * ENGAGEMENT_RADIUS)
                    {
                        buffer.RemoveComponent<PeelManoeuvre>(entityInQueryIndex, e);
                        buffer.AddComponent(entityInQueryIndex, e, new PursueBehaviour());
                        buffer.AddComponent(entityInQueryIndex, e, new TurnToDestinationBehaviour());
                    }
                }
                )
                .ScheduleParallel();
            aiStateBuffer.AddJobHandleForProducer(Dependency);
        }
    }
}