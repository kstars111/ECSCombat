using Unity.Entities;
using UnityEngine;

namespace Battle.Combat
{
    public class DamageProxy : MonoBehaviour
    {
        public float Damage;
    }

    public class DamageBaker : Baker<DamageProxy>
    {
        public override void Bake(DamageProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new Damage { Value = authoring.Damage });
        }
    }
}