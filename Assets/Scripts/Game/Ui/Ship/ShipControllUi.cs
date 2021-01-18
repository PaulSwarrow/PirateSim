using Game.Actors.Ship;
using Lib;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui.Ship
{
    public class ShipControllUi : BaseComponent
    {
        [SerializeField] private ShipActor target;
        [SerializeField] private Slider wheelSlider;

        public ShipActor Target => target;
        
        
        public void Show()
        {
            StageUi.RequireCursor(this);
            gameObject.SetActive(true);
            
        }

        public void Hide()
        {
            StageUi.LoseCursor(this);
            gameObject.SetActive(false);
            
        }

        private void Update()
        {
            target.Keel.wheel = wheelSlider.value;
        }
    }
}