using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

using Battle.Combat;
using Unity.Collections;
using Unity.Burst;
using Unity.Jobs;
using Unity.Entities.Graphics;
using Battle.Movement;
using Unity.Mathematics;
using UnityEditor;

namespace Battle.Effects
{
    /// <summary>
    /// Spawns effects of shields being struck.
    /// </summary>
    [
        UpdateAfter(typeof(PostAttackEntityBuffer)),
        //UpdateBefore(typeof(LateSimulationSystemGroup))
    ]
    public class SpawnShieldEffectSystem : SystemBase
    {
        Material ShieldMaterial;
        Mesh Mesh;

        protected override void OnCreate()
        {
            //ShieldMaterial = (Material)AssetDatabase.LoadAssetAtPath("Assets/Art/Effects/Shields/ShieldMaterial.mat", typeof(Material));
            ShieldMaterial = Resources.Load<Material>("ShieldMaterial");
            //Mesh = (Mesh)AssetDatabase.LoadAssetAtPath("Assets/Art/Misc/ScalePlane.fbx", typeof(Mesh));
            Mesh = Resources.Load<Mesh>("ScalePlane");

            if (Mesh == null)
                throw new System.Exception("Could not load mesh.");
            if (ShieldMaterial == null)
                throw new System.Exception("Could not load shield material.");
        }

        protected override void OnUpdate()
        {
            var bufferSystem = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
            var Buffer = bufferSystem.CreateCommandBuffer();
            var mesh = Mesh;
            var material = ShieldMaterial;

            Entities.ForEach(
                (ref ShieldHitEffect effect, ref LocalToWorld localToWorld, ref Shield shield) =>
                {
                    var e = Buffer.CreateEntity();
                    Buffer.AddComponent(e, new Lifetime { Value = 0.3f });
                    Buffer.AddComponent(e, LocalTransform.FromPositionRotationScale(
                        localToWorld.Position,
                        quaternion.LookRotation(effect.HitDirection, new float3(0.0f, 1.0f, 0.0f)),
                        shield.Radius));
                    Buffer.AddComponent(e, new LocalToWorld { });
                    Buffer.AddComponent(e, new RenderBounds {  });
                    Buffer.AddSharedComponent(e,
                    new RenderMesh
                    {
                        castShadows = UnityEngine.Rendering.ShadowCastingMode.Off,
                        receiveShadows = false,
                        mesh = mesh,
                        material = material
                    });
                }
                ).Run();
        }
    }
}