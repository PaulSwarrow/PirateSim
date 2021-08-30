using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lib.Tools;
using Lib.UnityQuickTools.Collections;
using UnityEngine;

[Serializable]
public class ClothJoint
{
    public Transform bone;
    public int vertex = -1;
    public float value = 0f;
}
public class ClothJoints : MonoBehaviour
{
    [SerializeField] private Cloth cloth;
    [SerializeField] private SkinnedMeshRenderer mesh;
    [SerializeField] private float value;

    // Start is called before the first frame update
    [SerializeField] private List<ClothJoint> joints = new List<ClothJoint>();


    private void Update()
    {
        var constraints = cloth.coefficients;
        foreach (var joint in joints)
        {
            var coefficient = constraints[joint.vertex];
            coefficient.maxDistance = float.MaxValue;
            constraints[joint.vertex] = coefficient;
            // if (!joint.active) joint.bone.position = GetVertexPosition(cloth.vertices[joint.vertex]);
        }

        cloth.coefficients = constraints.ToArray();
    }

    private void Start()
    {
        cloth.selfCollisionDistance = 0.1f;
    }

    public void Bake()
    {
        var vertices = cloth.vertices.Select(GetVertexPosition).ToArray();
        joints.Clear();
        foreach (var bone in mesh.bones)
        {
            var joint = new ClothJoint
            {
                bone = bone,
                vertex = vertices.LeastIndex(a => Vector3.Distance(a, bone.position))
            };
            // var coefficient = constraints[joint.vertex];
            // coefficient.maxDistance = joint.active ? joint.value : float.MaxValue;
            // constraints[joint.vertex] = coefficient;
            joints.Add(joint);
        }

        var constraints = new List<ClothSkinningCoefficient>();
        var selfCollision = new uint[cloth.vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            selfCollision[i] = (uint) i;
            constraints.Add(new ClothSkinningCoefficient
            {
                collisionSphereDistance = cloth.coefficients[i].collisionSphereDistance,
                maxDistance = (joints.TryFind(item => item.vertex == i, out var joint) )
                    ? joint.value
                    : float.MaxValue
            });
        }

        cloth.coefficients = constraints.ToArray();
    }

    // Update is called once per frame

    private Vector3 GetVertexPosition(Vector3 vertex) =>
        transform.TransformPoint(vertex) + transform.TransformVector(mesh.rootBone.localPosition);

    private void OnDrawGizmos()
    {
        foreach (var joint in joints)
        {
            if (joint.vertex == -1) continue;

            Gizmos.DrawWireSphere(GetVertexPosition(cloth.vertices[joint.vertex]), 0.05f);
        }

        /*foreach (var vertex in cloth.vertices)
        {
            var position = GetVertexPosition(vertex);
            Gizmos.DrawWireSphere(position, 0.05f);
        }*/
    }
}