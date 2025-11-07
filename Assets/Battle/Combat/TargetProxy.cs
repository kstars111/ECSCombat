using Unity.Entities;
using UnityEngine;

namespace Battle.Combat
{
    public class TargetProxy : MonoBehaviour
    {
    }

    public class TargetBaker : Baker<TargetProxy>
    {
        public override void Bake(TargetProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new Target { Value = Entity.Null });
        }
    }
}