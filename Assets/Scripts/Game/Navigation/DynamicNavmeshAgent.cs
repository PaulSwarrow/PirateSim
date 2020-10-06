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
    public class DynamicNavmeshAgent : BaseComponent, ICharacterMotor
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

        //TODO switch surface
        public Vector3 Velocity { get; set; }

        public float LocalRotation
        {
            get { return ghost.transform.eulerAngles.y; }
            set { ghost.transform.rotation = Quaternion.Euler(0, value, 0); }
        }

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

        private void Start()
        {
            ghost = Instantiate(ghostPrefab, transform.position, Quaternion.identity);
            ghost.name = name + " ghost";
            ghost.owner = this;
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

        private void FixedUpdate()
        {
            var offset = Velocity;
            if (surface) offset = surface.World2VirtualDirection(offset);
            ghost.agent.Move(offset * (Time.fixedDeltaTime * properties.speed));
        }

        public void GotToPlace(Vector3 worldPosition)
        {
            ghost.FindTargetPosition(worldPosition);
        }
    }
}