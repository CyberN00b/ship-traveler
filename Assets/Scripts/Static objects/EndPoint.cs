using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : Port
{
    [SerializeField]
    private GameObject _textEnd;
    private InterfaceGenerator generator = null;
    void Start() {
        _distance = 20;
        _collide_zone = 10;
        base.Start();
        this.GetComponent<SphereCollider>().radius = _collide_zone;
        generator = GameObject.Find("Generator").GetComponent<InterfaceGenerator>();
        StartCoroutine(EndLevel());

    }
    void Update()
    {
        base.Update();
        if (_is_activated) {
            if (Input.GetKey(KeyCode.F))
                controller.End();
        }
    }

    private IEnumerator EndLevel()
    {
        while(!_is_activated)
        {
            yield return new WaitForSeconds(1f);
        }
        EventText text = generator.addEventText("Press F to end mission", "end_level");
        while (_is_activated) 
        {
            yield return new WaitForSeconds(1f);
        }
        text.hideAndDisable();
        StartCoroutine(EndLevel());
    }
}
