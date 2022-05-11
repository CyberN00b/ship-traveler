using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private int distance = 1000; // - distance of lvl (tmp const)
    [SerializeField]
    [Range(-360f,360f)]
    private float _direction = 0; // - direction of sea
    [SerializeField]
    [Range(-20f,20f)]
    private Material sea = null;
    private float _speed = 1; // - sea speed 
    private float _pos_x = 0; // - ship coord x
    private float _pos_z = 0; // - ship coord z
    private float _angle = 0;
    private float _delta_x = 0;
    private float _delta_z = 0;
    private float _delta_angle = 0;
    public float speed {
        get {return _speed;}
    }
    public float angle {
        get {return _angle;}
    }
    public float delta_x {
        get {return _delta_x;}
    }
    public float delta_z {
        get {return _delta_z;}
    }
    public float direction {
        get {return _direction;}
    }
    void Start()
    {
        _direction = Mathf.Deg2Rad * _direction;
        sea = GameObject.Find("Plane").GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
    }

    public void ChangePosition(float ship_speed, float delta_angle) {
        _angle += delta_angle * ship_speed / 10;
        _delta_x = Mathf.Sin(Mathf.Deg2Rad * angle) * ship_speed * Time.deltaTime;
        _delta_z = Mathf.Cos(Mathf.Deg2Rad * angle) * ship_speed * Time.deltaTime;
        _pos_z += delta_z;
        _pos_x += delta_x;
        SeaWork();
    }
    void SeaWork(){
        Vector2  tmpVector = 
         new Vector2(
            (_pos_x + _speed * Mathf.Sin(_direction) * Time.time) / 4,
            (_pos_z + _speed * Mathf.Cos(_direction) * Time.time) / 4
         );
        sea.SetVector("_Wave_vector", tmpVector);
        tmpVector.x += _speed * Mathf.Sin(_direction) * Time.time * 0.375f;
        tmpVector.y += _speed * Mathf.Cos(_direction) * Time.time * 0.375f;
        sea.SetVector("_Normal_vector", tmpVector);
    }
    void End(){
        Debug.Log("Level ended!");
        Application.Quit();
    }
}
