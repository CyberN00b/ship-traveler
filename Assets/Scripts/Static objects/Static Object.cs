using System.Collections;
using System.Collections.Generic;
using Redcode.Extensions;
using UnityEngine;

public class StaticObject : MonoBehaviour
{
    protected Controller controller = null;
    [SerializeField] public bool is_prefab = false;
    protected void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<Controller>();
    }

    protected void Update()
    {
        if (!is_prefab)
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
