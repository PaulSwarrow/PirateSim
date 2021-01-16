using System.Collections.Generic;
using Lib.UnityQuickTools.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Navigation
{
    public class VirtualNavmesh
    {
        private static readonly Vector3 zero = Vector3.one * 1000;
        private const float boxSize = 100;
        private const float gap = 50;
        private static List<Vector3> positionsPool = new List<Vector3>();
        public static List<VirtualNavmesh> list = new List<VirtualNavmesh>();
        


        public Transform surface { get; private set; }
        public Transform transform { get; private set; }

        private static Vector3 FindNewPosition()
        {
            if (positionsPool.Count > 0) return positionsPool.Shift();
            return zero + Vector3.right * (boxSize + gap) * list.Count;
        }

        public Vector3 Position { get; }

        public VirtualNavmesh(NavMeshData data, Transform surface)
        {
            this.surface = surface;
            Position = FindNewPosition();
            NavMesh.AddNavMeshData(data, Position, Quaternion.identity);
            list.Add(this);
            transform = new GameObject().transform;
            transform.position = Position;
            transform.name = "V Navmesh" + list.Count;
        }
        

        public Vector3 FromWorld2VirtualPoint(Vector3 position)
        {
            return transform.TransformPoint(surface.InverseTransformPoint(position));
        }
        public Vector3 Virtual2WorldPoint(Vector3 position)
        {
            return surface.TransformPoint(transform.InverseTransformPoint(position));
        }
        
        
        public bool SamplePosition(Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int filter = NavMesh.AllAreas)
        {
            var virtualPosition = FromWorld2VirtualPoint(sourcePosition);
            return NavMesh.SamplePosition(virtualPosition, out hit, maxDistance, filter);
        }
        

        public void Dispose()
        {
            positionsPool.Add(Position);
            list.Remove(this);
        }
    }
}