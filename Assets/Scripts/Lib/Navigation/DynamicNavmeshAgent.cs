using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Lib.Navigation
{
    /* Responsibility:
     * creates a virtual ghost and provides surface for it.
     * Solves ICharacterMotor input 
     */
    public class DynamicNavmeshAgent : BaseComponent
    {
        [Serializable]
        public class Properties
        {
            public float speed = 2f;
            public float angularVelocity = 1f;
        }

        [SerializeField] private Properties properties;
        private VirtualNavmeshGhost ghost;
        [SerializeField] private VirtualNavmeshGhost ghostPrefab;


        public Vector3 Forward
        {
            get => ghost.WorldForward;
            set => ghost.WorldForward = value;
        }

        public Vector3 Position => ghost.WorldPosition;

        private void Awake()
        {
            ghost = Instantiate(ghostPrefab, transform.position, Quaternion.identity);
            ghost.name = name + " ghost";
            ghost.owner = this;
        }

        private void OnEnable()
        {
            CheckSurface();
        }

        private void OnDisable()
        {
            if(IsTraveling) StopTravel();
        }

        public void CheckSurface()
        {
            //TODO runtime support
            var ray = new Ray(transform.position + Vector3.up, Vector3.down);
            if (Physics.Raycast(ray, out var hit, 1.5f)
                && hit.rigidbody
                && hit.rigidbody.TryGetComponent<DynamicNavMeshSurface>(out var surface))
            {
                ghost.SetSurface(surface);
            }
            else
            {
                ghost.ClearSurface();
            }
        }

        public void Move(Vector3 velocity)
        {
            Assert.IsFalse(IsTraveling);
            ghost.Move(velocity);
        }

        public void StartTravel(NavPoint navPoint)
        {
            Assert.IsFalse(IsTraveling);
            IsTraveling = true;
            ghost.UpdateRotation = true;
            ghost.WorldForward = transform.forward;
            ghost.GotoPosition(navPoint);
        }

        public void StopTravel()
        {
            ghost.UpdateRotation = false;
            Assert.IsTrue(IsTraveling);
            IsTraveling = false;
            ghost.Stop();
        }

        public NavPoint GetCurrentNavPoint()
        {
            return ghost.GetCurrentNavPoint();
        }

        public Vector3 RelactiveVelocity => transform.InverseTransformDirection(WorldVelocity);
        public Vector3 WorldVelocity => ghost.WorldMoveVelocity;
        public bool IsTraveling { get; private set; }

        public void SetSpeed(float speed)
        {
            ghost.SetSpeed(speed);
        }
    }
}