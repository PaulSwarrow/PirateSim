using DI;

namespace Game.Actors.Character.Core
{
    public class CharacterCore
    {
        private ICharacterMotor currentMotor;
        [Inject] private CharacterActorContext _context;
        
        
        
        public void SetMotor<T>() where T : ICharacterMotor, new()
        {
            currentMotor?.Disable();
            currentMotor = new T();
            currentMotor.context = _context;
            currentMotor.Enable();
        }

        public void Update()
        {
            currentMotor.OnUpdate();
        }

        public void OnRootMotion()
        {
            currentMotor.OnRootMotion();
        }
    }
}