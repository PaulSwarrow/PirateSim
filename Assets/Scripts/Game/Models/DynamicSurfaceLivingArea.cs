using System;
using Game.Actors.Character.Interactions;
using Game.Interfaces;
using Lib.Navigation;
using Lib.UnityQuickTools.Collections;
using UnityEngine;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

namespace Game.Models
{
    public class DynamicSurfaceLivingArea : ICharacterLiveArea
    {
        private DynamicNavMeshSurface surface;
        private WorkPlace[] workplaces;

        public DynamicSurfaceLivingArea(DynamicNavMeshSurface surface)
        {
            this.surface = surface;
            workplaces = surface.GetComponentsInChildren<WorkPlace>();
        }

        public bool TryFindPlace(Vector3 worldPosition, float area, out NavPoint place)
        {
            return surface.virtualNavmesh.SamplePosition(worldPosition, out place, area);
        }

        public bool TryFindWorkPlace(Predicate<WorkPlace> predicate, out WorkPlace workPlace)
        {
            return workplaces.TryFind(predicate, out workPlace);
        }

        public NavPoint FindRandomPlace()
        {
            var point = new Vector3(
                Random.Range(-50,50),
                Random.Range(-50,50),
                Random.Range(-50,50)
                );
            point += surface.transform.position;
            surface.virtualNavmesh.SamplePosition(point,out var result, 100);
            return result;
        }
    }
}