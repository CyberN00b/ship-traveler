using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : Port
{
    void Start() {
        _distance = 20;
        _collide_zone = 10;
        base.Start();
        this.GetComponent<SphereCollider>().radius = _collide_zone;
    }
    void Update()
    {
        base.Update();
        if (_is_activated) {
            print("Press F to End!");
            if (Input.GetKey(KeyCode.F))
                controller.End();
        }
        print(_is_activated);
    }
}
