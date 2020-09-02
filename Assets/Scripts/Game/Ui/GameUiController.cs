using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DefaultNamespace;
using Ui;
using UnityEngine;

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