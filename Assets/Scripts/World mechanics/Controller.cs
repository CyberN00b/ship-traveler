using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField]
    [Range(-360f,360f)]
    private float _direction = 0; // - direction of sea
    [SerializeField]
    [Range(-20f,20f)]
    private Material sea = null;
    [SerializeField]
    private GameObject _completedLevel;
    [SerializeField]
    private GameObject _endText;
    private float _point_x = 0;
    private float _point_z = 0; 
    private float _speed = 1; // - sea speed 
    private float _pos_x = 0; // - ship coord x
    private float _pos_z = 0; // - ship coord z
    private float _angle = 0;
    private float _delta_x = 0;
    private float _delta_z = 0;
    private float _delta_angle = 0;
    public float stop_angle = 0;
    public bool is_collide = false;
    public float point_x {
        get {return _point_x;}
    }
    public float point_z {
        get {return _point_z;}
    }
    public float speed {
        get {return _speed;}
    }
    public float angle {
        get {return _angle;}
    }
    public float delta_x {
        get {return _delta_x;}
        set {_delta_x = value;}
    }
    public float delta_z {
        get {return _delta_z;}
        set {_delta_z = value;}
    }
    public float direction {
        get {return _direction;}
    }
    public float pos_x {
        get {return _pos_x;}
    }
    public float pos_z {
        get {return _pos_z;}
    }
    void Awake() 
    {
        _point_x = Random.Range(0, 50); // need rework
        _point_z = Random.Range(50, 100);
        sea = GameObject.Find("Plane").GetComponent<MeshRenderer>().material;
    }

    public void ChangePositionByShip(float ship_speed, float delta_angle) {
        
        _angle += Time.deltaTime * delta_angle * ship_speed / 10;
        _delta_x = Mathf.Sin(Mathf.Deg2Rad * angle) * ship_speed * Time.deltaTime;
        _delta_z = Mathf.Cos(Mathf.Deg2Rad * angle) * ship_speed * Time.deltaTime;
        if (is_collide) {
            _delta_x -= Mathf.Sin(Mathf.Deg2Rad * stop_angle) * ship_speed * Time.deltaTime;
            _delta_z -= Mathf.Cos(Mathf.Deg2Rad * stop_angle) * ship_speed * Time.deltaTime;
        }
        _pos_z += delta_z;
        _pos_x += delta_x;
        SeaWork();
    }
    void SeaWork(){
        Vector2  tmpVector = 
         new Vector2(
            (_pos_x + _speed * Mathf.Sin(Mathf.Deg2Rad * _direction) * Time.time) / 4,
            (_pos_z + _speed * Mathf.Cos(Mathf.Deg2Rad * _direction) * Time.time) / 4
         );
        sea.SetVector("_Wave_vector", tmpVector);
        tmpVector.x += _speed * Mathf.Sin(Mathf.Deg2Rad * _direction) * Time.time * 0.375f;
        tmpVector.y += _speed * Mathf.Cos(Mathf.Deg2Rad * _direction) * Time.time * 0.375f;
        sea.SetVector("_Normal_vector", tmpVector);
    }
    public void End(){
        //Debug.Log("Level ended!");
        //Application.Quit();
        _endText.SetActive(false);
        _completedLevel.SetActive(true);
    }
}
