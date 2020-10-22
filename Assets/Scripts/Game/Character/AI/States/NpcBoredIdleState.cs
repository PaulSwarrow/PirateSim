namespace App.Character.AI.States
{
    public class NpcBoredIdleState : GameCharacterState
    {
        public override void Start()
        {
            character.agent.SetDefaultMotor();
        }

        public override void Update()
        {
        }

        public override void Stop()
        {
            
        }
    }
}