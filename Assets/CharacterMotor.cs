using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CharacterMotor : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Rigidbody floor;
    [SerializeField] private PhysicMaterial frictionMaterial;
    [SerializeField] private PhysicMaterial slideMaterial;
    [SerializeField] private float groundRayLength = 0.05f;
    [SerializeField] private float groundedRayRadius = 0.4f;

    private Camera camera;
    private Animator animator;
    private Collider collider;

    private Rigidbody body;
    [SerializeField] private float speed = 1;
    private static readonly int ForwardKey = Animator.StringToHash("Forward");
    private Vector3 input;

    private Vector3 localForward;

    private bool grounded;
    private Vector3 floorUp;

    void Start()
    {
        camera = Camera.main;
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();

        localForward = floor.transform.InverseTransformDirection(transform.forward);
    }


    private void Update()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var ray = new Ray(transform.position + Vector3.up, Vector3.down);
        if (Physics.SphereCast(ray, groundedRayRadius, out var hitinfo, 1 + groundRayLength - groundedRayRadius))
        {
            grounded = true;
            floorUp = ray.origin + Vector3.down * hitinfo.distance - hitinfo.point;
        }
        else grounded = false;


        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        collider.material = input.magnitude > 0 ? slideMaterial : frictionMaterial;

        Vector3 velocity;

        if (grounded)
        {
            var vector = camera.transform.TransformDirection(input);
            vector.y = 0;
            vector.Normalize();
            vector *= input.magnitude;
            vector = Quaternion.FromToRotation(Vector3.up, floorUp) * vector;

            Debug.DrawRay(transform.position, vector, Color.yellow);
            velocity = vector * speed +  floor.GetPointVelocity(body.position);
            if (input.magnitude > 0)
            {
                localForward = floor.transform.InverseTransformDirection(vector);
            }
        }
        else
        {
            velocity = body.velocity + Physics.gravity * Time.fixedDeltaTime;
        }

        body.velocity = velocity;


        // body.position += (vector * speed + floor.GetPointVelocity(body.position)) * Time.fixedDeltaTime;
        // body.rotation = floor.rotation * q;

        var worldForward = floor.transform.TransformDirection(localForward);
        worldForward.y = 0;
        worldForward.Normalize();
        body.rotation = Quaternion.LookRotation(worldForward, Vector3.up);

        animator.SetFloat(ForwardKey, input.magnitude, 0, 1);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = grounded ? Color.green : Color.red;
        var a = transform.position + Vector3.up;
        var ray = new Ray(transform.position + Vector3.up, Vector3.down);
        Gizmos.DrawWireSphere(a + Vector3.down * (1 + groundRayLength - groundedRayRadius), groundedRayRadius);

        if (grounded)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, floorUp);

        }
    }
}