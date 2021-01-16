using System.Collections;
using System.Collections.Generic;
using Game.Actors.Ship.View.Sim;
using Lib.UnityQuickTools.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SailSimTestUi : MonoBehaviour
{
    [SerializeField] private SailCloth sail;

    [SerializeField] private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sail.progress = slider.value;
    }
}
