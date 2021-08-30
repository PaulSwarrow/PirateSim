using BehaviorDesigner.Runtime;
using Game.Actors.Character;

namespace Game.AI.BehaviorDesigner.Variables
{
    public class SharedCharacterActor : SharedVariable<GameCharacterActor>
    {
        public static implicit operator SharedCharacterActor(GameCharacterActor value)
        {
            return new SharedCharacterActor {Value = value};
        }
    }
}