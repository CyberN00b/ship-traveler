using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private GameObject _player;
    private float _speedEff = 1.5f;
    private void Start()
    {
        _player = GameObject.Find("Player");
    }

    void Update()
    {
        Moving _speedEffect = _player.GetComponent<Moving>();
        float _currentSpeed = _speedEffect.speed;
        if (transform.localPosition.z <= -8 && transform.localPosition.z >= -11)
        {
            if (_currentSpeed > 3.8f)
            {
                transform.localPosition -= new Vector3(0, -_speedEff, _speedEff) * Time.deltaTime;
                transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Clamp(transform.localPosition.y, 20, 23), 
                    Mathf.Clamp(transform.localPosition.z, -11 , -8));
            }
            else
            {
                transform.localPosition += new Vector3(0, -_speedEff, _speedEff) * Time.deltaTime;
                transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Clamp(transform.localPosition.y, 20, 23),
                    Mathf.Clamp(transform.localPosition.z, -11, -8));
            }
        }
    }
}
