using Game.Actors.Character;
using Services.AI.Interfaces;
using Services.AI.Structure;
using Services.AI.Structure.PropertyGetters;

namespace Game.AI
{
    public class CharacterBehaviourContext : BehaviorDataProxy
    {
        public ObjectBehaviorProperty<GameCharacterActor> actor;



        protected override void CreateProperties()
        {
            actor = Property<GameCharacterActor>("actor");

        }
    }
}