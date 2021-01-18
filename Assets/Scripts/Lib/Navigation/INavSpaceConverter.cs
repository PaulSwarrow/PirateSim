using UnityEngine;

namespace Lib.Navigation
{
    public interface INavSpaceConverter
    {
        Vector3 Virtual2WorldPoint(Vector3 position);

        Vector3 World2VirtualPoint(Vector3 position);

        Vector3 Virtual2WorldDirection(Vector3 direction);

        Vector3 World2VirtualDirection(Vector3 direction);
    }
}