using Unity.Entities;
using UnityEngine;

namespace Battle.Combat
{
    public class TargetableProxy : MonoBehaviour
    {
    }

    public class TargetableBaker : Baker<TargetableProxy>
    {
        public override void Bake(TargetableProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new Targetable());
        }
    }
}