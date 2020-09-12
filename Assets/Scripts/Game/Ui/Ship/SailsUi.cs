
using System.Collections.Generic;
using Lib;
using Lib.Tools;
using ShipSystems;

namespace Ui
{
    public class SailsUi : BaseComponent
    {
        private ShipEntity target;

        private LocalFactory<SimpleSailsUi> sailFactory;
        private LocalFactory<JibSailsUi> jibFactory;

        private List<SailGroupUi> items = new List<SailGroupUi>();
        private void Awake()
        {
            target = GetComponentInParent<ShipControllUi>().Target;
            sailFactory = new LocalFactory<SimpleSailsUi>(transform);
            jibFactory = new LocalFactory<JibSailsUi>(transform);
            
            foreach (var group in target.Sails.sails)
            {
                var item = group.Jib ? (SailGroupUi) jibFactory.Create() : sailFactory.Create();
                item.Model = group;
                item.Ship = target;
                items.Add(item);
            }
        }

        private void OnEnable()
        {
            items.ForEach(item=> item.OnShown());
        }

        private void OnDisable()
        {
            items.ForEach(item=> item.OnHidden());
        }
    }
}