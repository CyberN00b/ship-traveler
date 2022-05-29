using System.Collections;
using System.Collections.Generic;
using Redcode.Extensions;
using UnityEngine;

public class DynamicObject : MonoBehaviour
{
    protected Controller controller = null;
    protected Rigidbody rg = null;
    protected void Awake() 
    {
        controller = GameObject.Find("GameController").GetComponent<Controller>();
    }
    protected void Start()
    {
        rg = this.GetComponent<Rigidbody>();
        rg.AddForce(
            -controller.speed * Mathf.Sin(Mathf.Deg2Rad * controller.direction),
            0,
            -controller.speed * Mathf.Cos(Mathf.Deg2Rad * controller.direction),
            ForceMode.VelocityChange
        );
    }

    protected void Update()
    {
        ChangePosition();
    }
    void ChangePosition()
    {
        this.transform.SetPositionXZ(
            this.transform.position.x - controller.delta_x,
            this.transform.position.z - controller.delta_z
        );
    }
}
