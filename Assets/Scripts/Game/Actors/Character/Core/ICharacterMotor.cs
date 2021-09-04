namespace Game.Actors.Character.Core
{
    public interface ICharacterMotor
    {
        void Enable();

        void Disable();

        void OnUpdate();
        void OnRootMotion();
        CharacterActorContext context { get; set; }
    }
}