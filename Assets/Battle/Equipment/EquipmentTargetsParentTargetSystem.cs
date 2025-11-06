using Battle.Combat;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace Battle.Equipment
{
    /// <summary>
    /// Sets the target of all equipment to the target of the parent entity. Eg for weapons, etc.
    /// </summary>
    [UpdateInGroup(typeof(EquipmentUpdateGroup))]
    public class EquipmentTargetsParentTargetSystem : SystemBase
    {
        protected override void OnUpdate ()
        {
            Entities
                .WithAll<Equipment,Target>()
                .ForEach( ( Entity entity , in Parent parent ) =>
                {
                    if (!SystemAPI.HasComponent<Target>(entity) || !SystemAPI.HasComponent<Target>(parent.Value))
                        return;

                    SystemAPI.SetComponent(entity, SystemAPI.GetComponent<Target>(parent.Value));
                } )
                .WithBurst()
                .Schedule();
        }
    }
}
