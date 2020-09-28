using System;
using System.Collections;
using System.Collections.Generic;
using App.Character.Locomotion;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CharacterMotor : MonoBehaviour
{
    //CONSTANTS
    private static readonly int ForwardKey = Animator.StringToHash("Forward");
    private static readonly int InAirKey = Animator.StringToHash("InAir");

    //SETTINGS:
    [SerializeField] private PhysicMaterial frictionMaterial;
    [SerializeField] private PhysicMaterial slideMaterial;
    [SerializeField] private float groundRayLength = 0.05f;
    [SerializeField] private float groundedRayRadius = 0.4f;
    [SerializeField] private float walkSpeed = 2;
    [SerializeField] private float runSpeed = 4;
    [SerializeField] private float jumpForce;
    [SerializeField] private FloorProxySettings settings;

    //CACHE
    private Camera camera;
    private Animator animator;
    public CharacterFloorProxy floorProxy { get; private set; }
    public CapsuleCollider collider { get; private set; }

    public Rigidbody body { get; private set; }

    //STATE
    private Vector3 input;


    private float run;
    private float gravityDelay;

    void Start()
    {
        camera = Camera.main;
        body = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        collider = GetComponent<CapsuleCollider>();

        floorProxy = new CharacterFloorProxy(this, settings);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        floorProxy.Active = gravityDelay <= 0;
        floorProxy.Update();

        if (gravityDelay > 0) gravityDelay -= Time.fixedDeltaTime;


        //INPUT:
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        input = Vector3.ClampMagnitude(input, 1); // fix for keyboard
        run = Mathf.Lerp(run, Input.GetButton("Run") ? 1 : 0, 0.1f);
        var jump = Input.GetButtonDown("Jump");

        //MOVEMENT
        var movementSpeed = input.magnitude * (Mathf.Lerp(walkSpeed, runSpeed, run));
        Vector3 velocity;
        Vector3 desiredLocalForward = floorProxy.localForward;
        if (floorProxy.grounded)
        {
            collider.material = input.magnitude > 0  || !floorProxy.Steady? slideMaterial : frictionMaterial;
            //get absolute vecotr
            var vector = camera.transform.TransformDirection(input);
            vector.y = 0; //compensate camera x-angle
            vector.Normalize();
            vector *= input.magnitude;
            //use local forward instead of vector ( for smooth character rotation)
            var floorQ = Quaternion.FromToRotation(Vector3.up, floorProxy.Normale);
            velocity = floorQ * (transform.forward * movementSpeed); //align to floor
            velocity += floorProxy.GetVelocity(body.position); //prevent sliding on moving ship

            Debug.DrawRay(transform.position, vector, Color.yellow);
            if (input.magnitude > 0)
            {
                desiredLocalForward = floorProxy.InverseTransformDirection(vector); //look towards movement
                desiredLocalForward.y = 0;
            }

            if (jump)
            {
                velocity.y += jumpForce;
                gravityDelay = .2f;
            }
            else if (!floorProxy.Steady)
            {
                velocity += Physics.gravity * Time.fixedDeltaTime;
            }
        }
        else
        {
            collider.material = slideMaterial;
            velocity = body.velocity;
            if (gravityDelay <= 0) velocity += Physics.gravity * Time.fixedDeltaTime;
        }

        body.velocity = velocity;


        var delta = Vector3.SignedAngle(floorProxy.localForward, desiredLocalForward, Vector3.up);
        // localForward = Quaternion.Euler(0, delta * 0.1f, 0) * localForward; //lerp forward direction
        floorProxy.Turn(delta * 0.1f);
        var worldForward = floorProxy.worldForward;
        worldForward.y = 0;
        worldForward.Normalize();
        body.rotation = Quaternion.LookRotation(worldForward, Vector3.up); //compensate ship floating rotation

        //ANIMATE
        animator.SetBool(InAirKey, !floorProxy.grounded);
        animator.SetFloat(ForwardKey, input.magnitude * Mathf.Lerp(1, 2, run), 1, 0.9f);
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        floorProxy.DrawGizmos();
    }
}