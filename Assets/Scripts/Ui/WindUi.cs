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
        var forward = Camera.main.transform.forward;
        forward.y = 0;
        var angle = Vector3.SignedAngle(windSystem.Wind, forward, Vector3.up);
        Arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
        Label.text = windSystem.Wind.magnitude.ToString();
    }
}
