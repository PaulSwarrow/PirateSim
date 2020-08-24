using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Ui;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private SailsSetupUi sailsSetup;

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
        if (Input.GetButtonDown(OrdersButton)) sailsSetup.Show();
        if (Input.GetButtonUp(OrdersButton)) sailsSetup.Hide();


        if (Input.GetButton(OrdersButton))
        {
            cameraLook.m_YAxis.m_MaxSpeed = 0;
            cameraLook.m_XAxis.m_MaxSpeed = 0;
        }
        else
        {
            cameraLook.m_YAxis.m_MaxSpeed = 2;
            cameraLook.m_XAxis.m_MaxSpeed = 300;
        }
    }
}