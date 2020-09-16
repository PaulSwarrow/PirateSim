using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lib.Tools;
using Lib.UnityQuickTools.Collections;
using UnityEngine;

public class ClothJoints : MonoBehaviour
{
    [SerializeField] private Cloth cloth;
    [SerializeField] private SkinnedMeshRenderer mesh;
    [SerializeField] private float value;

    // Start is called before the first frame update
    [SerializeField] private List<ClothJoint> joints = new List<ClothJoint>();

    [Serializable]
    public class ClothJoint
    {
        public Transform bone;
        public int vertex = -1;
        public float value;
        public bool active;
    }

    private void Update()
    {
        var constraints = cloth.coefficients;
        foreach (var joint in joints)
        { 
            var coefficient = constraints[joint.vertex];
            coefficient.maxDistance = joint.active? joint.value: float.MaxValue;
            constraints[joint.vertex] = coefficient;
            
        }
        cloth.coefficients = constraints;
    }

    public void Bake()
    {
        var vertices = cloth.vertices.Select(GetVertexPosition).ToArray();
        var constraints = cloth.coefficients;
        foreach (var joint in joints)
        {
            var bonePosition = joint.bone.position;
            joint.vertex = vertices.Least(a => Vector3.Distance(a, bonePosition));
            var coefficient = constraints[joint.vertex];
            coefficient.maxDistance = joint.active? joint.value: float.MaxValue;
            constraints[joint.vertex] = coefficient;
        }

        cloth.coefficients = constraints;
    }

    // Update is called once per frame

    private Vector3 GetVertexPosition(Vector3 vertex) =>
        cloth.transform.TransformPoint(vertex) + transform.TransformVector(mesh.rootBone.localPosition);

    private void OnDrawGizmos()
    {
        var self = cloth.transform;

        foreach (var joint in joints)
        {
            if(joint.vertex == -1) continue;
            
            Gizmos.DrawWireSphere(GetVertexPosition(cloth.vertices[joint.vertex]), 0.05f);
            
        }
        /*foreach (var vertex in cloth.vertices)
        {
            var position = GetVertexPosition(vertex);
            Gizmos.DrawWireSphere(position, 0.05f);
        }*/
    }
}