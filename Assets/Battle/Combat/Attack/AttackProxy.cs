using Unity.Entities;
using UnityEngine;

namespace Battle.Combat.AttackSources
{
    public class AttackProxy : MonoBehaviour
    {
        public float Accuracy;
    }

    public class AttackBaker : Baker<AttackProxy>
    {
        public override void Bake(AttackProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, Attack.New(authoring.Accuracy));
        }
    }
}