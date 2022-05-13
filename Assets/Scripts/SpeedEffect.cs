using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEffect : MonoBehaviour
{
    private GameObject _player;
    private float _speedEff = 1.5f;
    private void Start()
    {
        _player = GameObject.Find("Player");
    }

    void Update()
    {
        Vector3 _localpos = transform.InverseTransformPoint(transform.position);
        Moving _speedEffect = _player.GetComponent<Moving>();
        float _currentSpeed = _speedEffect.speed;
        if (_localpos.z <= -8 && _localpos.z >= -11)
        {
            if (_currentSpeed > 3.8f)
            {
                _localpos -= Vector3.forward * _speedEff * Time.deltaTime;
                transform.localPosition = transform.TransformDirection(new Vector3(_localpos.x, _localpos.y,
                    Mathf.Clamp(_localpos.z, -11, -8)));
            }
            else
            {
                _localpos += Vector3.forward * _speedEff * Time.deltaTime;
                transform.localPosition = transform.TransformDirection(new Vector3(_localpos.x, _localpos.y,
                    Mathf.Clamp(_localpos.z, -11, -8)));
            }
        }
    }
}
