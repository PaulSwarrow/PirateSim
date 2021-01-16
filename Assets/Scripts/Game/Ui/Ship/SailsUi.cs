using System.Collections.Generic;
using Game.Actors.Ship;
using Game.Actors.Ship.Sails.Data;
using Lib;
using Lib.Tools;
using UnityEngine;

namespace Game.Ui.Ship
{
    public class SailsUi : BaseComponent
    {
        private ShipEntity target;
        [SerializeField] private SailOrderListUi ordersList;

        private LocalFactory<SimpleSailsUi> sailFactory;
        private LocalFactory<JibSailsUi> jibFactory;

        private List<SailGroupUi> items = new List<SailGroupUi>();
        private void Awake()
        {
            sailFactory = new LocalFactory<SimpleSailsUi>(transform);
            jibFactory = new LocalFactory<JibSailsUi>(transform);
            
        }

        private void OnOrderUpdate(SailOrder order)
        {
            if (order.IsEmpty())
            {
                ordersList.Remove(order);
            }
            else
            {
                ordersList.Add(order);
            }
        }

        private void OnEnable()
        {
            if (target == null)
            {
                target = GetComponentInParent<ShipControllUi>().Target;
                foreach (var group in target.Sails.sails)
                {
                    var item = group.Jib ? (SailGroupUi) jibFactory.Create() : sailFactory.Create();
                    item.Model = group;
                    item.Ship = target;
                    item.OrderUpdateEvent += OnOrderUpdate;
                    items.Add(item);
                }
                
            }
            ordersList.Clear();
            items.ForEach(item=> item.OnShown());
        }

        private void OnDisable()
        {
            items.ForEach(item=> item.OnHidden());
            target.Sails.ApplyOrders(ordersList.GetOrders());
        }
    }
}