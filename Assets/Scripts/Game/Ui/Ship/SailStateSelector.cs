using System;
using System.Collections.Generic;
using App;
using Lib;
using Lib.Tools;
using ShipSystems;
using UnityEngine;

namespace Ui
{
    public class SailStateSelector : BaseComponent
    {

        public event Action<int> SelectEvent;

        private LocalFactory<SailStateUi> factory;
        private List<SailStateUi> items = new List<SailStateUi>();
        public SailGroupUi target;

        public SailGroup Model => target.Model;
        public ShipEntity Ship => target.Ship;

        private void Awake()
        {
            factory = new LocalFactory<SailStateUi>(transform);
        }

        private void Start()
        {
            foreach (var option in Model.Options)
            {
                var item = factory.Create();
                item.Jib = Model.jib;
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
                item.Fill = Vector3.Dot(Ship.localWind.normalized, SailGroup.GetNormaleVector(item.Angle, Model.jib));
            }

        }
    }
}