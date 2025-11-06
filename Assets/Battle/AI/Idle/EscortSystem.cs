using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace Battle.AI
{
    /// <summary>
    /// Causes entities to walk randomly about.
    /// </summary>
    [UpdateBefore(typeof(RandomWalkSystem))]
    [UpdateInGroup(typeof(AISystemGroup))]
    public class EscortSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities
                .ForEach( ( ref RandomWalkBehaviour walk , in Escort escort ) =>
                {
                    if( SystemAPI.HasComponent<LocalToWorld>(escort.Target) )
                        walk.Centre = SystemAPI.GetComponent<LocalToWorld>(escort.Target).Position;
                } )
                .WithBurst()
                .ScheduleParallel();
        }
    }
}