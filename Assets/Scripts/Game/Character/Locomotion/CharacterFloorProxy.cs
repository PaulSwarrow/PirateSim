using UnityEngine;

namespace App.Character.Locomotion
{
    public class CharacterFloorProxy
    {
        private Rigidbody body;
        private bool dynamicFloor;
        public bool Changed { get; private set; }

        public void OnFloorCollider(Rigidbody rigidbody)
        {
            Changed = rigidbody != body;
            body = rigidbody;
            dynamicFloor = rigidbody;
        }

        public Vector3 TransformDirection(Vector3 vector3)
        {
            return dynamicFloor ? body.transform.TransformDirection(vector3) : vector3;
        }

        public Vector3 InverseTransformDirection(Vector3 vector3)
        {
            return dynamicFloor ? body.transform.InverseTransformDirection(vector3) : vector3;
        }

        public Vector3 GetVelocity(Vector3 position)
        {
            return dynamicFloor ? body.GetPointVelocity(position) : Vector3.zero;
        }
    }
}