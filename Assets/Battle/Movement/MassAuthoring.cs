using Unity.Entities;
using UnityEngine;

namespace Battle.Movement
{
    public class MassAuthoring : MonoBehaviour
    {
        [Tooltip("Mass of a ship.")]
        public float Mass = 1.0f;
    }

    public class MassBaker : Baker<MassAuthoring>
    {
        public override void Bake(MassAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new Mass { Value = authoring.Mass, Base = authoring.Mass });
        }
    }
}