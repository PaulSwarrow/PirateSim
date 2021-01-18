using System;
using Lib;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Navigation
{
    /* Responsibility:
     * creates a virtual ghost and provides surface for it.
     * Solves ICharacterMotor input 
     */
    public class DynamicNavmeshAgent : BaseComponent
    {
        [Serializable]
        public class Properties
        {
            public float speed = 2f;
            public float angularVelocity = 1f;
        }

        [SerializeField] private Properties properties;
        private VirtualNavmeshGhost ghost;
        [SerializeField] private VirtualNavmeshGhost ghostPrefab;


        public Vector3 Forward
        {
            get => ghost.WorldForward;
            set => ghost.WorldForward = value;
        }

        public Vector3 NavPosition => ghost.transform.position;

        private void Awake()
        {
            ghost = Instantiate(ghostPrefab, transform.position, Quaternion.identity);
            ghost.name = name + " ghost";
            ghost.owner = this;
        }

        public void CheckSurface()
        {
            //TODO runtime support
            var ray = new Ray(transform.position + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out var hit, 1.5f) 
                && hit.rigidbody 
                && hit.rigidbody.TryGetComponent<DynamicNavMeshSurface>(out var surface))
            {
                ghost.SetSurface(surface);
            }
            else
            {
                ghost.ClearSurface();
            }
        }

        public void Sync(float blendWeights)
        {
            transform.position = Vector3.Lerp(transform.position, ghost.WorldPosition, blendWeights);
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.LookRotation(ghost.WorldForward, Vector3.up), blendWeights);
        }

        public void Move(Vector3 velocity)
        {
            ghost.Move(velocity);
        }

        public void StartTravel(NavPoint navPoint)
        {
            ghost.GotoPosition(navPoint);
        }

        public void StopTravel()
        {
            ghost.Stop();
        }

        public NavPoint GetCurrentNavPoint()
        {
            return ghost.GetCurrentNavPoint();
        }

        public INavSpaceConverter navSpace => ghost;
        public Vector3 LocalVelocity => transform.InverseTransformDirection(WorldVelocity);
        public Vector3 WorldVelocity => ghost.WorldMoveVelocity;

        public void SetSpeed(float speed)
        {
            ghost.SetSpeed(speed);
        }
    }
}