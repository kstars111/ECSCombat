using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Battle.Combat.AttackSources
{
    public class DirectWeaponProxy : MonoBehaviour
    {
        [Tooltip("Ammunition used by this direct weapon.")]
        public GameObject Ammo;

        [Tooltip("Range of the weapon")]
        public float Range;

        [Tooltip("Is the weapon armed to fire?")]
        public bool Armed;

        [Tooltip("Full attack cone for the weapon, in degrees")]
        public float AttackCone;

        [Tooltip("Base accuracy rating of the weapon")]
        public float Accuracy;
    }

    public class DirectWeaponBaker : Baker<DirectWeaponProxy>
    {
        public override void Bake(DirectWeaponProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            float coneInRad = authoring.AttackCone * Mathf.PI / 180f;
            var prefab = GetEntity(authoring.Ammo, TransformUsageFlags.None);
            AddComponent(entity, new InstantEffect { AttackTemplate = prefab, Accuracy = authoring.Accuracy });
            AddComponent(entity, new TargettedTool { Armed = authoring.Armed, Range = authoring.Range, Cone = coneInRad, Firing = false });
        }
    }
}