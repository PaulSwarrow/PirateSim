using App.AI;

namespace App.Character
{
    public abstract class WorkingCharacterState<T> : GameCharacterState
    {
        private WorkPlace workPlace;
        public override void Start()
        {
            workPlace.Occupy(character.agent.view);
        }

        public override void Update()
        {
        }

        public override void Stop()
        {
            
        }
    }
}