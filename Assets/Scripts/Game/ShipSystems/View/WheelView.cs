using System.Collections;
using System.Collections.Generic;
using ShipSystems;
using UnityEngine;

public class WheelView : MonoBehaviour
{
    private Keel model;

    // Start is called before the first frame update
    void Start()
    {
        model = GetComponentInParent<Keel>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(0,0, -model.wheel);
    }
}
