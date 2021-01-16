using Lib;
using Lib.UnityQuickTools.Collections;
using UnityEngine;

namespace Game.Actors.Character.Interactions
{
    public class WorkableObject : BaseComponent
    {
        [SerializeField] private WorkPlace[] workplaces;

        public bool OccupyWorkplace(GameCharacter owner, out WorkPlace workPlace)
        {
            if (workplaces.TryFind(item => !item.Occupied, out workPlace))
            {
                workPlace.Occupy(owner);
                return true;
            }

            return false;
        }
    }
}