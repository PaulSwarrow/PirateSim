using System;
using UnityEngine;

namespace ShipSystems.Sim
{
    public interface ISailClothRig
    {
        void Bake();
        bool OnVertexBaked(Vector3 vertex, int index);
        int Priority { get;}
        void OnClothUpdate(ClothSkinningCoefficient[] coefficients, float progress);
        void DrawGizmos(Func<Vector3, Vector3> getVertexPosition, Vector3[] clothVertices);
    }
}