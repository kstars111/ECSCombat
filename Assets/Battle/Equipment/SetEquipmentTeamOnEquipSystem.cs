using Battle.Combat;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace Battle.Equipment
{
    /// <summary>
    /// Adds parent team to the equipment as it is being equiped.
    /// 
    /// Very similar to EquipmentTargetsParentTarget
    /// </summary>
    [UpdateInGroup(typeof(EquipmentUpdateGroup))]
    public class SetEquipmentTeamOnEquipSystem : SystemBase
    {
        protected override void OnUpdate ()
        {
            Entities
                .WithAll<Team,Equipment,Equipping>()
                .ForEach( ( Entity entity , in Parent parent ) =>
                {
                    if( !SystemAPI.HasComponent<Team>(parent.Value) )
                        return;

                    SystemAPI.SetComponent(entity, SystemAPI.GetComponent<Team>(parent.Value));
                } )
                .WithBurst()
                .ScheduleParallel();
        }
    }
}
