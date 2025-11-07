using Unity.Entities;
using UnityEngine;

namespace Battle.Combat
{
    public class SizeRadiusProxy : MonoBehaviour
    {
        public float Size;
    }

    public class SizeRadiusBaker : Baker<SizeRadiusProxy>
    {
        public override void Bake(SizeRadiusProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new SizeRadius { Value = authoring.Size });
        }
    }
}