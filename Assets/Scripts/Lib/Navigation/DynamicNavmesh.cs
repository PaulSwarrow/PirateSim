using System;
using UnityEngine;
using UnityEngine.AI;

namespace Lib.Navigation
{
    public static class DynamicNavmesh
    {
        public static NavPoint RequirePosition(Vector3 sourcePosition, float maxDistance = 0.1f, int filter = NavMesh.AllAreas)
        {
            if (SamplePosition(sourcePosition, out var point, maxDistance, filter))
            {
                return point;
            }
            throw new Exception($"Required position is not reachable!");
        }
        public static bool SamplePosition(Vector3 sourcePosition, out NavPoint hit, float maxDistance, int filter = NavMesh.AllAreas)
        {
            if (NavMesh.SamplePosition(sourcePosition, out var hitPoint, maxDistance, filter))
            {
                hit = new NavPoint(hitPoint.position);
                return true;
            }

            foreach (var vmesh in VirtualNavmesh.list)
            {
                if (vmesh.SamplePosition(sourcePosition, out var vmeshPoint, maxDistance, filter))
                {
                    hit = vmeshPoint;
                    return true;
                }
            }

            hit = default;
            return false;
        }
    }
}