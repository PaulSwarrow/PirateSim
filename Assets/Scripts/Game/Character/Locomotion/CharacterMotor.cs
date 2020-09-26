using System;
using System.Collections;
using System.Collections.Generic;
using App.Character.Locomotion;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CharacterMotor : MonoBehaviour
{
    // Start is called before the first frame update

    public CharacterFloorProxy floor { get; private set; }
    [SerializeField] private PhysicMaterial frictionMaterial;
    [SerializeField] private PhysicMaterial slideMaterial;
    [SerializeField] private float groundRayLength = 0.05f;
    [SerializeField] private float groundedRayRadius = 0.4f;
    [SerializeField] private float walkSpeed = 2;
    [SerializeField] private float runSpeed = 4;
    [SerializeField] private float jumpForce;

    private Camera camera;
    private Animator animator;
    private Collider collider;

    private Rigidbody body;
    private static readonly int ForwardKey = Animator.StringToHash("Forward");
    private static readonly int InAirKey = Animator.StringToHash("InAir");
    private Vector3 input;

    private Vector3 localForward; //cache for moving floors

    private bool grounded;
    private Vector3 floorUp;
    private float run;
    private float gravityDelay;

    void Start()
    {
        camera = Camera.main;
        body = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        collider = GetComponent<Collider>();

        floor = new CharacterFloorProxy();
    }


    private void Update()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GroundCheck();

        if (gravityDelay > 0) gravityDelay -= Time.fixedDeltaTime;


        //INPUT:
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        input = Vector3.ClampMagnitude(input, 1); // fix for keyboard
        run = Mathf.Lerp(run, Input.GetButton("Run") ? 1 : 0, 0.1f);
        var jump = Input.GetButtonDown("Jump");

        //MOVEMENT
        var movementSpeed = input.magnitude * (Mathf.Lerp(walkSpeed, runSpeed, run));
        collider.material = input.magnitude > 0 || !grounded ? slideMaterial : frictionMaterial;
        Vector3 velocity;
        Vector3 desiredLocalForward = localForward;
        if (grounded)
        {
            //get absolute vecotr
            var vector = camera.transform.TransformDirection(input);
            vector.y = 0; //compensate camera x-angle
            vector.Normalize();
            vector *= input.magnitude;
            //use local forward instead of vector ( for smooth character rotation)
            var floorQ = Quaternion.FromToRotation(Vector3.up, floorUp);
            velocity = floorQ * (transform.forward * movementSpeed); //align to floor
            velocity += floor.GetVelocity(body.position); //prevent sliding on moving ship

            Debug.DrawRay(transform.position, vector, Color.yellow);
            if (input.magnitude > 0)
            {
                desiredLocalForward = floor.InverseTransformDirection(vector); //look towards movement
                desiredLocalForward.y = 0;
            }

            if (jump)
            {
                velocity.y += jumpForce;
                gravityDelay = .2f;
                grounded = false;
            }
        }
        else
        {
            velocity = body.velocity;
            if (gravityDelay <= 0) velocity += Physics.gravity * Time.fixedDeltaTime;
        }

        body.velocity = velocity;


        var delta = Vector3.SignedAngle(localForward, desiredLocalForward, Vector3.up);
        localForward = Quaternion.Euler(0, delta * 0.1f, 0) * localForward; //lerp forward direction
        var worldForward = floor.TransformDirection(localForward);
        worldForward.y = 0;
        worldForward.Normalize();
        body.rotation = Quaternion.LookRotation(worldForward, Vector3.up); //compensate ship floating rotation

        animator.SetBool(InAirKey, !grounded);
        animator.SetFloat(ForwardKey, input.magnitude * Mathf.Lerp(1, 2, run), 1, 0.9f);
    }

    private void GroundCheck()
    {
        var ray = new Ray(transform.position + Vector3.up, Vector3.down);
        var distance = grounded ? 1 + (groundRayLength - groundedRayRadius) : 1.01f;
        if (gravityDelay <= 0 && Physics.SphereCast(ray, groundedRayRadius, out var raycastHit, distance))
        {
            grounded = true;
            floorUp = ray.origin + Vector3.down * raycastHit.distance - raycastHit.point;
            body.position += Vector3.down * (raycastHit.distance - 1 + groundedRayRadius); //magnet for slopes & stairs

            floor.OnFloorCollider(raycastHit.rigidbody);
        }
        else
        {
            grounded = false;
            floorUp = Vector3.up;
            floor.OnFloorCollider(null);
        }
        if (floor.BakeRotation) localForward = floor.InverseTransformDirection(transform.forward);
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