using Battle.Combat;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace Battle.Equipment
{
    /// <summary>
    /// Essentially a workaround until I implement a more permanent solution using initialisation tag components.
    /// </summary>
    [UpdateBefore(typeof(AI.AISystemGroup))]
    public class SetTeamToParentTeam : SystemBase
    {
        protected override void OnUpdate ()
        {
            Entities
                .WithAll<Team>()
                .WithChangeFilter<Parent>()
                .ForEach( ( Entity entity , in Parent parent ) =>
                {
                    if( SystemAPI.HasComponent<Team>(parent.Value) )
                        SystemAPI.SetComponent(entity, SystemAPI.GetComponent<Team>(parent.Value));
                } )
                .WithBurst()
                .Schedule();
        }
    }
}
