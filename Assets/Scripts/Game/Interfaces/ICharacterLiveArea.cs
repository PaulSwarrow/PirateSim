using System;
using Game.Actors.Character.Interactions;
using Game.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Interfaces
{
    public interface ICharacterLiveArea
    {
        bool TryFindPlace(Vector3 worldPosition, float area, out VirtualNavPoint place);
    }
}