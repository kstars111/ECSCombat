using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Entities.Graphics;

namespace Battle.Effects
{
    [Serializable]
    [MaterialProperty("_HitColor", MaterialPropertyFormat.Float4)]
    public struct LastHitColor : IComponentData
    {
        public float4 Value;
    }
}
