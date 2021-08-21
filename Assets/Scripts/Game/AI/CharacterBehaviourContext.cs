using Game.Actors.Character;
using Game.Actors.Character.AI;
using Services.AI.Interfaces;

namespace Game.AI
{
    public class CharacterBehaviourContext : IBehaviourTreeContext
    {
        public GameCharacterActor actor;


        public T GetProperty<T>(string property)
        {
            return default;
        }
    }
}