using Lib;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Navigation
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class VirtualNavmeshGhost : BaseComponent
    {
        public NavMeshAgent agent;

        public DynamicNavmeshAgent owner;
        private DynamicNavMeshSurface surface;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            // agent.updatePosition = false;
            agent.updateRotation = false;
        }

        public void SetSurface(DynamicNavMeshSurface surface)
        {
            // if(surface == this.surface) return;
            this.surface = surface;
            transform.parent = surface.virtualNavmesh.transform;
            RecalculatePosition();
        }
        public void RecalculatePosition()
        {
            agent.enabled = false;
            transform.parent = surface.virtualNavmesh.transform;
            transform.localPosition = surface.transform.InverseTransformPoint(owner.transform.position);
            agent.nextPosition = transform.position;
            agent.enabled = true;
            
            
        }

        public void ClearSurface()
        {
            agent.enabled = false;
            transform.parent = null;
            transform.position = owner.transform.position;
            agent.nextPosition = transform.position;
            surface = null;
            agent.enabled = true;
        }


        public Vector3 WorldPosition
        {
            get =>surface ? surface.Virtual2WorldPoint(transform.position) : transform.position;
            set => transform.position = surface ? surface.World2VirtualPoint(value) : value;
        }

        public Vector3 WorldForward
        {
            get =>  surface ? surface.Virtual2WorldDirection(transform.forward) : transform.forward; 
            set => transform.forward = surface? surface.World2VirtualDirection(value) : value;
        }

        public void FindTargetPosition(Vector3 worldPosition)
        {
            var localPosition = surface.transform.InverseTransformPoint(worldPosition);
            var virtualPosition = surface.virtualNavmesh.transform.TransformPoint(localPosition);
            if (NavMesh.SamplePosition(virtualPosition, out var hit, 10, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
            
        }
    }
}