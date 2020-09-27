using UnityEngine;

namespace App.Character.Locomotion
{
    public class CharacterFloorProxy
    {
        private class Properties
        {
            public float groundedRayLength;
            public float inAirRayLength;
        }

        private readonly CharacterMotor character;
        private readonly FloorProxySettings settings;
        private readonly Properties properties;

        private Rigidbody floorRigidbody;
        public bool grounded { get; private set; }
        public bool dynamicFloor { get; private set; }
        public Vector3 Normale { get; private set; }
        public bool Active { get; set; }
        public Vector3 localForward { get; private set; }
        public Vector3 worldForward => TransformDirection(localForward);

        public CharacterFloorProxy(CharacterMotor character, FloorProxySettings settings)
        {
            this.character = character;
            this.settings = settings;
            localForward = character.transform.forward;
            properties = new Properties
            {
                groundedRayLength = 1 + (settings.groundRayLength - character.collider.radius),
                inAirRayLength = 1.01f
            };
        }

        public void Update()
        {
            if (CheckGrounded(out var ray, out var info))
            {
                grounded = true;
                Normale = ray.origin + Vector3.down * info.distance - info.point;
                character.body.position +=
                    Vector3.down * (info.distance - 1 + character.collider.radius); //magnet for slopes & stairs

                if (floorRigidbody != info.rigidbody)
                {
                    localForward = InverseTransformDirection(character.transform.forward);
                }

                floorRigidbody = info.rigidbody;
                dynamicFloor = floorRigidbody;
            }
            else
            {
                grounded = false;
                Normale = Vector3.up;
                floorRigidbody = null;
                dynamicFloor = false;
            }

        }

        public void Turn(float angle)
        {
            localForward = Quaternion.Euler(0, angle, 0) * localForward; //lerp forward direction
        }

        private bool CheckGrounded(out Ray ray, out RaycastHit info)
        {
            if (!Active)
            {
                ray = default;
                info = default;
                return false;
            }

            ray = new Ray(character.transform.position + Vector3.up, Vector3.down);
            var distance = grounded ? properties.groundedRayLength : properties.inAirRayLength;
            return Physics.SphereCast(ray, character.collider.radius, out info, distance);
        }
        

        public Vector3 TransformDirection(Vector3 vector3)
        {
            return dynamicFloor ? floorRigidbody.transform.TransformDirection(vector3) : vector3;
        }

        public Vector3 InverseTransformDirection(Vector3 vector3)
        {
            return dynamicFloor ? floorRigidbody.transform.InverseTransformDirection(vector3) : vector3;
        }

        public Vector3 GetVelocity(Vector3 position)
        {
            return dynamicFloor ? floorRigidbody.GetPointVelocity(position) : Vector3.zero;
        }
    }
}