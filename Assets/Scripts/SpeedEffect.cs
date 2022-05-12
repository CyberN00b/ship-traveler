using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEffect : MonoBehaviour
{
    private Vector3 _startPos;
    private GameObject _player;
    private float _speedEff = 50;
    private float _speed = 0;
    private float _pos = 5;
    private float _deltaPos = 0;
    void Start()
    {
        _player = GameObject.Find("Player");
    }

    void Update()
    {
        Moving _speedEffect = _player.GetComponent<Moving>();
        float _currentSpeed = _speedEffect.speed;
        float _nextPos = _pos;
        if (_pos <= 5 && _pos >= 0)
        {
            if (_currentSpeed > _speed)
            {
                _speed = _currentSpeed;
                _pos -= _speedEff * Time.deltaTime;
                _pos = Mathf.Clamp(_pos, 0, 5);
                _deltaPos = _pos - _nextPos;
                transform.Translate(0, 0, _deltaPos);

            }
            else if (_currentSpeed < _speed)
            {
                _speed = _currentSpeed;
                _pos += _speedEff * Time.deltaTime;
                _pos = Mathf.Clamp(_pos, 0, 5);
                _deltaPos = _pos - _nextPos;
                transform.Translate(0, 0, _deltaPos);
            }
        }
        print(_pos);
    }
}
