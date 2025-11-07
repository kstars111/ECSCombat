using Unity.Entities;
using Unity.Jobs;

namespace Battle.Combat
{
    /// <summary>
    /// Reduces lifetime, marks expired entities for deletion.
    /// </summary>
    public class ReduceLifetimeSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var bufferSystem = World.GetOrCreateSystemManaged<LateSimulationCommandBufferSystem>();
            var buffer = bufferSystem.CreateCommandBuffer().AsParallelWriter();
            float dT = GetSingleton<GameTimeDelta>().dT;
            Entities
                .ForEach(
                (Entity e, int entityInQueryIndex, ref Lifetime lifetime) =>
                {
                    lifetime.Value -= dT;
                    if (lifetime.Value < 0f)
                        buffer.AddComponent<Delete>(entityInQueryIndex, e);
                })
                .ScheduleParallel();
            bufferSystem.AddJobHandleForProducer(Dependency);
        }
    }
}