using App.Character.AI.States;

namespace App.Character.AI
{
    public class AiCharacterStateMachine : CharacterStateMachine
    {
        protected override GenericMap<GameCharacterState> PrepareStates()
        {
            var map = new GenericMap<GameCharacterState>();
            map.Set(new NpcBoredIdleState());
            map.Set(new NpcTravelState());
            map.Set(new NpcChillState());
            return map;
        }
    }
}