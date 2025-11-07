using Unity.Entities;
using UnityEngine;

namespace Battle.Combat
{
    public class CooldownProxy : MonoBehaviour
    {
        public float Duration = 1.0f;
    }

    public class CooldownBaker : Baker<CooldownProxy>
    {
        public override void Bake(CooldownProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new Cooldown { Duration = authoring.Duration, Timer = 0.0f });
        }
    }
}