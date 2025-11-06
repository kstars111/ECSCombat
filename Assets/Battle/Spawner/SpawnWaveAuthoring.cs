using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Battle.Spawner
{
    [DisallowMultipleComponent]
    public class SpawnWaveAuthoring : MonoBehaviour
    {
        public List<GameObject> SpawnWaves;
    }

    public class SpawnWaveBaker : Baker<SpawnWaveAuthoring>
    {
        public override void Bake(SpawnWaveAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            var buffer = AddBuffer<SpawnWave>(entity);

            for (int i = 0; i < authoring.SpawnWaves.Count; i++)
            {
                var wave = GetEntity(authoring.SpawnWaves[i], TransformUsageFlags.None);
                buffer.Add(new SpawnWave
                {
                    Wave = wave
                });
            }
        }
    }
}