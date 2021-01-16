using Game.Actors.Ship.Sails.Data;
using Lib;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui.Ship
{
    public class SailOrderUi : BaseComponent
    {
        [SerializeField] private Text title;
        [SerializeField] private Text description;

        public SailOrder order;

        public void UpdateView()
        {
            title.text = order.sails.name;
            var text = "";
            if (order.task.angleIndex != order.sails.Task.angleIndex) 
                text += $"angle: {order.sails.Config.GetAngle(order.task.angleIndex)}\n";
            if (order.task.sailsUp != order.sails.Task.sailsUp) 
                text += $"value: {order.task.sailsUp}\n";
            description.text = text;
        }

    }
}