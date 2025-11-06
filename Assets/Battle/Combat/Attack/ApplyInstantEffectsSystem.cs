using Battle.Combat.AttackSources;
using Battle.Combat.Calculations;
using Battle.Equipment;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Battle.Combat.AttackSources
{
    /// <summary>
    /// Applies all instant effects.
    /// </summary>
    [
        UpdateInGroup(typeof(WeaponSystemsGroup)),
        UpdateAfter(typeof(FireTargettedToolsSystem))
        ]
    public class ApplyInstantEffectsSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var entityBufferSystem = World.GetOrCreateSystemManaged<WeaponEntityBufferSystem>();
            var buffer = entityBufferSystem.CreateCommandBuffer().AsParallelWriter();

            Entities.ForEach(
                (
                    Entity attacker,
                    int entityInQueryIndex,
                    in Target target,
                    in LocalToWorld localToWorld,
                    in TargettedTool tool,
                    in InstantEffect effect
                ) =>
                {
                    if (!tool.Firing)
                        return;

                    if (target.Value == Entity.Null)
                        return;

                    // Create the effect
                    Entity attack = buffer.Instantiate(entityInQueryIndex, effect.AttackTemplate);
                    buffer.AddComponent(entityInQueryIndex, attack, Attack.New(effect.Accuracy));
                    buffer.AddComponent(entityInQueryIndex, attack, target);
                    buffer.AddComponent(entityInQueryIndex, attack, new Instigator() { Value = attacker });
                    buffer.AddComponent(entityInQueryIndex, attack, new EffectSourceLocation { Value = localToWorld.Position });
                    buffer.AddComponent(entityInQueryIndex, attack, new Effectiveness { Value = 1f });
                    buffer.AddComponent(entityInQueryIndex, attack, new SourceLocation { Position = localToWorld.Position });
                    if (SystemAPI.HasComponent<LocalToWorld>(target.Value))
                        buffer.AddComponent(entityInQueryIndex, attack, new HitLocation { Position = SystemAPI.GetComponent<LocalToWorld>(target.Value).Position });
                }
                )
                .ScheduleParallel();

            entityBufferSystem.AddJobHandleForProducer(Dependency);
        }
    }
}