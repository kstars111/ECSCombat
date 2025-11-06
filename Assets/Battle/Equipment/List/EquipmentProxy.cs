using Unity.Entities;
using UnityEngine;

namespace Battle.Equipment
{
    public class EquipmentProxy : MonoBehaviour
    {
    }

    public class EquipmentBaker : Baker<EquipmentProxy>
    {
        public override void Bake(EquipmentProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new Equipment());
        }
    }
}