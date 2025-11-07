using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Battle.AI
{
    public class TurnToDestinationProxy : MonoBehaviour
    {
        public float3 Destination;
    }

    public class TurnToDestinationBaker : Baker<TurnToDestinationProxy>
    {
        public override void Bake(TurnToDestinationProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new TurnToDestinationBehaviour { Destination = authoring.Destination });
        }
    }
}