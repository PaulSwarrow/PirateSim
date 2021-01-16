using Lib;
using Lib.Tools;
using UnityEngine;

namespace Game.Actors.Ship
{
    [RequireComponent(typeof(Rigidbody))]
    public class Keel : BaseComponent
    {
        private Rigidbody rigidbody;
        private Transform self;
        [SerializeField]  private float sideDrag = 0.5f;
        [SerializeField] [Range(0, 1)] private float backDrag = 0.5f;
        [SerializeField] [Range(0, 1)] private float angularDrag = 0.5f;

        [SerializeField] [Range(-90, 90)] public float wheel = 0;
        [SerializeField] private float wheelInfluence = 1;

        private void Awake()
        {
            self = transform;
            rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            
            var right = self.right;
            right.y = 0;
            right.Normalize();
            var forward = self.forward;
            forward.y = 0;
            forward.Normalize();
            var sideVelocity = Vector3.Dot(right, rigidbody.velocity);
            var linearVelocity = Vector3.Dot(forward, rigidbody.velocity);

            var drag = right * (sideVelocity * sideDrag);
            if (linearVelocity < 0) drag += forward * (linearVelocity * backDrag);

            rigidbody.AddForce(-drag, ForceMode.VelocityChange);

            var angularVelocity = rigidbody.angularVelocity;
            angularVelocity.y -= angularVelocity.y * angularDrag;

            
            var localUp = Vector3.up;
            // var localUp = transform.InverseTransformDirection(Vector3.up);
            var m = wheelInfluence * linearVelocity;
            var q = Quaternion.AngleAxis(wheel * m *  Time.fixedDeltaTime, localUp);
            var euler = AngularMath.Minify(q.eulerAngles);
            rigidbody.AddRelativeTorque(euler, ForceMode.Acceleration);

            //todo static angular drag wheel influence
        }
    }
}