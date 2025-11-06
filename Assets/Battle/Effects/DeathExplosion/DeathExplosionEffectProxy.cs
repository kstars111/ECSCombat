using Unity.Entities;
using UnityEngine;

namespace Battle.Effects
{
    public class DeathExplosionEffectProxy : MonoBehaviour
    {
        [Tooltip("Particle system generated when the entity dies.")]
        public GameObject ParticleSystem;
    }

    public class DeathExplosionEffectBaker : Baker<DeathExplosionEffectProxy>
    {
        public override void Bake(DeathExplosionEffectProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddSharedComponent(entity, new DeathExplosionEffect { ParticleSystem = authoring.ParticleSystem });
        }
    }
}