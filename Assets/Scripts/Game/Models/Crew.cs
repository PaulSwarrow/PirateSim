using System.Collections.Generic;
using Game.Actors.Character;
using Game.Interfaces;

namespace Game.Models
{
    public class Crew
    {
        public ICharacterLiveArea livigArea;
        public List<GameCharacter> staff;
    }
}