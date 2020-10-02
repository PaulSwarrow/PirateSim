using System;
using Lib;
using UnityEngine;
using UnityEngine.AI;

namespace App.Navigation
{
    public class DynamicNavmeshAngent : BaseComponent
    {
        private static readonly int ForwardKey = Animator.StringToHash("Forward");
        private static readonly int InAirKey = Animator.StringToHash("InAir");
        private VirtualNavmeshGhost ghost;
        [SerializeField] private VirtualNavmeshGhost ghostPrefab;
        private DynamicNavMeshSurface surface;
        [SerializeField] private Animator animator;

        private void Awake()
        {
            
            
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

        private void Update()
        {
            animator.SetBool(InAirKey, false);
            animator.SetFloat(ForwardKey, ghost.NormalizedVelocity.magnitude * 1.2f);
        }

        public void GotToPlace(Vector3 worldPosition)
        {
            ghost.FindTargetPosition(worldPosition);
        }
    }
}