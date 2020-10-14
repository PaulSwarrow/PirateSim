using System;
using System.Collections.Generic;
using App.AI;
using Lib.UnityQuickTools.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace App.Character.UserControl
{
    public class InteractiveObjectSelector
    {
        private HashSet<InteractiveObject> objectsNearby = new HashSet<InteractiveObject>();
        private Camera camera;
        
        public InteractiveObject SelectedObject { get; private set; }
        public bool HasObject { get; private set; }

        public void SetCamera(Camera camera)
        {
            this.camera = camera;
            UpdateSelection();
        }
        
        public void TrackObject(InteractiveObject interactiveObject)
        {
            objectsNearby.Add(interactiveObject);
        }

        public void UnTrackObject(InteractiveObject interactiveObject)
        {
            objectsNearby.Remove(interactiveObject);
        }

        public void UpdateSelection()
        {
            Assert.IsNotNull(camera);
            SelectedObject = objectsNearby.LeastOrDefault(GetDistanceFromScreenCenter);
            HasObject = SelectedObject;
        }

        private float GetDistanceFromScreenCenter(InteractiveObject item)
        {
            return Vector3.Distance(camera.WorldToViewportPoint(item.position), Vector3.one/2);
        }
    }
}