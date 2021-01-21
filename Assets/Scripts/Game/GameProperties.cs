using System;
using Game.Actors.Character;
using Game.Actors.Ship;

namespace Game
{
    [Serializable]
    public class GameProperties
    {
        public SailingConstantsConfig sailsConfig;
        public GameCharacterActor characterActorPrefab;
    }
}