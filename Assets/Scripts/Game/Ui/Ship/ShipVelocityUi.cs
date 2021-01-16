using Lib;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui.Ship
{
    public class ShipVelocityUi : BaseComponent
    {
        [SerializeField] private Text linearTf;
        [SerializeField] private Text rotationTf;
        private ShipControllUi main;

        private void Start()
        {
            main = GetComponentInParent<ShipControllUi>();
            
        }

        private void Update()
        {
            linearTf.text = "Speed: " + main.Target.LinearVelocity.ToString("0.00");
            rotationTf.text = "Rotation: " + main.Target.AngularVelocity.ToString("0.00");
        }
    }
}