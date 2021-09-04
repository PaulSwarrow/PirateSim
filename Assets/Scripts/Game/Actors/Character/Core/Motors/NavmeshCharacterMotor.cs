namespace Game.Actors.Character.Core.Motors
{
    public class NavmeshCharacterMotor : ICharacterMotor
    {
       
        public CharacterActorContext context { get; set; }

        public void Enable()
        {
        }

        public void Disable()
        {
        }

        public void OnUpdate()
        {
            context.transform.position = context.agent.Position;
            context.transform.forward = context.agent.Forward;
        }

        public void OnRootMotion()
        {
            
        }
    }
}