using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace Lib.Navigation
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class VirtualNavmeshGhost : BaseComponent, INavSpaceConverter
    {
        private NavMeshAgent agent;

        public DynamicNavmeshAgent owner;
        private DynamicNavMeshSurface surface;
        private bool hasSurface;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            // agent.updatePosition = false;
            agent.updateRotation = false;
        }

        public bool UpdateRotation
        {
            get => agent.updateRotation;
            set => agent.updateRotation = value;
        }

        public void SetSurface(DynamicNavMeshSurface surface)
        {
            Assert.IsNull(this.surface);
            Assert.IsNotNull(surface);
            hasSurface = true;
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
            hasSurface = false;
            agent.enabled = false;
            transform.parent = null;
            transform.position = owner.transform.position;
            agent.nextPosition = transform.position;
            surface = null;
            agent.enabled = true;
        }


        public Vector3 WorldPosition
        {
            get => hasSurface ? surface.Virtual2WorldPoint(transform.position) : transform.position;
            set => transform.position = hasSurface ? surface.World2VirtualPoint(value) : value;
        }

        public Vector3 WorldForward
        {
            get => hasSurface ? surface.Virtual2WorldDirection(transform.forward) : transform.forward;
            set => transform.forward = hasSurface ? surface.World2VirtualDirection(value) : value;
        }

        public void GotoPosition(NavPoint navPoint)
        {
            if (NavMesh.SamplePosition(navPoint.LocalPosition, out var hit, 10, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
                agent.isStopped = false;
            }
        }

        public void Stop()
        {
            agent.isStopped = true;
        }

        public void Move(Vector3 velocity)
        {
            var movement = hasSurface ? surface.World2VirtualDirection(velocity) : velocity;
            agent.velocity = movement; //uniform with traveling
            // agent.Move(movement * Time.deltaTime);
        }


        public Vector3 WorldMoveVelocity => Virtual2WorldDirection(agent.velocity);

        public NavPoint GetCurrentNavPoint()
        {
            return new NavPoint(transform.position, surface);
        }

        public Vector3 Virtual2WorldPoint(Vector3 position)
        {
            return hasSurface ? surface.Virtual2WorldPoint(position) : position;
        }

        public Vector3 World2VirtualPoint(Vector3 position)
        {
            return hasSurface ? surface.World2VirtualPoint(position) : position;
        }

        public Vector3 Virtual2WorldDirection(Vector3 direction)
        {
            return hasSurface ? surface.Virtual2WorldDirection(direction) : direction;
        }

        public Vector3 World2VirtualDirection(Vector3 direction)
        {
            return hasSurface ? surface.World2VirtualDirection(direction) : direction;
        }

        public void SetSpeed(float speed)
        {
            agent.speed = speed;
        }
    }
}