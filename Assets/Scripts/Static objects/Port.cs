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
            controller.stop_angle = Mathf.Atan2(x, z);
        }
    }
    IEnumerator PortCheck()
    {
        if (!is_prefab)
            for (;;) 
            {
                float x = this.transform.position.x, z = this.transform.position.z;
                if (x * x + z * z <= _distance * _distance)
                    _is_activated = true;
                else
                    _is_activated = false;
                if (x * x  + z * z < _collide_zone * _collide_zone) 
                {
                    _is_collide = true;
                    controller.is_collide = true;
                } else 
                    if (_is_collide)
                    {
                        _is_collide = false;
                        controller.is_collide = false;
                    }
                yield return new WaitForSeconds(0.1f);
            }
    }
}
