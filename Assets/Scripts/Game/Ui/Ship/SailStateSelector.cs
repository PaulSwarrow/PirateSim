using System;
using System.Collections.Generic;
using Game.Actors.Ship;
using Game.Actors.Ship.Sails;
using Lib;
using Lib.Tools;
using UnityEngine;

namespace Game.Ui.Ship
{
    public class SailStateSelector : BaseComponent
    {

        public event Action<int> SelectEvent;

        private LocalFactory<SailStateUi> factory;
        private List<SailStateUi> items = new List<SailStateUi>();
        public SailGroupUi target;

        public SailGroupModel Model => target.Model;
        public ShipEntity Ship => target.Ship;

        private void Awake()
        {
            factory = new LocalFactory<SailStateUi>(transform);
        }

        private void Start()
        {
            foreach (var option in Model.Config.configuration.availableAngles)
            {
                var item = factory.Create();
                item.Jib = Model.Jib;
                item.Angle = option;
                item.ClickEvent += OnClick;
                items.Add(item);
            }
            
        }

        private void OnClick(SailStateUi item)
        {
            SelectEvent?.Invoke(items.IndexOf(item));
        }

        protected virtual void Update()
        {
            foreach (var item in items)
            {
                item.Fill = Vector3.Dot(Ship.localWind.normalized, SailGroup.GetNormaleVector(item.Angle, Model.Jib));
            }

        }
    }
}