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

    private Camera camera;
    private Animator animator;
    private Collider collider;

    private Rigidbody body;
    [SerializeField] private float speed = 1;
    private static readonly int ForwardKey = Animator.StringToHash("Forward");
    private Vector3 input;

    private Vector3 localForward;
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
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        var vector = camera.transform.TransformDirection(input);
        vector.y = 0;
        vector.Normalize();
        vector *= input.magnitude;

        collider.material = input.magnitude > 0 ? slideMaterial : frictionMaterial;
        if (input.magnitude > 0)
        {
            var velocity = vector * speed +  floor.GetPointVelocity(body.position);// + Physics.gravity;

            Debug.Log(vector + ", " + velocity);

            localForward = floor.transform.InverseTransformDirection(vector);
        }

        // body.position += (vector * speed + floor.GetPointVelocity(body.position)) * Time.fixedDeltaTime;
        // body.rotation = floor.rotation * q;
       
        var worldForward = floor.transform.TransformDirection(localForward);
        worldForward.y = 0;
        worldForward.Normalize();
        body.rotation = Quaternion.LookRotation(worldForward, Vector3.up);

        animator.SetFloat(ForwardKey, vector.magnitude, 0, 1);

        /*if (Physics.Raycast(new Ray(transform.position + Vector3.up, Vector3.down), out var info, 1.01f))
        {
            var bodyPosition = body.position;
            bodyPosition.y = info.point.y;
            body.position = bodyPosition;
        }*/
    }
}