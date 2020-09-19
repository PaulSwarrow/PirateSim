using System;
using System.Collections.Generic;
using Lib;
using UnityEngine;

namespace ShipSystems.Sim
{
    public class SailClothJoint : BaseSailRig
    {
        [SerializeField] private float radius = 0.5f;
        [SerializeField] private float maxValue = 1;
        [SerializeField] private float minValue = 0.01f;
        
        public override int Priority => 1;
        protected override Color GizmoColor => Color.cyan;

        protected override bool CheckVertex(Vector3 vertex) => vertex.magnitude  < radius;


        protected override float GetValue(float progress) => Mathf.Lerp(minValue, maxValue, Mathf.Pow(progress, 4));
    }
}