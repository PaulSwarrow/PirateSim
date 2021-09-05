using Game.Actors.Workplaces;
using Lib;
using Lib.UnityQuickTools.Collections;
using UnityEngine;

namespace Game.Actors.Character.Interaction
{
    public class WorkableObject : BaseComponent
    {
        [SerializeField] private WorkPlace[] workplaces;

        public bool OccupyWorkplace(GameCharacter owner, out WorkPlace workPlace)
        {
            if (workplaces.TryFind(item => !item.Occupied, out workPlace))
            {
                workPlace.Occupy(owner.actor);
                return true;
            }

            return false;
        }
    }
}