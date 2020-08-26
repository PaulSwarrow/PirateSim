using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using App;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NaturalFloatingEntity : MonoBehaviour
{
    private Rigidbody body;
    private WaterSystem waterSystem;

    [SerializeField] private Vector2[] points = new Vector2[0];
    [SerializeField] private float depthBeforeSubmerged = 1f;
    [SerializeField] private float displacementAmount = 3f;
    [SerializeField] private float waterDrag = 0.99f;
    [SerializeField] private float waterAngularDrag = 0.5f;
    [SerializeField] private float waterline = 0.4f;


    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        body.centerOfMass = Vector3.zero;
    }

    private void Start()
    {
        waterSystem = WaterSystem.instance;
    }

    private void FixedUpdate()
    {
        if (points.Length == 0) return; //TODO handle as one zero-point
        var massDistributionTotal = points.Sum(item => item.magnitude);
        foreach (var point in points)
        {
            var gravityPart = Physics.gravity /points.Length;
            var forcePoint = GetWorldPoint(point);
            // body.AddForceAtPosition( Physics.gravity /points.Length, forcePoint, ForceMode.Acceleration);
            var waterHeight = waterSystem.GetWaterHeight(forcePoint);
            if (forcePoint.y < waterHeight)
            {
                /*var multiplier = Mathf.Clamp01((waterHeight - forcePoint.y) / depthBeforeSubmerged) *
                                 displacementAmount;*/
                var multiplier = Mathf.Max(0,(waterHeight - forcePoint.y) / depthBeforeSubmerged);

                multiplier = displacementAmount * Mathf.Pow(multiplier, 2);//smoother accelerations
                body.AddForceAtPosition(new Vector3(0, Mathf.Abs(gravityPart.y) * multiplier
                        , 0),
                    forcePoint, ForceMode.Acceleration);

                body.AddForce(-body.velocity * (displacementAmount * waterDrag * Time.fixedDeltaTime),
                    ForceMode.VelocityChange);
                body.AddTorque(
                    -body.angularVelocity * (displacementAmount * waterAngularDrag * Time.fixedDeltaTime),
                    ForceMode.VelocityChange);
            }
        }
    }

    private Vector3 GetWorldPoint(Vector2 point) => transform.TransformPoint(new Vector3(point.x, waterline, point.y));

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        foreach (var point in points)
        {
            Gizmos.DrawWireSphere(GetWorldPoint(point), 0.3f);
        }
        
        
            
    }
}