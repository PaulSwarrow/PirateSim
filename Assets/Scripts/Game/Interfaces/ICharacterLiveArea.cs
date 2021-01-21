using System;
using Game.Actors.Character.Interactions;
using Lib.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Interfaces
{
    public interface ICharacterLiveArea
    {
        bool TryFindPlace(Vector3 worldPosition, float area, out NavPoint place);
        bool TryFindWorkPlace(Predicate<WorkPlace> predicate, out WorkPlace workPlace);
        NavPoint FindRandomPlace();
    }
}