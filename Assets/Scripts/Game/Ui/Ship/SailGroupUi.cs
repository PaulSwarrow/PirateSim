using System;
using System.Collections.Generic;
using App;
using Game.ShipSystems.Sails.Data;
using Lib;
using Lib.Tools;
using ShipSystems;
using UnityEngine;

namespace Ui
{
    public abstract class SailGroupUi : BaseUiComponent
    {
        [SerializeField] private SailStateSelector selector;

        [SerializeField] private SailStateUi item;

        private SailOrder order = new SailOrder();

        private SailGroupModel model;
        public SailGroupModel Model
        {
            get => model;
            set
            {
                model = value;
                item.Jib = value.Jib;

            }
        }

        private void Awake()
        {
            selector.target = this;
            selector.gameObject.SetActive(false);
            selector.SelectEvent += OnStateSelected;
            item.AltClickEvent += OnValueChange;
            item.ClickEvent += ShowSelector; //unsubscribe
        }

        public void OnShown()
        {
            order = new SailOrder
            {
                sails = model, 
                task = model.Task.Copy()
            };
            Update();
        }


        public void OnHidden()
        {
            HideSelector();
            
        }

        private void OnValueChange(SailStateUi obj)
        {
            order.task.sailsUp = order.task.sailsUp == 1 ? 0 : 1;
        }

        private void OnStateSelected(int index)
        {
            order.task.angleIndex = index;
            HideSelector();
        }

        private void ShowSelector(SailStateUi obj)
        {
            selector.gameObject.SetActive(true);
        }

        private void HideSelector()
        {
            selector.gameObject.SetActive(false);
        }


        public ShipEntity Ship { get; set; }


        protected virtual void Update()
        {
            item.Angle = order.sails.Config.GetAngle(order.task.angleIndex);
            item.Fill = Vector3.Dot(Ship.localWind.normalized, SailGroup.GetNormaleVector(item.Angle, item.Jib));
            item.Value = order.task.sailsUp;
        }
    }
}