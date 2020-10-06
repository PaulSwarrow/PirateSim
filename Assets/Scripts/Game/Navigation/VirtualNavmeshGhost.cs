using System;
using Lib;
using UnityEngine;
using UnityEngine.AI;

namespace App.Navigation
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
            if(surface == this.surface) return;
            agent.enabled = false;
            this.surface = surface;
            transform.parent = surface.virtualNavmesh.transform;
            transform.localPosition = surface.transform.InverseTransformPoint(owner.transform.position);
            agent.nextPosition = transform.position;
            agent.enabled = true;

        }

        public void ClearSurface()
        {
            if(surface == null) return;
            agent.enabled = false;
            transform.parent = null;
            transform.position = surface.transform.TransformPoint(owner.transform.position);
            agent.nextPosition = transform.position;
            surface = null;
            agent.enabled = true;
        }

        private void Update()
        {
            owner.transform.position = surface ? surface.Virtual2WorldPoint(transform.position) : transform.position;
            owner.transform.forward = surface ? surface.Virtual2WorldDirection(transform.forward) : transform.forward;
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