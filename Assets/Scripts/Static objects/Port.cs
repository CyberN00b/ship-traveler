using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : StaticObject
{
    protected float _distance = 0;
    protected float _spawn_distance = 100;
    protected float _collide_zone = 0;
    protected bool _is_activated = false;
    protected bool _is_collide = false;
    public float spawn_distance {
        get {return _spawn_distance;}
    }
    protected new void Start() 
    {
        base.Start();
        StartCoroutine("PortCheck");
        this.GetComponent<SphereCollider>().radius = _collide_zone;
    }
    protected new void Update() 
    {
        base.Update();
        if (_is_collide) 
        {
            float x = this.transform.position.x, z = this.transform.position.z;
            float stop_angle = Mathf.Atan2(-x, -z) * Mathf.Rad2Deg;
            controller.ChangePosition((_collide_zone - Mathf.Sqrt(x * x + z * z)) * 2, stop_angle, false);
        }
    }
    IEnumerator PortCheck()
    {
        if (!is_prefab)
            for (;;) 
            {
                float x = this.transform.position.x, z = this.transform.position.z;
                _is_activated = x * x + z * z <= _distance * _distance;
                _is_collide = x * x  + z * z <= _collide_zone * _collide_zone;
                yield return new WaitForSeconds(0.1f);
            }
    }
}
