using UnityEngine;

namespace Game.Navigation
{
    public class NavPoint
    {
        public Vector3 worldPosition => surface ? surface.Virtual2WorldPoint(navPosition) : navPosition;
        public Vector3 navPosition;
        public DynamicNavMeshSurface surface;
    }
}