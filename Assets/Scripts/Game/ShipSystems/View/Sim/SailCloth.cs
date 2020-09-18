using System;
using System.Collections.Generic;
using System.Linq;
using Lib;
using Lib.Tools;
using UnityEngine;

namespace ShipSystems.Sim
{
    public class SailCloth : BaseComponent, ISailView
    {
        [SerializeField] private SkinnedMeshRenderer mesh;
        [SerializeField] private Cloth cloth;
        [SerializeField] private SailClothRig[] rigs = new SailClothRig[0];
        [SerializeField] private BoxCollider[] holders;
        [HideInInspector] [SerializeField] private List<int> holderVertices = new List<int>();
        [Buttons("Bake")] [SerializeField] private bool buttons;
        [SerializeField] private float windMultiplier = 3;


        [Range(0, 1)] public float progress;
        
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
        {
            // cloth.SetSelfAndInterCollisionIndices(new List<uint>(cloth.vertices.Select((item, i)=> (uint)i)));
            cloth.selfCollisionDistance = 0.1f;
            var coefficients = cloth.coefficients;
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

                clothRig.progress = progress;
            }

            cloth.coefficients = coefficients;
        }

        private void OnDrawGizmos()
        {
            /*foreach (var clothRig in rigs)
            {
                foreach (var joint in clothRig.joints)
                {
                    var position = GetVertexPosition(cloth.vertices[joint.vertex]);
                    Gizmos.DrawWireSphere(position, 0.05f);
                }
            }*/

            /*foreach (var vertex in cloth.vertices)
            {
                var position = GetVertexPosition(vertex);
                Gizmos.DrawWireSphere(position, 0.05f);
            }*/
        }

        private Vector3 GetVertexPosition(Vector3 vertex) =>
            transform.TransformPoint(vertex) + transform.TransformVector(mesh.rootBone.localPosition);

        public float Progress {  set=> progress = value; }
        public Vector3 Wind
        {
            set
            {
                cloth.externalAcceleration = value * windMultiplier;
                cloth.randomAcceleration = Vector3.one * value.magnitude / 10;
            }
        }
    }
}