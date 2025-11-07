using Unity.Entities;
using UnityEditor;
using UnityEngine;

namespace Battle.AI
{
    public class AgentCategoryProxy : MonoBehaviour
    {
        [Tooltip("Type of this entity.")]
        public AgentCategory.eType AgentType;

#if UNITY_EDITOR
        void OnGUI()
        {
            AgentType = (AgentCategory.eType)EditorGUILayout.EnumFlagsField("Type of this entity", AgentType);
        }
#endif
    }

    public class AgentCategoryBaker : Baker<AgentCategoryProxy>
    {
        public override void Bake(AgentCategoryProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            var category = new AgentCategory { Type = authoring.AgentType };
            AddComponent(entity, category);
        }
    }
}