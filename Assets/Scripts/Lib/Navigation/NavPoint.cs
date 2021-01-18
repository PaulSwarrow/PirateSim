using UnityEngine;

namespace Lib.Navigation
{
    public abstract class NavPoint
    {
        public static float Distance(NavPoint a, NavPoint b)
        {
            return Vector3.Distance(a.worldPosition, b.worldPosition);
        }

        public static NavPoint Create(Vector3 position, DynamicNavMeshSurface surface = null)
        {
            if (surface) return new VirtualNavPoint(surface, position);
            return new WorldNavPoint(position);
        }

        public abstract Vector3 virtualPosition { get; }
        public abstract Vector3 worldPosition { get; }
    }

    public class WorldNavPoint : NavPoint
    {
        public WorldNavPoint(Vector3 position)
        {
            virtualPosition = worldPosition = position;
        }

        public override Vector3 virtualPosition { get; }
        public override Vector3 worldPosition { get; }
    }

    public class VirtualNavPoint : NavPoint
    {
        private DynamicNavMeshSurface surface;

        public VirtualNavPoint(DynamicNavMeshSurface surface, Vector3 virtualPosition)
        {
            this.surface = surface;
            this.virtualPosition = virtualPosition;
        }

        public override Vector3 worldPosition => surface.Virtual2WorldPoint(virtualPosition);
        public override Vector3 virtualPosition { get; }
    }
}