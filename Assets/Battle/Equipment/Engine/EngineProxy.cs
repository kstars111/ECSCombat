using Unity.Entities;
using UnityEngine;

namespace Battle.Equipment
{
    [RequireComponent(typeof(EnabledProxy))]
    public class EngineProxy : MonoBehaviour
    {
        public float ForwardThrust;
        public float TurningThrust;
    }

    public class EngineBaker : Baker<EngineProxy>
    {
        public override void Bake(EngineProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new Engine { ForwardThrust = authoring.ForwardThrust, TurningThrust = authoring.TurningThrust });
        }
    }
}