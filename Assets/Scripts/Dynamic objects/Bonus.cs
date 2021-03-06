using System.Collections;
using System.Collections.Generic;
using Redcode.Extensions;
using UnityEngine;

public class Bonus : DynamicObject
{
    protected InterfaceGenerator interface_generator = null;
    protected float radius_of_getting = 4f;
    protected float speed_of_getting = 0.5f;
    [SerializeField]
    protected AudioClip sound_on_take = null;
    
    protected new void Awake()
    {
        base.Awake();
        interface_generator = GameObject.Find("Generator").GetComponent<InterfaceGenerator>();
    }
    protected new void Update()
    {
        if (!is_prefab) 
        {
            base.Update();
            float x = transform.position.x, z = this.transform.position.z;
            if (radius_of_getting * radius_of_getting >= x * x + z * z)
            {
                this.transform.SetPositionXZ(
                    x - (((x < 0)? -controller.speed : controller.speed) + x) * speed_of_getting * Time.deltaTime,
                    z - (((z < 0)? -controller.speed : controller.speed) + z) * speed_of_getting * Time.deltaTime
                );
            }
        }
    }
}
