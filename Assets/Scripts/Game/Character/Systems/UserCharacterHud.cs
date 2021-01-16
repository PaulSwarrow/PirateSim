using System;
using App.AI;
using App.Interfaces;
using UnityEngine;

namespace App.Character.UserControl.Modules
{
    public class UserCharacterHud : IGameSystem
    {
        public event Action<WorkableObject> WorkEvent; 
        
        private GameCharacterAgent agent;
        private InteractiveObjectSelector objectSelector = new InteractiveObjectSelector();
        private GameCharacter character;

        public bool InteractionAvailable { get; private set; }
        public Vector3 InteractionViewportPoint { get; private set; }
        public InteractiveObject InteractionObject => objectSelector.SelectedObject;
        public bool Active { get; set; }

        public void Init()
        {
            
        }

        public void Start()
        {
            objectSelector.SetCamera(Camera.main);
            character = GameManager.CharacterUserControl.character;
            agent = GameManager.CharacterUserControl.character.agent;
            agent.TriggerEnterEvent += OnTriggerEnter;
            agent.TriggerExitEvent += OnTriggerExit;
            
            GameManager.UpdateEvent += Update;
        }

        public void Stop()
        {
            agent.TriggerEnterEvent -= OnTriggerEnter;
            agent.TriggerExitEvent -= OnTriggerExit;
            GameManager.UpdateEvent -= Update;
        }

        public void Update()
        {
            objectSelector.UpdateSelection();
            var available = objectSelector.HasObject && Active;
            InteractionAvailable = available;
            if (available)
            {
                InteractionViewportPoint =
                    Camera.main.WorldToScreenPoint(objectSelector.SelectedObject.transform.position);
                
                
                if (Input.GetButtonDown("Action"))
                {
                    if (GameManager.CharacterHud.InteractionObject.TryGetComponent<WorkableObject>(out var workableObject))
                    {
                        WorkEvent?.Invoke(workableObject);
                    }
                }
                
                
            }
        }

        private void OnTriggerEnter(Collider trigger)
        {
            if (trigger.TryGetComponent<InteractiveObject>(out var item))
            {
                objectSelector.TrackObject(item);
            }
        }

        private void OnTriggerExit(Collider trigger)
        {
            if (trigger.TryGetComponent<InteractiveObject>(out var item))
            {
                objectSelector.UnTrackObject(item);
            }
        }
    }
}