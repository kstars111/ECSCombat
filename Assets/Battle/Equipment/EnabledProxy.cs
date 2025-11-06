using Unity.Entities;
using UnityEngine;

namespace Battle.Equipment
{
    public class EnabledProxy : MonoBehaviour
    {
    }

    public class EnabledBaker : Baker<EnabledProxy>
    {
        public override void Bake(EnabledProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new Enabling());
        }
    }
}