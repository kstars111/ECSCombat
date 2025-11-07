using System;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Battle.Spawner
{
    [DisallowMultipleComponent]
    public class SpawnWaveComponentAuthoring : MonoBehaviour
    {
        public List<GameObject> Templates;
        public List<float> Number;
    }

    public class SpawnWaveComponentBaker : Baker<SpawnWaveComponentAuthoring>
    {
        public override void Bake(SpawnWaveComponentAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            var buffer = AddBuffer<SpawnWaveComponent>(entity);

            if (authoring.Templates.Count != authoring.Number.Count)
                throw new Exception("Template and Number lists must be of matching length.");

            for (int i = 0; i < authoring.Templates.Count; i++)
            {
                var toSpawn = GetEntity(authoring.Templates[i], TransformUsageFlags.None);
                buffer.Add(new SpawnWaveComponent
                {
                    Number = authoring.Number[i],
                    Template = toSpawn
                });
            }
        }
    }
}