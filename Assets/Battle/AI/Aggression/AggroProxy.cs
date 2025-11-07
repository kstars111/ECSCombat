using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Battle.AI
{
    public class AggroProxy : MonoBehaviour
    {
        public float Radius = 10f;

        [Tooltip("Time in seconds between retargetting. 0 to disable.")]
        public float RetargetTime = 0.0f;
    }

    public class AggroBaker : Baker<AggroProxy>
    {
        public override void Bake(AggroProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new AggroRadius { Value = authoring.Radius });
            AddComponent(entity, new AggroLocation());
            if (authoring.RetargetTime > 0.0f)
                AddComponent(entity, new RetargetBehaviour { Interval = authoring.RetargetTime, RemainingTime = authoring.RetargetTime });
        }
    }

    /// <summary>
    /// Distance from which this entity will engage another entity.
    /// </summary>
    [Serializable]
    public struct AggroRadius : IComponentData
    {
        public float Value;
        public const float MAX_AGGRO_RADIUS = 80f;
    }

    /// <summary>
    /// The location from which targets should be sought
    /// </summary>
    [Serializable]
    public struct AggroLocation : IComponentData
    {
        public float3 Position;
    }
}