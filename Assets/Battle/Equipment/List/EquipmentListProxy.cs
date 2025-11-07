using Unity.Entities;
using UnityEngine;

namespace Battle.Equipment
{
    public class EquipmentListProxy : MonoBehaviour
    {
    }

    public class EquipmentListBaker : Baker<EquipmentListProxy>
    {
        public override void Bake(EquipmentListProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddBuffer<EquipmentList>(entity);
        }
    }
}