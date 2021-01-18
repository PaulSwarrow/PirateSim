using UnityEngine;
using UnityEngine.AI;

namespace Game.Navigation
{
    public class DynamicNavMeshSurface : MonoBehaviour, INavSpaceConverter
    {
        [SerializeField] private NavMeshData navMeshData;
        // Start is called before the first frame update
        public VirtualNavmesh virtualNavmesh;

        void Awake()
        {
            virtualNavmesh = new VirtualNavmesh(Instantiate(navMeshData), this);

        }

        public Vector3 Virtual2WorldPoint(Vector3 position)
        {
            return virtualNavmesh.Virtual2WorldPoint(position);
        }

        public Vector3 World2VirtualPoint(Vector3 position)
        {
            return virtualNavmesh.FromWorld2VirtualPoint(position);
        }

        public Vector3 Virtual2WorldDirection(Vector3 direction)
        {
            return transform.TransformDirection(direction);
        }

        public Vector3 World2VirtualDirection(Vector3 direction)
        {
            return transform.InverseTransformDirection(direction);
        
        }
    }
}
