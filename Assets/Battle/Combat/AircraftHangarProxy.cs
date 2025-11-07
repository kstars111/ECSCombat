using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Battle.Combat
{
    public class AircraftHangarProxy : MonoBehaviour
    {
        [Tooltip("Entity type spawned by this hangar.")]
        public GameObject Archetype;
    }

    public class AircraftHangarBaker : Baker<AircraftHangarProxy>
    {
        public override void Bake(AircraftHangarProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            // word of warning: this will create a prefab entity for every hangar, which might inefficient if you have thousands of hangars.
            // However, I'm not fussed.
            var prefab = GetEntity(authoring.Archetype, TransformUsageFlags.None);
            AddComponent(entity, new AircraftHangar { Archetype = prefab });
        }
    }
}
