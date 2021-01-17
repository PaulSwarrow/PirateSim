using System;
using Game.Actors.Character.Interactions;
using Game.Navigation;

namespace Game.Interfaces
{
    public interface ICharacterLiveArea
    {
        bool TryFindPlace(float area, out NavPoint place);
    }
}