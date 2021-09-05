using UnityEngine;

namespace Game.Actors.Workplaces
{
    public class IdleWorkPlace : WorkPlace
    {
        protected override WorkPlaceTag Tags => WorkPlaceTag.Chilling;

        [SerializeField] private float timeToGetBored;

        protected override void Awake()
        {
            base.Awake();
            MapParameter(WorkPlaceParameter.GetBoredTime, () => timeToGetBored);
        }
    }
}