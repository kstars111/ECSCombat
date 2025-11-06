using Unity.Entities;
using UnityEngine;

namespace Battle.Effects
{
    public class ParticleBeamEffectProxy : MonoBehaviour
    {
        public GameObject ParticleSystem;
    }

    public class ParticleBeamEffectBaker : Baker<ParticleBeamEffectProxy>
    {
        public override void Bake(ParticleBeamEffectProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddSharedComponent(entity, new ParticleBeamEffect { ParticleSystem = authoring.ParticleSystem });
        }
    }
}