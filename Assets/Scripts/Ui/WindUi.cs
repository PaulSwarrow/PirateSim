using System.Collections;
using System.Collections.Generic;
using App;
using UnityEngine;
using UnityEngine.UI;

public class WindUi : MonoBehaviour
{
    [SerializeField] private Image Arrow;
    [SerializeField] private Text Label;
    // Start is called before the first frame update
    private WindSystem windSystem;
    void Start()
    {
        windSystem = GameManager.current.GetSystem<WindSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        Arrow.transform.rotation = Quaternion.Euler(0, 0,  Vector3.SignedAngle(windSystem.Wind, Vector3.forward, Vector3.up));
        Label.text = windSystem.Wind.magnitude.ToString();
    }
}
