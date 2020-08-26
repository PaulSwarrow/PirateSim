using System;
using Lib;
using Lib.Tools;
using ShipSystems;
using UnityEngine;

namespace Ui
{
    public class SailsUi : BaseComponent
    {
        private ShipEntity target;

        private LocalFactory<SimpleSailsUi> sailFactory;
        private LocalFactory<JibSailsUi> jibFactory;

        private void Awake()
        {
            target = GetComponentInParent<ShipControllUi>().Target;
            sailFactory = new LocalFactory<SimpleSailsUi>(transform);
            jibFactory = new LocalFactory<JibSailsUi>(transform);
            
        }

        private void Start()
        {

            foreach (var group in target.sails)
            {
                SailGroupUi item = group.jib ? (SailGroupUi) jibFactory.Create() : sailFactory.Create();
                item.Model = group;
                item.Ship = target;

            }
        }
    }
}