using UnityEngine;
using UnityEngine.AI;

namespace Game.Navigation
{
    public static class DynamicNavmeshSystem
    {
        public static bool SamplePosition(Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int filter)
        {
            if (NavMesh.SamplePosition(sourcePosition, out hit, maxDistance, filter))
            {
                return true;
            }

            foreach (var vmesh in VirtualNavmesh.list)
            {
                if (vmesh.SamplePosition(sourcePosition, out hit, maxDistance, filter))
                {
                    return true;
                }
            }

            return false;
        }
    }
}