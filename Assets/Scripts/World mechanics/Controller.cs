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
    private float _speed = 2; // - sea speed 
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
        _direction = ToRadian(_direction);
    }

    void Update()
    {
        
    }

    public void ChangePosition(float speed, float delta_angle) {
        _angle += delta_angle;
        _delta_x = Mathf.Sin(ToRadian(angle)) * speed;
        _delta_z = Mathf.Cos(ToRadian(angle)) * speed;
        _pos_z += delta_z;
        _pos_x += delta_x;
    }
    void End(){
        Debug.Log("Level ended!");
        Application.Quit();
    }
    public float ToRadian(float degrees) {
        return degrees * Mathf.PI / 180;
    }
}
