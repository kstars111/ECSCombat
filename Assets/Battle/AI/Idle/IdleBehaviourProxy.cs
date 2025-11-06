using Unity.Entities;
using UnityEngine;

namespace Battle.AI
{
    public class IdleBehaviourProxy : MonoBehaviour
    {
    }

    public class IdleBehaviourBaker : Baker<IdleBehaviourProxy>
    {
        public override void Bake(IdleBehaviourProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new IdleBehaviour { });
        }
    }
}