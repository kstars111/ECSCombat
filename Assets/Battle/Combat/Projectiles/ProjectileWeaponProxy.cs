using Battle.Combat.AttackSources;
using System;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Battle.Combat
{
    public class ProjectileWeaponProxy : MonoBehaviour
    {
        [Tooltip("Projectile created by this weapon.")]
        public GameObject Projectile;

        [Tooltip("Range of the weapon")]
        public float Range;

        [Tooltip("Is the weapon armed to fire?")]
        public bool Armed;

        [Tooltip("Full attack cone for the weapon, in degrees")]
        public float AttackCone;
    }

    public class ProjectileWeaponBaker : Baker<ProjectileWeaponProxy>
    {
        public override void Bake(ProjectileWeaponProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            float coneInRad = authoring.AttackCone * Mathf.PI / 180f;
            var prefab = GetEntity(authoring.Projectile, TransformUsageFlags.None);
            //if (!dstManager.HasComponent<Projectile>(prefab))
            //    throw new Exception("ProjectileWeaponProxy Projectile archetype must have a Projectile component.");
            AddComponent(entity, new ProjectileWeapon { Projectile = prefab });
            AddComponent(entity, new TargettedTool { Armed = authoring.Armed, Range = authoring.Range, Cone = coneInRad, Firing = false });
        }
    }
}