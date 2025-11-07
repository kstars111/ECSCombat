using Unity.Entities;
using Unity.Entities.Graphics;
using UnityEngine;

namespace Battle.Effects
{
    public class LaserRendererProxy : MonoBehaviour
    {
        public Material Material;
    }

    public class LaserRendererBaker : Baker<LaserRendererProxy>
    {
        public override void Bake(LaserRendererProxy authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new LaserRenderer());

            var renderMeshDescription = new RenderMeshDescription(
                shadowCastingMode: UnityEngine.Rendering.ShadowCastingMode.Off,
                receiveShadows: false);

            RenderMeshUtility.AddComponents(
                entity,
                this,
                renderMeshDescription,
                new RenderMeshArray(new Material[] { authoring.Material }, new Mesh[] { new Mesh() }),
                MaterialMeshInfo.FromRenderMeshArrayIndices(0, 0));
        }
    }

    public struct LaserRenderer : IComponentData
    {
        bool dummy;
    }
}