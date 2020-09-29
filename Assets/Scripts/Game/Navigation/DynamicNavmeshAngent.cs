using System;
using Lib;
using UnityEngine;
using UnityEngine.AI;

namespace App.Navigation
{
    public class DynamicNavmeshAngent : BaseComponent
    {
        private VirtualNavmeshGhost ghost;
        [SerializeField] private VirtualNavmeshGhost ghostPrefab;
        [SerializeField] private DynamicNavMeshSurface surface;

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
        }
    }
}