using System;
using UnityEngine;

namespace Lib.Navigation
{
    [Serializable]
    public class NavPoint
    {
        [SerializeField] private Vector3 position;
        [SerializeField] private DynamicNavMeshSurface surface;
        [SerializeField] private string label;

        public static float Distance(NavPoint a, NavPoint b)
        {
            return Vector3.Distance(a.WorldPosition, b.WorldPosition);
        }

        public NavPoint()
        {
            
        }
        
        public NavPoint(Vector3 position, DynamicNavMeshSurface surface = null)
        {
            this.position = position;
            SetSurface(surface);
        }


        public void SetSurface(DynamicNavMeshSurface surface)
        {
            if (surface == this.surface) return;
            var cachePosition = WorldPosition;

            this.surface = surface;
            if (surface)
            {
                position = surface.World2VirtualPoint(cachePosition);
            }
        }

        public Vector3 LocalPosition => position;
        public Vector3 WorldPosition => surface ? surface.Virtual2WorldPoint(position) : position;
    }
}