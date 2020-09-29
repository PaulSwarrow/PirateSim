using System.Collections;
using System.Collections.Generic;
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
        virtualNavmesh = new VirtualNavmesh(Instantiate(navMeshData));

    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 FromWorldToVirtual(Vector3 position)
    {
        return transform.InverseTransformPoint(position) + virtualNavmesh.position;
    }
    public Vector3 Virtual2WorldPoint(Vector3 position)
    {
        return transform.TransformPoint(position - virtualNavmesh.position);
    }

    public Vector3 Virtual2WorldDirection(Vector3 direction)
    {
        return transform.TransformDirection(direction);
    }
}
