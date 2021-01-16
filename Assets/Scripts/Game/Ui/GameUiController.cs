using Game.Ui.Ship;
using UnityEngine;

namespace Game.Ui
{
    public class GameUiController : MonoBehaviour
    {
        [SerializeField] private ShipControllUi shipControll;

        private const string OrdersButton = "Orders";

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown(OrdersButton))
            {
                shipControll.Show();
            }

            if (Input.GetButtonUp(OrdersButton))
            {
                shipControll.Hide();
            }

        }
    }
}