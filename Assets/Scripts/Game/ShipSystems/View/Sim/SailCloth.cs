using System;
using System.Collections.Generic;
using System.Linq;
using Lib;
using Lib.Tools;
using UnityEngine;

namespace ShipSystems.Sim
{
    public class SailCloth : BaseComponent
    {
        [SerializeField] private SkinnedMeshRenderer mesh;
        [SerializeField] private Cloth cloth;
        [SerializeField] public SailClothRig[] rigs = new  SailClothRig[0];
        [SerializeField] private BoxCollider[] holders;
        [HideInInspector] [SerializeField] private List<int> holderVertices = new List<int>();
        [Buttons("Bake")] [SerializeField] private bool buttons;


        public void Bake()
        {
            cloth = GetComponentInChildren<Cloth>();
            mesh = cloth.GetComponent<SkinnedMeshRenderer>();
            rigs = GetComponentsInChildren<SailClothRig>();

            //Reset cloth
            var coefficients = cloth.coefficients;
            var vertices = cloth.vertices.Select(GetVertexPosition).ToArray();
            holderVertices.Clear();
            for (var i = 0; i < coefficients.Length; i++)
            {
                var c = coefficients[i];
                c.maxDistance = float.MaxValue;
                coefficients[i] = c;
                
                foreach (var holder in holders)
                {
                    if (holder.bounds.Contains(vertices[i]))
                    {
                        holderVertices.Add(i);
                        break;
                    }
                }
            }
            cloth.coefficients = coefficients;

            
            //bake rigs
            foreach (var rig in rigs)
            {
                rig.Bake(FindVertex);
            }


            int FindVertex(Transform bone) => vertices.Least(a => Vector3.Distance(a, bone.position));

        }

        private void Start()
        { var coefficients = cloth.coefficients;
            var vertices = cloth.vertices.Select(GetVertexPosition).ToArray();

            foreach (var vertex in holderVertices)
            {
                var c = coefficients[vertex];
                c.maxDistance = 0;
                coefficients[vertex] = c;
            }
            cloth.coefficients = coefficients;
            
        }

        private void Update()
        {
            var coefficients = cloth.coefficients;
            foreach (var clothRig in rigs)
            {
                foreach (var joint in clothRig.joints)
                {
                    var coef = coefficients[joint.vertex];
                    coef.maxDistance = joint.value;
                    coefficients[joint.vertex] = coef;
                }
            }
            cloth.coefficients = coefficients;
        }
        
        private void OnDrawGizmos()
        {
            foreach (var clothRig in rigs)
            {
                foreach (var joint in clothRig.joints)
                {
                    
                    var position = GetVertexPosition(cloth.vertices[joint.vertex]);
                    Gizmos.DrawWireSphere(position, 0.05f);
                }
            }

            /*foreach (var vertex in cloth.vertices)
            {
                var position = GetVertexPosition(vertex);
                Gizmos.DrawWireSphere(position, 0.05f);
            }*/
        }

        private Vector3 GetVertexPosition(Vector3 vertex) =>
            transform.TransformPoint(vertex) + transform.TransformVector(mesh.rootBone.localPosition);
    }
}