using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace Battle.Combat
{
    /// <summary>
    /// Sets any Target components that point to an invalid, non-existant entity to Entity.Null
    /// </summary>
    [
        UpdateAfter(typeof(DeleteEntitiesSystem)),
        UpdateInGroup(typeof(LateSimulationSystemGroup))
    ]
    public class RemoveDeadTargetReferencesSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities
                .ForEach(
                (ref Target target) =>
                {
                    if (!SystemAPI.HasComponent<Targetable>(target.Value))
                        target.Value = Entity.Null;
                    if (SystemAPI.HasComponent<Delete>(target.Value))
                        target.Value = Entity.Null;
                }
                )
                .ScheduleParallel();
        }
    }
}