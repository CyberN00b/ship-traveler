using System.Collections;
using System.Collections.Generic;
using Redcode.Extensions;
using UnityEngine;
public class Moving : MonoBehaviour
{
    [SerializeField]
 
    [Range(-5f,20f)]
    private float _speed = 0; // - ship speed
    private float _fuel = 100;
    private float _max_fuel = 100;
    private float _fuel_decrease = 2; 
    private float _mass = 5; // - ship mass
    private float _acceleration = 0; // - ship acceleration
    private float _force = 0.2f; // - ship force
    private float _rotation = 0; // - ship rotation
    private float _cur_rotation = 0; // - ship current rotation
    private float _rotation_speed = 30; // - ship rotation speed (tmp const)
    private float _percent_stop = 0.2f; // - ship passive stoping (_speed * _percent_stop)
    private bool _is_overheat = false;
    private Controller controller = null;
    void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<Controller>();
    }
 
    public float fuel {
        get {return _fuel;}
        set {_fuel = Mathf.Max(Mathf.Min(value, _max_fuel), 0);}
    }
    public float speed {
        get {return _speed;}
        set {_speed = value;}
    }
    void Update()
    {   
        if (_fuel > 0) {
            if (Input.GetKeyDown(KeyCode.W))
                _acceleration = _force / _mass;
            else 
                if (Input.GetKeyDown(KeyCode.S))
                    _acceleration = -_force / (_mass * 2);
            if ((Input.GetKeyUp(KeyCode.S) && _acceleration < 0) 
                || (Input.GetKeyUp(KeyCode.W) && _acceleration > 0))
                _acceleration = 0;
        } else {
            _acceleration = 0;
        }
        if (Input.GetKeyDown(KeyCode.A)){
            _rotation = -Time.deltaTime * _rotation_speed;
        } else 
            if (Input.GetKeyDown(KeyCode.D)){
                _rotation = Time.deltaTime * _rotation_speed; 
            }
        if ((Input.GetKeyUp(KeyCode.A) && _rotation < 0) 
            || (Input.GetKeyUp(KeyCode.D) && _rotation > 0))
            _rotation = 0;
        
        if (!_acceleration.Equals(0)) 
            fuel -= _fuel_decrease * Time.deltaTime * speed * 20;
        print("speed: " + _speed + " fuel: " + _fuel);
        _speed += (_acceleration - _speed * _percent_stop) * Time.deltaTime; // - stoping + move
        _cur_rotation += _rotation * _speed * 5;
        this.transform.SetEulerAnglesY(_cur_rotation);
        controller.ChangePosition(_speed, _cur_rotation * Mathf.PI / 180);
        this.transform.SetPositionXZ(controller.pos_x, controller.pos_z);
    }
}
 