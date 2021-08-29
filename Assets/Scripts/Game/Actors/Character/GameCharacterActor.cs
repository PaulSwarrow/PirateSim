using Game.Actors.Character.Interactions;
using Game.Actors.Character.Motors;
using Lib;
using Lib.Navigation;
using UnityEngine;

namespace Game.Actors.Character
{
    public enum CharacterControlMode
    {
        none,
        ai,
        user
    }

    /*
     * A concrete instance of the character.
     * Provides facade api for GameCharacterMotdel
     * Can create character models on scene awake
     * Exists only in loaded/rendered area
     */
    [RequireComponent(typeof(DynamicNavmeshAgent))]
    public class GameCharacterActor : BaseComponent
    {
        public delegate void TriggerEvent(Collider trigger);

        public event TriggerEvent TriggerEnterEvent;
        public event TriggerEvent TriggerExitEvent;

        [SerializeField] public CharacterMainMotor defaultMotor;
        public CharacterMotor motor;
        public GameCharacterView view { get; private set; }
        public DynamicNavmeshAgent navigator { get; private set; }
        public WorkPlace currentWorkPlace  { get; set; }

        private void Awake()
        {
            view = GetComponentInChildren<GameCharacterView>();
            navigator = GetComponent<DynamicNavmeshAgent>();
            SetMotor(defaultMotor);
        }

        public void SetMotor(CharacterMotor motor)
        {
            if (this.motor == motor) return;
            this.motor?.Disable();
            this.motor = motor;
            this.motor.Enable(this);
        }

        private void Update()
        {
            motor.Update();
        }

        public void OnAreaEnter(Collider area)
        {
            TriggerEnterEvent?.Invoke(area);
        }

        public void OnAreaExit(Collider area)
        {
            TriggerExitEvent?.Invoke(area);
        }

        public void SetDefaultMotor()
        {
            SetMotor(defaultMotor);
        }

        public NavPoint GetCurrentNavPoint()
        {
            return navigator.GetCurrentNavPoint();
        }


    }
}