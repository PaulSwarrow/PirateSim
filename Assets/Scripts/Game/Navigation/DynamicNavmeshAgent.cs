using System;
using App.Character.Locomotion;
using Lib;
using UnityEngine;
using UnityEngine.AI;

namespace App.Navigation
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
        public DynamicNavMeshSurface surface;


        public Vector3 Forward
        {
            get => surface
                ? surface.Virtual2WorldDirection(ghost.agent.transform.forward)
                : ghost.agent.transform.forward;
            set
            {
                if (surface)
                {
                    ghost.agent.transform.forward = surface.World2VirtualDirection(value);
                }
                else
                {
                    ghost.agent.transform.forward = value;
                }
            }
        }

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
            if (Physics.Raycast(ray, out var hit, 1.5f) && hit.rigidbody && hit.rigidbody.TryGetComponent(out surface))
            {
                ghost.SetSurface(surface);
            }
            else
            {
                ghost.ClearSurface();
            }
        }

        public void Sync()
        {
            transform.position = ghost.WorldPosition;
            transform.forward = ghost.WorldForward;
        }

        public void Move(Vector3 offset)
        {
            if (surface) offset = surface.World2VirtualDirection(offset);
            ghost.agent.Move(offset );
        }

        public void GotToPlace(Vector3 worldPosition)
        {
            ghost.FindTargetPosition(worldPosition);
        }
    }
}