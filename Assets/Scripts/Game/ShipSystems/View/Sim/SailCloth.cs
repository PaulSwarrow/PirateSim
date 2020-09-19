using System.Linq;
using Lib;
using UnityEngine;

namespace ShipSystems.Sim
{
    public class SailCloth : BaseComponent, ISailView
    {
        [SerializeField] private SkinnedMeshRenderer mesh;
        [SerializeField] private Cloth cloth;
        [SerializeField] private BaseSailRig[] rigs = new BaseSailRig[0];
        [SerializeField] private float windMultiplier = 3;
        [Buttons("Bake")] [SerializeField] private bool buttons;


        [Range(0, 1)] public float progress;

        public void Bake()
        {
            cloth = GetComponentInChildren<Cloth>();
            mesh = cloth.GetComponent<SkinnedMeshRenderer>();
            rigs = GetComponentsInChildren<BaseSailRig>().OrderBy(item => item.Priority).ToArray();

            //Reset cloth
            var coefficients = cloth.coefficients;
            var vertices = cloth.vertices.Select(GetVertexPosition).ToArray();


            //bake rigs
            foreach (var rig in rigs)
            {
                rig.Bake();
            }


            for (var i = 0; i < coefficients.Length; i++)
            {
                var c = coefficients[i];
                var v = vertices[i];
                c.maxDistance = float.MaxValue;
                coefficients[i] = c;

                foreach (var rig in rigs)
                {
                    if(rig.OnVertexBaked(v, i)) break;
                }
            }

            cloth.coefficients = coefficients;
        }

        private void Start()
        {
            // cloth.SetSelfAndInterCollisionIndices(new List<uint>(cloth.vertices.Select((item, i)=> (uint)i)));
            // cloth.selfCollisionDistance = 0.1f;
        }

        private void Update()
        {
            cloth.damping = 1 - progress;

            var coefficients = cloth.coefficients;
            foreach (var clothRig in rigs)
            {
                clothRig.OnClothUpdate(coefficients, progress);
            }

            cloth.coefficients = coefficients;
        }

        private enum GizmoMode
        {
            none,
            rigs,
            vertices
        }

        [SerializeField] private GizmoMode DrawGizmos;

        private void OnDrawGizmos()
        {
            switch (DrawGizmos)
            {
                case GizmoMode.rigs:

                    foreach (var clothRig in rigs)
                    {
                        clothRig.DrawGizmos(GetVertexPosition, cloth.vertices);
                    }


                    break;
                case GizmoMode.vertices:

                    Gizmos.color = Color.white;
                    foreach (var vertex in cloth.vertices)
                    {
                        var position = GetVertexPosition(vertex);
                        Gizmos.DrawWireSphere(position, 0.05f);
                    }

                    break;
            }
        }

        private Vector3 GetVertexPosition(Vector3 vertex) =>
            transform.TransformPoint(vertex) + transform.TransformVector(mesh.rootBone.localPosition);

        public float Progress
        {
            set => progress = value;
        }

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