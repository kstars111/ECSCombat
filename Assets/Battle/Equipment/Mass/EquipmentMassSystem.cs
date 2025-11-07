using Battle.Movement;
using Unity.Entities;
using Unity.Transforms;

namespace Battle.Equipment
{
    /// <summary>
    /// Modifies Mass as entities are added.
    /// </summary>
    [UpdateInGroup(typeof(EquipmentUpdateGroup))]
    public class EquipmentMassSystem : SystemBase
    {
        protected override void OnUpdate ()
        {
            Entities
                .WithAll<Enabling>()
                .ForEach( ( in EquipmentMass equipmentMass , in Parent parent ) =>
                {
                    Parent ship = parent;
                    while( SystemAPI.HasComponent<Parent>(ship.Value) )
                        ship = SystemAPI.GetComponent<Parent>(ship.Value);

                    ref var mass = ref SystemAPI.GetComponentRW<Mass>(ship.Value).ValueRW;
                    mass.Value += equipmentMass.MassFractionalIncrease * mass.Base;
                } )
                .WithBurst()
                .ScheduleParallel();
        }
    }
}