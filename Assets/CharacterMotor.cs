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

    private Camera camera;
    private Animator animator;
    private Collider collider;

    private Rigidbody body;
    [SerializeField] private float speed = 1;
    private static readonly int ForwardKey = Animator.StringToHash("Forward");
    private Vector3 input;

    private Vector3 localForward;

    private bool grounded;

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
        grounded = Physics.Raycast(
            new Ray(transform.position + Vector3.up, Vector3.down),
            out var groundCollision,
            1 + groundRayLength);

        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        var vector = camera.transform.TransformDirection(input);
        vector.y = 0;
        vector.Normalize();
        vector *= input.magnitude;

        collider.material = input.magnitude > 0 ? slideMaterial : frictionMaterial;

        var velocity = floor.GetPointVelocity(body.position);

        if (grounded)
        {
            velocity += vector * speed;
            if (input.magnitude > 0)
            {
                localForward = floor.transform.InverseTransformDirection(vector);
            }
        }
        else
        {
            velocity += Physics.gravity;
        }

        Debug.Log(vector + ", " + velocity);
        body.velocity = velocity;


        // body.position += (vector * speed + floor.GetPointVelocity(body.position)) * Time.fixedDeltaTime;
        // body.rotation = floor.rotation * q;

        var worldForward = floor.transform.TransformDirection(localForward);
        worldForward.y = 0;
        worldForward.Normalize();
        body.rotation = Quaternion.LookRotation(worldForward, Vector3.up);

        animator.SetFloat(ForwardKey, vector.magnitude, 0, 1);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = grounded ? Color.green : Color.red;
        var a = transform.position + Vector3.up;
        Gizmos.DrawLine(a, a + Vector3.down * (1 + groundRayLength));
    }
}