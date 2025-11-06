using Battle.AI;
using Battle.Movement;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Battle.Combat
{
    public class ProjectileProxy : MonoBehaviour
    {
        [Tooltip("Attack transferred by this projectile to the target.")]
        public GameObject Attack;

        public bool Homing;
    }

    public class ProjectileBaker : Baker<ProjectileProxy>
    {
        public override void Bake(ProjectileProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            var prefab = GetEntity(authoring.Attack, TransformUsageFlags.None);
            AddComponent(entity, new Projectile { AttackEntity = prefab, ReachedTarget = false });
            AddComponent(entity, new Instigator());
            AddComponent(entity, new Target());

            if (authoring.Homing)
            {
                AddComponent(entity, new Homing());
                AddComponent(entity, new TurnToDestinationBehaviour());
                AddComponent(entity, new Heading());
            }
        }
    }
}