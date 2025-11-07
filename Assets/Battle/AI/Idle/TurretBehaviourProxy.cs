using Unity.Entities;
using UnityEngine;

namespace Battle.AI
{
    public class TurretBehaviourProxy : MonoBehaviour
    {
        public float Range;
    }

    public class TurretBehaviourBaker : Baker<TurretBehaviourProxy>
    {
        public override void Bake(TurretBehaviourProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new TurretBehaviour { Range = authoring.Range });
        }
    }
}