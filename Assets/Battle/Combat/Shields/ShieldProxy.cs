using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Battle.Combat
{
    public class ShieldProxy : MonoBehaviour
    {
        [Tooltip("Hit points of this shield.")]
        public float Capacity;

        [Tooltip("Radius of the shield.")]
        public float Radius;
    }

    public class ShieldBaker : Baker<ShieldProxy>
    {
        public override void Bake(ShieldProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new Shield { Health = authoring.Capacity, Radius = authoring.Radius });
            AddComponent(entity, new MaxShield { Value = authoring.Capacity });
        }
    }
}
