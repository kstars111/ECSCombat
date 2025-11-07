using Battle.Effects;
using Unity.Entities;
using UnityEngine;

namespace Battle.Combat
{
    public class HealthProxy : MonoBehaviour
    {
        public float MaxHealth;
        public bool IsMortal = true;
    }

    public class HealthBaker : Baker<HealthProxy>
    {
        public override void Bake(HealthProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new Health { Value = authoring.MaxHealth });
            AddComponent(entity, new MaxHealth { Value = authoring.MaxHealth, Base = authoring.MaxHealth });
            if (authoring.IsMortal)
                AddComponent(entity, new Mortal());
            AddComponent(entity, new LastHitTimer { Value = 0f });
            AddComponent(entity, new LastHitColor { Value = new Unity.Mathematics.float4(1f,1f,1f,1f) });
        }
    }
}