using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : Port
{
    [SerializeField]
    private GameObject _textEnd;
    void Start() {
        _distance = 20;
        _collide_zone = 10;
        base.Start();
        this.GetComponent<SphereCollider>().radius = _collide_zone;
        StartCoroutine(EndLevel());

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

    private IEnumerator EndLevel()
    {
        Animation _anim = _textEnd.GetComponent<Animation>();
        while(!_is_activated)
        {
            yield return new WaitForSeconds(1f);
        }
        _textEnd.SetActive(true);
        _anim.Play();
        while (_is_activated) 
        {
            yield return new WaitForSeconds(1f);
        }
        //обратная анимация
        _textEnd.SetActive(false);
        StartCoroutine(EndLevel());
    }
}
