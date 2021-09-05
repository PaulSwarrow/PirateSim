using DI;
using Game.Actors.Character.Core;
using Game.Actors.Character.Input;
using Game.Actors.Character.Interactions;
using Game.Actors.Character.StateMachine;
using Lib;
using Lib.Navigation;
using UnityEngine;

namespace Game.Actors.Character
{

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

        [SerializeField] private CharacterActorSettings _settings;
        private CharacterStateMachine _stateMachine;
        private DependencyContainer _di;
        private CharacterInput _input;
        private CharacterActorContext _context = new CharacterActorContext();
        private GameCharacterView _view;

        public CharacterCore Core => _context.core;
        public GameCharacterView View => _view;
        public WorkPlace currentWorkPlace  { get; set; }
        public CharacterInput Input => _input;
        public CharacterStateMachine StateMachine => _stateMachine;


        private void Awake()
        {
            _di = new DependencyContainer();
            _di.Register( _view = GetComponentInChildren<GameCharacterView>());
            _di.Register(_view.animator);
            _di.Register(GetComponent<DynamicNavmeshAgent>());
            _di.Register<ICharacterInput>(_input = new CharacterInput());
            _di.Register( new CharacterCore());
            _di.Register(transform);
            _di.Register(_settings);
            _di.Register(_context);
            _di.Register(this);

            _di.InjectDependencies();

            _context.view.MoveEvent += OnRootMotion;

            _stateMachine = new CharacterStateMachine(_di);
        }

        private void OnRootMotion()
        {
            _context.core.OnRootMotion();
        }

        private void Update()
        {
            _context.core.Update();
            _stateMachine.Update();
            _input.CleanUp();
        }

        public void OnAreaEnter(Collider area)
        {
            TriggerEnterEvent?.Invoke(area);
        }

        public void OnAreaExit(Collider area)
        {
            TriggerExitEvent?.Invoke(area);
        }

        public NavPoint GetCurrentNavPoint()
        {
            return _context.agent.GetCurrentNavPoint();
        }


    }
}