using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Game.Navigation;
using Game.Ui;
using UnityEngine;

public class VirtualNavmeshDemo : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private DynamicNavmeshAgent target;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        StageUi.RequireCursor(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Input.GetButton("Orders")) {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            Debug.DrawLine(ray.origin, ray.origin + ray.direction* 45, Color.magenta);
            if (Physics.Raycast(ray, out var hit, 45))
            {
                target.GotToPlace(hit.point);
            }
        }
        
    }
}
