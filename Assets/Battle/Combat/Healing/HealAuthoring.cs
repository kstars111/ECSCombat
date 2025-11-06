using Unity.Entities;
using UnityEngine;

namespace Battle.Combat
{
    public class HealAuthoring : MonoBehaviour
    {
        public float Heal;
    }

    public class HealBaker : Baker<HealAuthoring>
    {
        public override void Bake(HealAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new Heal { Value = authoring.Heal });
        }
    }
}