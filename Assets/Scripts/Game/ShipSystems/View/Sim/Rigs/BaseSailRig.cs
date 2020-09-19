using System;
using System.Collections.Generic;
using Lib;
using UnityEngine;

namespace ShipSystems.Sim
{
    public abstract class BaseSailRig : BaseComponent, ISailClothRig
    {
        public abstract int Priority { get; }
        protected abstract Color GizmoColor { get; }
        protected abstract bool CheckVertex(Vector3 vertex);
        protected abstract float GetValue(float progress);

        [SerializeField] private List<int> vertecies = new List<int>();

        public void Bake()
        {
            vertecies.Clear();
        }

        public bool OnVertexBaked(Vector3 vertex, int index)
        {
            if (CheckVertex(transform.InverseTransformPoint(vertex)))
            {
                vertecies.Add(index);
                return true;
            }

            return false;
        }


        public virtual void OnClothUpdate(ClothSkinningCoefficient[] coefficients, float progress)
        {
            var value = GetValue(progress);
            foreach (var vertex in vertecies)
            {
                var c = coefficients[vertex];
                c.maxDistance = value;
                coefficients[vertex] = c;
            }
        }

        public void DrawGizmos(Func<Vector3, Vector3> getVertexPosition, Vector3[] clothVertices)
        {
            Gizmos.color = GizmoColor;
            foreach (var index in vertecies)
            {
                Gizmos.DrawWireSphere(getVertexPosition(clothVertices[index]), 0.05f);
            }
        }
    }
}