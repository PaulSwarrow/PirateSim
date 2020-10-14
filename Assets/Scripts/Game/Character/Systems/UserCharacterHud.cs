using App.AI;
using UnityEngine;

namespace App.Character.UserControl.Modules
{
    public class UserCharacterHud : GameSystem
    {
        private GameCharacterAgent agent;
        private InteractiveObjectSelector objectSelector = new InteractiveObjectSelector();

        public bool InteractionAvailable { get; private set; }
        public Vector3 InteractionViewportPoint { get; private set; }
        public InteractiveObject InteractionObject => objectSelector.SelectedObject;

        public override void Start()
        {
            objectSelector.SetCamera(Camera.main);
            agent = GameManager.CharacterUserControl.character.agent;
            agent.TriggerEnterEvent += OnTriggerEnter;
            agent.TriggerExitEvent += OnTriggerExit;
        }

        public override void Stop()
        {
            agent.TriggerEnterEvent -= OnTriggerEnter;
            agent.TriggerExitEvent -= OnTriggerExit;
        }

        public override void Update()
        {
            objectSelector.UpdateSelection();
            InteractionAvailable = objectSelector.HasObject;
            if (objectSelector.HasObject)
            {
                InteractionViewportPoint =
                    Camera.main.WorldToScreenPoint(objectSelector.SelectedObject.transform.position);
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