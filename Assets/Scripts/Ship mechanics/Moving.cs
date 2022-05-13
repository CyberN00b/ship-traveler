using System.Collections;
using System.Collections.Generic;
using Redcode.Extensions;
using UnityEngine;
public class Moving : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0; // - ship speed
    private float _fuel = 100; // - ship fuel
    private float _max_fuel = 100;
    private float _max_speed = 0;
    private float _fuel_decrease = 0.2f; 
    private float _mass = 5; // - ship mass
    private float _acceleration = 0; // - ship acceleration
    private float _force = 5f; // - ship force
    private float _rotation = 0; // - ship rotation
    private float _cur_rotation = 0; // - ship current rotation
    private float _rotation_direction = 0; // - ship rotation speed
    private float _max_rotation = 30; // - ship max rotation speed
    private float _rotation_N = 0; // - calculation variable
    private float _percent_stop = 0.2f; // - ship passive stoping (_speed * _percent_stop)
    private bool _is_overheat = false;
    private Controller controller = null;
    void Awake()
    {

    }
    void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<Controller>();
        _max_speed = _force / (_percent_stop * _mass);
        _rotation_N = _max_rotation / _max_speed;
        StartCoroutine("SpeedWork");
    }
 
    public float fuel {
        get {return _fuel;}
        set {_fuel = Mathf.Max(Mathf.Min(value, _max_fuel), 0);}
    }
    public float speed {
        get {return _speed;}
        set {_speed = value;}
    }
    public float max_speed {
        get {return _max_speed;}
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
            _rotation_direction = -1;
        } else 
            if (Input.GetKeyDown(KeyCode.D)){
                _rotation_direction = 1; 
            }
        if ((Input.GetKeyUp(KeyCode.A) && _rotation_direction < 0)
            || (Input.GetKeyUp(KeyCode.D) && _rotation_direction > 0))
            _rotation_direction = 0;
        print("Speed with delta: " + _speed + " Fuel: " + _fuel + " Rotation: " + _rotation);
        fuel -= (_fuel_decrease + _speed / 10) * Time.deltaTime;
        controller.ChangePosition(_speed, _rotation);
        this.transform.SetEulerAnglesY(controller.angle);
    }
    IEnumerator SpeedWork() 
    {
        for(;;) 
        {
            if (Mathf.Abs(_rotation + _speed * _rotation_N * _rotation_direction * 0.1f) < _max_rotation)
                _rotation += _speed * _rotation_N * _rotation_direction * 0.1f;
            if ((_rotation < 0 && _rotation_direction > 0) ||
                (_rotation > 0 && _rotation_direction < 0) ||
                _rotation_direction == 0)
                _rotation -= _rotation * 0.1f;
            _speed += _acceleration - _speed * _percent_stop; // - stoping + move
            yield return new WaitForSeconds(0.3f);
        }
    }
}
 