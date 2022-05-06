using System.Collections;
using System.Collections.Generic;
using Redcode.Extensions;
using UnityEngine;

public class DynamicObject : MonoBehaviour
{
    protected Controller controller = null;
    protected void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<Controller>();
    }

    protected void Update(){
        ChangePosition();
    }
    void ChangePosition(){
        this.transform.SetPositionXZ(
            this.transform.position.x - controller.delta_x - Time.deltaTime * controller.speed * Mathf.Sin(controller.direction),
            this.transform.position.z - controller.delta_z - Time.deltaTime * controller.speed * Mathf.Cos(controller.direction)
        );
    }
}
