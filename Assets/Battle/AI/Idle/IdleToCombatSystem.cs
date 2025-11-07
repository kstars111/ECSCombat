using Unity.Entities;
using Unity.Jobs;

using Battle.Combat;

namespace Battle.AI
{
    /// <summary>
    /// Transitions entities from Idle state into Combat.
    /// </summary>
    [UpdateBefore(typeof(PursueBehaviourSystem))]
    [UpdateInGroup(typeof(AISystemGroup))]
    public class IdleToCombatSystem : SystemBase
    {
        protected override void OnUpdate ()
        {
            var commandBufferSystem = World.GetOrCreateSystemManaged<AIStateChangeBufferSystem>();
            var commands = commandBufferSystem.CreateCommandBuffer().AsParallelWriter();

            Entities
                .WithAll<IdleBehaviour>()
                .WithChangeFilter<Target>()
                .ForEach( ( Entity entity , int entityInQueryIndex , in Target target ) =>
                {
                    if( target.Value!=Entity.Null )
                    {
                        commands.RemoveComponent<IdleBehaviour>( entityInQueryIndex , entity );
                        commands.AddComponent<PursueBehaviour>( entityInQueryIndex , entity );
                    }
                } )
                .WithBurst()
                .ScheduleParallel();

            commandBufferSystem.AddJobHandleForProducer( Dependency );
        }

    }
}
