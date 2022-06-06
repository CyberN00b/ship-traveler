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
    private MenuController _menuController = null;
    private float _point_x = 0;
    private float _point_z = 0; 
    private float _speed = 1;
    private float _pos_x = 0;
    private float _pos_z = 0;
    private float _angle = 0;
    private float _delta_x = 0;
    private float _delta_z = 0;
    public float delta_x = 0;
    public float delta_z = 0;
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
        _point_x = Random.Range(-150, 150);
        _point_z = Random.Range(800,1000);
        _direction = Random.Range(-60, 60);
        _speed = Random.Range(0.5f, 2f);
        sea = GameObject.Find("Plane").GetComponent<MeshRenderer>().material;
        _menuController = this.GetComponent<MenuController>();
    }
    void Update() 
    {
        delta_x = _delta_x;
        delta_z = _delta_z;
        _pos_z += delta_z;
        _pos_x += delta_x;
        SeaWork();
        _delta_x = 0;
        _delta_z = 0;
    }
    public void ChangePosition(float change_speed, float change_angle = 0, bool is_delta = true) 
    {
        if (is_delta)
        {
            _angle += Time.deltaTime * change_angle * change_speed / 10;
            if (Mathf.Abs(_angle) > 180f)
            {
                if (_angle < 0)
                    _angle += 360;
                else
                    _angle -= 360;
            }
            change_angle = _angle;
        }
        _delta_x += Mathf.Sin(Mathf.Deg2Rad * change_angle) * change_speed * Time.deltaTime;
        _delta_z += Mathf.Cos(Mathf.Deg2Rad * change_angle) * change_speed * Time.deltaTime;
    }
    void SeaWork()
    {
        Vector2  tmpVector = 
         new Vector2(
            (_pos_x + _speed * Mathf.Sin(Mathf.Deg2Rad * _direction) * Time.time) / 4,
            (_pos_z + _speed * Mathf.Cos(Mathf.Deg2Rad * _direction) * Time.time) / 4
         );
        sea.SetVector("_Wave_vector", tmpVector);
        tmpVector *= 1.45f;
        sea.SetVector("_Normal_vector", tmpVector);
    }
    public void End()
    {
        _menuController.EnableMenu(_completedLevel, 2);
    }
}
