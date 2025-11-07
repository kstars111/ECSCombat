using Unity.Entities;
using UnityEngine;

namespace Battle.Combat
{
    public class LifetimeProxy : MonoBehaviour
    {
        public float Lifetime;
    }

    public class LifetimeBaker : Baker<LifetimeProxy>
    {
        public override void Bake(LifetimeProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new Lifetime { Value = authoring.Lifetime });
        }
    }
}