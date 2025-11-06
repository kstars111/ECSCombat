using Battle.Movement;
using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;

namespace Battle.Equipment
{
    [UpdateInGroup(typeof(EquipmentUpdateGroup))]
    public class EngineSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities
                .WithAll<Enabling>()
                .ForEach( ( in Engine engine , in Parent parent ) =>
                {
                    ref var thrust = ref SystemAPI.GetComponentRW<Thrust>(parent.Value).ValueRW;
                    thrust.Forward += engine.ForwardThrust;
                    thrust.Turning += engine.TurningThrust;
                } )
                .WithBurst()
                .Schedule();

            Entities
                .WithAll<Disabling>()
                .ForEach( ( in Engine engine , in Parent parent ) =>
                {
                    ref var thrust = ref SystemAPI.GetComponentRW<Thrust>(parent.Value).ValueRW;
                    thrust.Forward -= engine.ForwardThrust;
                    thrust.Turning -= engine.TurningThrust;
                } )
                .WithBurst()
                .Schedule();
        }
    }
}