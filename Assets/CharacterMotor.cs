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
    [SerializeField] private float walkSpeed = 2;
    [SerializeField] private float runSpeed = 4;

    private Camera camera;
    private Animator animator;
    private Collider collider;

    private Rigidbody body;
    private static readonly int ForwardKey = Animator.StringToHash("Forward");
    private static readonly int InAirKey = Animator.StringToHash("InAir");
    private Vector3 input;

    private Vector3 localForward;

    private bool grounded;
    private Vector3 floorUp;
    private float run;

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
        var justFall = false;
        if (Physics.SphereCast(ray, groundedRayRadius, out var hitinfo, 1 + groundRayLength - groundedRayRadius))
        {
            grounded = true;
            floorUp = ray.origin + Vector3.down * hitinfo.distance - hitinfo.point;
        }
        else
        {
            if (grounded) justFall = true;

            grounded = false;
        }


        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        input.Normalize();
        run = Mathf.Lerp(run, Input.GetButton("Run") ? 1 : 0, 0.1f);

        var movementSpeed = input.magnitude * (Mathf.Lerp(walkSpeed, runSpeed, run));

        collider.material = input.magnitude > 0 ? slideMaterial : frictionMaterial;

        Vector3 velocity;

        Vector3 desiredLocalForward = localForward;
        if (grounded)
        {
            var vector = camera.transform.TransformDirection(input);
            vector.y = 0;
            vector.Normalize();
            vector *= input.magnitude;
            velocity = Quaternion.FromToRotation(Vector3.up, floorUp) * (transform.forward * movementSpeed);
            velocity += floor.GetPointVelocity(body.position);

            Debug.DrawRay(transform.position, vector, Color.yellow);
            if (input.magnitude > 0)
            {
                desiredLocalForward = floor.transform.InverseTransformDirection(vector);
                desiredLocalForward.y = 0;
            }
        }
        else
        {
            velocity = body.velocity;
            if (justFall && velocity.y > 0) velocity.y = 0;
            velocity += Physics.gravity * Time.fixedDeltaTime;
        }

        body.velocity = velocity;


        var delta = Vector3.SignedAngle(localForward, desiredLocalForward, Vector3.up);
        localForward = Quaternion.Euler(0, delta * 0.1f, 0) * localForward;
        var worldForward = floor.transform.TransformDirection(localForward);
        worldForward.y = 0;
        worldForward.Normalize();
        body.rotation = Quaternion.LookRotation(worldForward, Vector3.up);

        animator.SetBool(InAirKey, !grounded);
        animator.SetFloat(ForwardKey, input.magnitude * Mathf.Lerp(1, 2, run), 1, 0.9f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = grounded ? Color.green : Color.red;
        var a = transform.position + Vector3.up;
        Gizmos.DrawWireSphere(a + Vector3.down * (1 + groundRayLength - groundedRayRadius), groundedRayRadius);

        if (grounded)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, floorUp);
        }
    }
}