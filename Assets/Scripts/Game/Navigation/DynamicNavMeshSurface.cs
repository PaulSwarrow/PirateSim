using App.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class DynamicNavMeshSurface : MonoBehaviour
{
    [SerializeField] private NavMeshData navMeshData;
    // Start is called before the first frame update
    public VirtualNavmesh virtualNavmesh;

    void Awake()
    {
        virtualNavmesh = new VirtualNavmesh(Instantiate(navMeshData), transform);

    }

    public Vector3 Virtual2WorldPoint(Vector3 position)
    {
        return virtualNavmesh.Virtual2WorldPoint(position);
    }

    public Vector3 Virtual2WorldDirection(Vector3 direction)
    {
        return transform.TransformDirection(direction);
    }

    public Vector3 World2VirtualDirection(Vector3 direction)
    {
        return transform.InverseTransformDirection(direction);
        
    }
}
