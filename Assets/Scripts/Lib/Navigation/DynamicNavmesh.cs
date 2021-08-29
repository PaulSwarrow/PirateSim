using UnityEngine;
using UnityEngine.AI;

namespace Lib.Navigation
{
    public static class DynamicNavmesh
    {
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