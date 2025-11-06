using System.Collections.Generic;
using Unity.Entities;
using UnityEditor;
using UnityEngine;

namespace Battle.AI
{
    public class TargetingOrdersProxy : MonoBehaviour
    {
        [Tooltip("Types of entity we are encouraged to target.")]
        public AgentCategory.eType PreferredTargets;

        [Tooltip("Types of entity we are discouraged from targeting.")]
        public AgentCategory.eType DiscouragedTargets;

        public bool TargetSameTeam = false;

#if UNITY_EDITOR
        void OnGUI()
        {
            PreferredTargets = (AgentCategory.eType)EditorGUILayout.EnumFlagsField("Preferred Targets", PreferredTargets);
            DiscouragedTargets = (AgentCategory.eType)EditorGUILayout.EnumFlagsField("Discouraged Targets", DiscouragedTargets);
        }
#endif
    }

    public class TargetingOrdersBaker : Baker<TargetingOrdersProxy>
    {
        public override void Bake(TargetingOrdersProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            var targetOrders = new TargetingOrders { Discouraged = authoring.DiscouragedTargets, Preferred = authoring.PreferredTargets, TargetSameTeam = authoring.TargetSameTeam };
            AddComponent(entity, targetOrders);
        }
    }
}