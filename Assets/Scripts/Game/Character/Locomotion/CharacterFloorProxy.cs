using System;
using UnityEngine;

namespace App.Character.Locomotion
{
    public class CharacterFloorProxy
    {
        private class Properties
        {
            public float groundedRayLength;
            public float inAirRayLength;
            public float rayOffset;
            public float steadyCheckRayLength;
            public float slopeMaxStepHeight;
            public float maxAngle;
            public float slopeMaxAngle;
        }

        private readonly CharacterMotor character;
        private readonly Properties properties;

        private Rigidbody floorRigidbody;

        //GETTERS:
        public bool grounded { get; private set; }
        public bool dynamicFloor { get; private set; }
        public Vector3 Normale { get; private set; } = Vector3.up;
        public bool Steady { get; private set; }

        public Vector3 localForward { get; private set; }
        public Vector3 worldForward => TransformDirection(localForward);

        public CharacterFloorProxy(CharacterMotor character, FloorProxySettings settings)
        {
            this.character = character;
            localForward = character.transform.forward;
            properties = new Properties
            {
                groundedRayLength = settings.groundRayLength,
                inAirRayLength = .1f,
                rayOffset = 1f,
                steadyCheckRayLength = character.collider.radius / Mathf.Cos(Mathf.Deg2Rad * settings.maxAngle),
                maxAngle = settings.maxAngle,
                slopeMaxAngle = settings.slopeMaxAngle,
                slopeMaxStepHeight = character.collider.radius / Mathf.Cos(Mathf.Deg2Rad * settings.slopeMaxAngle)
            };
        }

        //API:
        public bool Active { get; set; }

        public void Update()
        {
            if (CheckGrounded(out var info))
            {
                grounded = true;
                Normale = info.normale;
                Steady = info.steady;

                //magnet for slopes & stairs:
                character.body.position += Vector3.down * info.excessiveDistance;

                //cache local direction
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
                Steady = false;
                floorRigidbody = null;
                dynamicFloor = false;
            }
        }


        public void Turn(float angle)
        {
            localForward = Quaternion.Euler(0, angle, 0) * localForward; //lerp forward direction
        }

        private bool CheckGrounded(out GroundHitInfo info)
        {
            info = default;
            if (!Active) return false;

            var sphereCast = SphereCast();
            var rayHit = RayCast();
            var normalAngle = Vector3.Angle(Vector3.up, sphereCast.Normale);

            if (sphereCast.Hit && rayHit == RayHit.step)
            {
                info = new GroundHitInfo
                {
                    normale = sphereCast.Normale,
                    rigidbody = sphereCast.Rigidbody,
                    excessiveDistance = sphereCast.ExcessiveDistance,
                };

                info.steady = true;
                if (normalAngle < properties.maxAngle)
                {
                    //steady floor
                }
                else
                {
                    //prevent move towards
                    info.normale = Normale; //recover previous normale
                }


                return true;
            }
            else
            {
                //fall free
                return false;
            }
        }

        private SphereHit SphereCast()
        {
            var result = new SphereHit();
            var ray = new Ray(
                character.transform.position + Vector3.up * (character.collider.radius + properties.rayOffset),
                Vector3.down);
            var distance = (grounded ? properties.groundedRayLength : properties.inAirRayLength) + properties.rayOffset;
            if (Physics.SphereCast(ray, character.collider.radius, out var hit, distance))
            {
                result.Hit = true;
                result.Rigidbody = hit.rigidbody;
                result.ExcessiveDistance = hit.distance - properties.rayOffset;
                var sphereCenter = ray.origin + Vector3.down * hit.distance;
                result.Normale = sphereCenter - hit.point;
            }
            else
            {
                result.Normale = Vector3.up;
            }

            return result;
        }

        public RayHit RayCast()
        {
            var ray2 = new Ray(character.transform.position + Vector3.up * (character.collider.radius), Vector3.down);
            var l = properties.slopeMaxStepHeight;

            if (Physics.Raycast(ray2, out var hit2, l))
            {
                var pureDistance = hit2.distance;
                if (pureDistance <= properties.steadyCheckRayLength)
                {
                    Debug.DrawLine(ray2.origin, hit2.point, Color.green);
                    return RayHit.step;
                }
                else
                {
                    Debug.DrawLine(ray2.origin, hit2.point, Color.cyan);
                    return RayHit.slope;
                }
            }
            else
            {
                Debug.DrawLine(ray2.origin, ray2.direction * l, Color.red);
                return RayHit.none;
            }
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

        public void DrawGizmos()
        {
            Gizmos.color = grounded ? Color.green : Color.red;
            var a = character.transform.position + Vector3.up * character.collider.radius;
            Gizmos.DrawWireSphere(a, character.collider.radius);

            if (grounded)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawRay(character.transform.position, Normale);
            }
        }
    }
}