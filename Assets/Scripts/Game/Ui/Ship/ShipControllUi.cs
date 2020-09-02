using System;
using DefaultNamespace;
using Lib;
using ShipSystems;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class ShipControllUi : BaseComponent
    {
        [SerializeField] private ShipEntity target;
        [SerializeField] private Slider wheelSlider;

        public ShipEntity Target => target;
        
        
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