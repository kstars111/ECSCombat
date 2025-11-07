using Unity.Entities;
using UnityEngine;

namespace Battle.Movement
{
    public class SpeedProxy : MonoBehaviour
    {
        public float MaxSpeed = 1.0f;
    }

    public class SpeedBaker : Baker<SpeedProxy>
    {
        public override void Bake(SpeedProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new Speed { Value = authoring.MaxSpeed });
            AddComponent(entity, new MaxSpeed { Value = authoring.MaxSpeed });
        }
    }
}