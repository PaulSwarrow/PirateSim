using System;
using System.Collections.Generic;
using System.Linq;
using Game.ShipSystems.Sails.Data;
using Lib;
using Lib.Tools;
using Lib.UnityQuickTools.Collections;

namespace Ui
{
    public class SailOrderListUi : BaseComponent
    {

        private List<SailOrderUi> items = new List<SailOrderUi>();
        private LocalFactory<SailOrderUi> factory;

        private void Awake()
        {
            factory = new LocalFactory<SailOrderUi>(transform);
        }
        
        public void Add(SailOrder order)
        {
            if (!items.TryFind(item => item.order == order, out var orderView))
            {
                orderView = factory.Create();
                orderView.order = order;
                items.Add(orderView);
            }
            orderView.UpdateView();
            
        }

        public void Remove(SailOrder order)
        {
            if (items.TryFind(item => item.order == order, out var orderView))
            {
                items.Remove(orderView);
                factory.Remove(orderView);
            }

        }
        
        public void Clear()
        {
            foreach (var orderUi in items)
            {
                factory.Remove(orderUi);
            }
            items.Clear();
        }

        public IEnumerable<SailOrder> GetOrders()
        {
            return items.Select(item => item.order);
        }
    }
}