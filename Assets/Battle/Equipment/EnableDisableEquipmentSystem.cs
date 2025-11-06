using Unity.Entities;
using Unity.Jobs;

namespace Battle.Equipment
{
    /// <summary>
    /// Enable/Disable equipment
    /// </summary>
    [
        UpdateAfter(typeof(EquipmentUpdateGroup)),
        UpdateBefore(typeof(EquipmentBufferSystem))
        ]
    public class EnableDisableEquipmentSystem : SystemBase
    {
        protected EntityQuery EnablingQuery;
        protected EntityQuery DisablingQuery;

        protected override void OnCreate()
        {
            EnablingQuery = GetEntityQuery(new EntityQueryDesc {
                All = new[] {
                    ComponentType.ReadOnly<Enabling>()
                }});
            DisablingQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new[] {
                    ComponentType.ReadOnly<Disabling>()
                }
            });
        }

        protected override void OnUpdate()
        {
            var equipmentBuffer = World.GetOrCreateSystemManaged<EquipmentBufferSystem>();
            var buffer = equipmentBuffer.CreateCommandBuffer();
            buffer.RemoveComponent<Enabled>(DisablingQuery);
            buffer.RemoveComponent<Disabling>(DisablingQuery);
            buffer.AddComponent<Enabled>(EnablingQuery);
            buffer.RemoveComponent<Enabling>(EnablingQuery);
            equipmentBuffer.AddJobHandleForProducer(Dependency);
        }
    }
}