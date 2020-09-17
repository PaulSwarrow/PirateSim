using System;
using System.Collections.Generic;
using Lib;
using UnityEngine;

namespace ShipSystems.Sim
{
    public class SailClothRig : BaseComponent
    {
        [SerializeField] private Transform permanentJoint;
        public List<ClothJoint> joints = new List<ClothJoint>();

        [SerializeField] [Range(0, 1)] public float progress;

        private float minValue = 0.001f;
        private float maxValue = 1;
        
        
        private void Update()
        {
            var s = transform.localScale;
            s.y = Mathf.Max(0.01f, progress);
            transform.localScale = s;

            foreach (var joint in joints)
            {
                if (progress < 1)
                {
                    joint.value = Mathf.Lerp(minValue, joint.bone == permanentJoint ? maxValue : 5, Mathf.Pow(progress, 4));
                }
                else
                {
                    joint.value = joint.bone == permanentJoint ? maxValue : float.MaxValue;
                }
            }
        }

        public void Bake(Func<Transform, int> vertexProvider)
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                var joint = new ClothJoint
                {
                    bone = child,
                    value = 0,
                    vertex = vertexProvider(child)
                };
                joints.Add(joint);
            }
        }
    }
}