using Unity.Entities;

namespace Battle.Combat
{
    /// <summary>
    /// Deletes all entities with Attack component.
    /// </summary>
    [
        UpdateAfter(typeof(AttackResultSystemsGroup)),
        UpdateBefore(typeof(PostAttackEntityBuffer))
        ]
    public class DestroyAttacksSystem : SystemBase
    {
        private EntityQuery AttackQuery;

        protected override void OnCreate()
        {
            AttackQuery = EntityManager.CreateEntityQuery(ComponentType.ReadOnly<Attack>());
        }

        protected override void OnUpdate()
        {
            var endSimBufferSystem = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
            var buffer = endSimBufferSystem.CreateCommandBuffer();
            buffer.DestroyEntity(AttackQuery);
        }
    }
}