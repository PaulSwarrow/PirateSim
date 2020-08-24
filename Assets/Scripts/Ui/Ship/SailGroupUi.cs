using System;
using System.Collections.Generic;
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
        private SailGroup model;
        public SailGroup Model
        {
            get => model;
            set
            {
                model = value;
                item.Jib = model.jib;
                item.Angle = model.Options[model.Angle];

            }
        }

        private void Awake()
        {
            selector.target = this;
            selector.gameObject.SetActive(false);
            selector.SelectEvent += OnStateSelected;
            item.ClickEvent += ShowSelector; //unsubscribe
        }

        private void OnStateSelected(int index)
        {
            model.Angle = index;
            item.Angle = model.Options[model.Angle];
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

        private void OnDisable()
        {
            HideSelector();
        }

        public ShipEntity Ship { get; set; }


        protected virtual void Update()
        {
            var influence = Vector3.Dot(Ship.localWind, SailGroup.GetForceVector(item.Angle, model.jib));
            if (Mathf.Abs(influence) >= model.minInfluence)
                item.State = influence > 0 ? 1 : -1;
            else item.State = 0;

        }
    }
}