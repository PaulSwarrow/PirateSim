using System;
using App.AI;
using Lib;
using UnityEngine;

namespace App.Character
{
    public class WorkerCharacter : BaseComponent
    {
        private GameCharacterView view;
        private WorkPlace workPlace;

        private void Awake()
        {
            view = GetComponentInChildren<GameCharacterView>();

        }

        public void TakeWorkPlace(WorkPlace workPlace)
        {
            this.workPlace = workPlace;
            view.transform.SetParent(workPlace.transform, true);
            workPlace.Occupy(view);
            workPlace.ReleasedEvent += OnWorkPlaceReleased;
        }

        public void ReleaseWorkPlace()
        {
            workPlace.Release();
        }

        private void OnWorkPlaceReleased()
        {
            workPlace.ReleasedEvent -= OnWorkPlaceReleased;
            view.transform.parent = transform;
            view.transform.localPosition = Vector3.zero;
            workPlace = null;
        }
        
    }
}