using System;
using Unity.Entities;
using Unity.Entities.Graphics;

namespace Battle.Combat
{
    [GenerateAuthoringComponent]
    [Serializable]
    [MaterialProperty("GameTime", MaterialPropertyFormat.Float)]
    public struct GameTimeMaterialProperty : IComponentData
    {
        public float Value;
    }
}