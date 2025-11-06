using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Entities.Graphics;
using UnityEngine;

namespace Battle.Combat
{
    public class TeamProxy : MonoBehaviour
    {
        public byte TeamID = 0;
    }

    public class TeamBaker : Baker<TeamProxy>
    {
        public override void Bake(TeamProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            var data = new Team { ID = authoring.TeamID };
            AddComponent(entity, data);

            // Define team colors
            float4 color;
            switch (authoring.TeamID)
            {
                default: color = new float4(1.0f, 1.0f, 1.0f, 1.0f); break;
                case 1: color = new float4(0.5f, 0.7f, 1.0f, 1.0f); break;
                case 2: color = new float4(1.0f, 0.0f, 0.0f, 1.0f); break;
            }
            AddComponent(entity, new MaterialColor { Value = color });
        }
    }

    /// <summary>
    /// Team Color associated with the ship.
    /// </summary>
    [Serializable]
    [MaterialProperty("_TeamColor", MaterialPropertyFormat.Float4)]
    public struct TeamColor : IComponentData
    {
        public float4 Color;
    }
}