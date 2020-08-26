using System;
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
        
        private void Awake()
        {
            Hide();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            
        }

        private void Update()
        {
            target.Keel.wheel = wheelSlider.value;
        }
    }
}