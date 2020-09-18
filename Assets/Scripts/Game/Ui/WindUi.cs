using System.Collections;
using System.Collections.Generic;
using App;
using UnityEngine;
using UnityEngine.UI;

public class WindUi : MonoBehaviour
{
    [SerializeField] private Transform Arrow;
    [SerializeField] private Text Label;

    public Transform RelativeTo;
    // Start is called before the first frame update
    private WindSystem windSystem;
    void Start()
    {
        windSystem = GameManager.current.GetSystem<WindSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        var forward = RelativeTo != null? RelativeTo.forward: Vector3.forward;
        forward.y = 0;
        var angle = Vector3.SignedAngle(WindSystem.Wind, forward, Vector3.up);
        Arrow.rotation = Quaternion.Euler(0, 0, angle);
        Label.text = "Wind: "+ WindSystem.Wind.magnitude.ToString("0.00");
    }
}
