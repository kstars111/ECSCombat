using Unity.Entities;
using UnityEngine;

namespace Battle.Combat.Calculations
{
    public class EffectiveRangeProxy : MonoBehaviour
    {
        [Tooltip("Start of the range over which effectiveness changes.")]
        public float EffectiveRangeStart = 1f;

        [Tooltip("End of the range over which effectiveness changes.")]
        public float EffectiveRangeEnd = 3f;

        [Tooltip("Effectiveness when far outside the effective range.")]
        public float MinimumEffectiveness = 0.2f;

        [Tooltip("Does effectiveness increase or decrease over the effective range?")]
        public bool IsIncreasing;
    }

    public class EffectiveRangeBaker : Baker<EffectiveRangeProxy>
    {
        public override void Bake(EffectiveRangeProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity,
                new LinearEffectiveRange {
                    EffectiveRangeStart = authoring.EffectiveRangeStart,
                    EffectiveRangeEnd = authoring.EffectiveRangeEnd,
                    MinimumEffectiveness = authoring.MinimumEffectiveness,
                    IsIncreasing = authoring.IsIncreasing
                });
        }
    }
}