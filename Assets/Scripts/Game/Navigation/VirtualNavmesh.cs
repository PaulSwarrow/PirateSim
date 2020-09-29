using System.Collections.Generic;
using Lib.Tools;
using UnityEngine;
using UnityEngine.AI;

namespace App.Navigation
{
    public class VirtualNavmesh
    {
        private static readonly Vector3 zero = Vector3.one * 1000;
        private const float boxSize = 100;
        private const float gap = 50;
        private static List<Vector3> positionsPool = new List<Vector3>();
        private static List<VirtualNavmesh> list = new List<VirtualNavmesh>();

        public Transform transform { get; private set; }

        private static Vector3 FindNewPosition()
        {
            if (positionsPool.Count > 0) return positionsPool.Shift();
            return zero + Vector3.right * (boxSize + gap) * list.Count;
        }

        public Vector3 position { get; }

        public VirtualNavmesh(NavMeshData data)
        {
            position = FindNewPosition();
            NavMesh.AddNavMeshData(data, position, Quaternion.identity);
            list.Add(this);
            transform = new GameObject().transform;
            transform.position = position;
            transform.name = "V Navmesh" + list.Count;
        }

        public void Dispose()
        {
            positionsPool.Add(position);
            list.Remove(this);
        }
    }
}