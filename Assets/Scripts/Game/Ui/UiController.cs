using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Ui;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private ShipControllUi shipControll;

    private const string OrdersButton = "Orders";
    private CinemachineFreeLook cameraLook;

    // Start is called before the first frame update
    void Start()
    {
        cameraLook = FindObjectOfType<CinemachineFreeLook>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(OrdersButton)) shipControll.Show();
        if (Input.GetButtonUp(OrdersButton)) shipControll.Hide();


        if (Input.GetButton(OrdersButton))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cameraLook.m_YAxis.m_MaxSpeed = 0;
            cameraLook.m_XAxis.m_MaxSpeed = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            cameraLook.m_YAxis.m_MaxSpeed = 2;
            cameraLook.m_XAxis.m_MaxSpeed = 300;
        }
    }
}