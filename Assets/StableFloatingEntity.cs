using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using App;
using Lib.Tools;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class StableFloatingEntity : MonoBehaviour
{
    private Rigidbody body;
    private WaterSystem waterSystem;

    [SerializeField] private Vector3[] points = new Vector3[0];
    [SerializeField] private float depthBeforeSubmerged = 1f;
    [SerializeField] private float displacementAmount = 3f;
    [SerializeField] private float angularStability = 0.5f;
    [SerializeField] private float waterDrag = 0.99f;
    [Range(0,1)][SerializeField] private float waterAngularDrag = 0.5f;
    [SerializeField] private float waterline = 0.4f;


    [SerializeField]
    private  float massDistribution = 0.1f;
    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        waterSystem = WaterSystem.instance;
    }

    //TODO simple optimizations
    private void FixedUpdate()
    {
        if (points.Length == 0) return; //TODO handle as one zero-point
        var massDistributionTotal = points.Sum(item => item.magnitude);
        var angularForce = Vector3.zero;
        foreach (var point in points)
        {
            var gravityPart = Physics.gravity *(body.mass/points.Length);
            var forcePoint = GetWorldPoint(point);
            var pointVector = transform.TransformVector(point);
            // body.AddForceAtPosition( Physics.gravity /points.Length, forcePoint, ForceMode.Acceleration);
            var waterHeight = waterSystem.GetWaterHeight(forcePoint);
            if (forcePoint.y < waterHeight)
            {
                var multiplier = Mathf.Max(0,(waterHeight - forcePoint.y) / depthBeforeSubmerged) *
                                 displacementAmount;

                var linerForce = -gravityPart * multiplier;
                // multiplier *= multiplier;//smoother accelerations
                //gravityPart -= gravityPart * multiplier;
                body.AddForce(linerForce);
                /*body.AddForceAtPosition(new Vector3(0, Mathf.Abs(gravityPart.y) * multiplier
                        , 0), transform.position + transform.up,
                     ForceMode.Force);*/


                var forceResultPoint = forcePoint + angularStability * linerForce/body.mass;
                
                var q  = Quaternion.FromToRotation(pointVector, forceResultPoint - transform.position);
                var euler = AngularMath.Minify(q.eulerAngles);
                euler.y = 0;
                /*Debug.Log(euler);
                Debug.DrawRay(forcePoint, linerForce/body.mass);
                Debug.DrawLine(transform.position, forceResultPoint, Color.green);
                Debug.DrawLine(transform.position, forcePoint, Color.red);*/

                
                body.AddTorque(euler, ForceMode.Acceleration);
                angularForce += euler;
                
                
                body.AddForce(-body.velocity * (displacementAmount * waterDrag * Time.fixedDeltaTime),
                    ForceMode.VelocityChange);
                // body.AddTorque(-body.angularVelocity *waterAngularDrag,ForceMode.VelocityChange);
                
            }
            
        }
        
        // body.AddTorque(angularForce, ForceMode.Force);
    }

    private Vector3 GetWorldPoint(Vector3 point)
    {
        return transform.TransformPoint(point) + transform.up * waterline;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        foreach (var point in points)
        {
            Gizmos.DrawWireSphere(GetWorldPoint(point), 0.3f);
        }
    }
}