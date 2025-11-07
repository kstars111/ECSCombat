using Unity.Entities;
using UnityEngine;

namespace Battle.Movement
{
    public class HeadingProxy : MonoBehaviour
    {
    }

    public class HeadingBaker : Baker<HeadingProxy>
    {
        public override void Bake(HeadingProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new Heading { Value = 0.0f });
        }
    }
}