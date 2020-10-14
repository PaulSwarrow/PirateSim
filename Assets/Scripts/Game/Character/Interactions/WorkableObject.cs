using System;
using App.Character;
using Lib;
using Lib.UnityQuickTools.Collections;
using UnityEngine;

namespace App.AI
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

        public void ReleaseWorkPlace(WorkPlace workPlace)
        {
            workPlace.Release();//??
        }
    }
}