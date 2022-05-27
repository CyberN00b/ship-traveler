using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : Port
{
    [SerializeField]
    private GameObject _textEnd;
    private InterfaceGenerator generator = null;
    new void Start() {
        _distance = 20;
        _collide_zone = 10;
        base.Start();
        this.GetComponent<SphereCollider>().radius = _collide_zone;
        generator = GameObject.Find("Generator").GetComponent<InterfaceGenerator>();
        StartCoroutine(EndLevel());

    }
    new void Update()
    {
        base.Update();
        if (_is_activated) {
            if (Input.GetKey(KeyCode.F))
                controller.End();
        }
    }

    private IEnumerator EndLevel()
    {
        yield return new WaitUntil(() => _is_activated == true);
        EventText text = generator.addEventText("Press F to end mission", "end_level");
        yield return new WaitUntil(() => _is_activated == false);
        text.hideAndDisable();
        StartCoroutine(EndLevel());
    }
}
