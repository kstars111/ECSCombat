using Unity.Entities;
using UnityEngine;

namespace Battle.Combat
{
    public class CombatSizeProxy : MonoBehaviour
    {
        [Tooltip("Characteristic length of the entity for purposes of hit chance in combat.")]
        public float Value = 0f;
    }

    public class CombatSizeBaker : Baker<CombatSizeProxy>
    {
        public override void Bake(CombatSizeProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new CombatSize { Value = authoring.Value });
        }
    }
}