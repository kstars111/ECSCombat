using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Battle.Effects
{
    public class LaserBeamProxy : MonoBehaviour
    {
        public float Width = 0.1f;
        public Color PrimaryColor;
        public Color SecondaryColor;
    }

    public class LaserBeamBaker : Baker<LaserBeamProxy>
    {
        public override void Bake(LaserBeamProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new BeamEffectStyle { Width = authoring.Width, PrimaryColor = new float4(authoring.PrimaryColor.r, authoring.PrimaryColor.g, authoring.PrimaryColor.b, authoring.PrimaryColor.a) });
        }
    }
}