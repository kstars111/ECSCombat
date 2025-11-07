using Unity.Collections;
using Unity.Entities;

namespace Battle.Combat
{
    /// <summary>
    /// Deletes all entities with Destroy component
    /// </summary>
    [
        UpdateAfter(typeof(LateSimulationCommandBufferSystem)),
        UpdateInGroup(typeof(LateSimulationSystemGroup))
        ]
    public class DeleteEntitiesSystem : SystemBase
    {
        private EntityQuery DeleteEntities;

        protected override void OnCreate()
        {
            DeleteEntities = EntityManager.CreateEntityQuery(ComponentType.ReadOnly<Delete>());
        }

        protected override void OnUpdate()
        {
            var entities = DeleteEntities.ToEntityArray(Allocator.TempJob);
            EntityManager.DestroyEntity(entities);
            entities.Dispose();
        }
    }
}