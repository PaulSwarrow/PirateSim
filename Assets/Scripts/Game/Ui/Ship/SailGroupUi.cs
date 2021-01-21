using System;
using Game.Actors.Ship;
using Game.Actors.Ship.Sails;
using Game.Actors.Ship.Sails.Data;
using Lib;
using UnityEngine;

namespace Game.Ui.Ship
{
    public abstract class SailGroupUi : BaseUiComponent
    {
        [SerializeField] private SailStateSelector selector;

        [SerializeField] private SailStateUi item;

        public event Action<SailOrder> OrderUpdateEvent; 
        
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
            OrderUpdateEvent?.Invoke(order);
        }

        private void OnStateSelected(int index)
        {
            order.task.angleIndex = index;
            HideSelector();
            OrderUpdateEvent?.Invoke(order);
        }

        private void ShowSelector(SailStateUi obj)
        {
            selector.gameObject.SetActive(true);
        }

        private void HideSelector()
        {
            selector.gameObject.SetActive(false);
        }


        public ShipActor Ship { get; set; }


        protected virtual void Update()
        {
            item.Angle = order.sails.Config.GetAngle(order.task.angleIndex);
            item.Fill = Vector3.Dot(Ship.localWind.normalized, SailMath.GetNormaleVector(item.Angle, item.Jib));
            item.Value = order.task.sailsUp;
        }
    }
}