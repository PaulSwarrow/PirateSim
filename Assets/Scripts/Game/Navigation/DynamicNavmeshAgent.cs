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
        private static readonly int ForwardKey = Animator.StringToHash("Forward");
        private static readonly int InAirKey = Animator.StringToHash("InAir");
        private VirtualNavmeshGhost ghost;
        [SerializeField] private VirtualNavmeshGhost ghostPrefab;
        public DynamicNavMeshSurface surface;
        [SerializeField] private Animator animator;


        //TODO switch surface
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

        public void Move(Vector3 offset)
        {
            offset = surface.World2VirtualDirection(offset);
            ghost.agent.Move(offset);
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

        public float LocalRotation
        {
            get { return ghost.transform.eulerAngles.y; }
            set { ghost.transform.rotation = Quaternion.Euler(0, value, 0); }
        }


        private void Update()
        {
            // ghost.agent.steeringTarget
            //must not be here
            animator.SetBool(InAirKey, false);
            animator.SetFloat(ForwardKey, ghost.NormalizedVelocity.magnitude * 1.2f);
        }

        public void GotToPlace(Vector3 worldPosition)
        {
            ghost.FindTargetPosition(worldPosition);
        }
    }
}