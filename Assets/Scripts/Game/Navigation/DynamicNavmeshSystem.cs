using UnityEngine;
using UnityEngine.AI;

namespace Game.Navigation
{
    public static class DynamicNavmeshSystem
    {
        public static bool SamplePosition(Vector3 sourcePosition, out NavPoint hit, float maxDistance, int filter)
        {
            if (NavMesh.SamplePosition(sourcePosition, out var hitPoint, maxDistance, filter))
            {
                hit = new WorldNavPoint(hitPoint.position);
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