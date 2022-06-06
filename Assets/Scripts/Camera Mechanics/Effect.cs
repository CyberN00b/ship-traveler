using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private GameObject _player;
    private float _speedEff = 3f;
    [SerializeField]
    private float high_speed = 2.6f;
    [SerializeField]
    private float min_distance = 8;
    [SerializeField]
    private float max_distance = 15;
    private void Start()
    {
        _player = GameObject.Find("Player");
    }

    void Update()
    {
        Moving _speedEffect = _player.GetComponent<Moving>();
        float _currentSpeed = _speedEffect.speed;
        float min_z = -min_distance * Mathf.Cos(transform.localEulerAngles.x * Mathf.Deg2Rad) - 4,
              max_z = -max_distance * Mathf.Cos(transform.localEulerAngles.x * Mathf.Deg2Rad) - 4,
              min_y = min_distance * Mathf.Sin(transform.localEulerAngles.x * Mathf.Deg2Rad) + 10,
              max_y = max_distance * Mathf.Cos(transform.localEulerAngles.x * Mathf.Deg2Rad) + 10;
        if (transform.localPosition.z <= min_z && transform.localPosition.z >= max_z)
        {
            if (_currentSpeed > high_speed)
            {
                transform.localPosition -= new Vector3(0, -_speedEff, _speedEff) * Time.deltaTime;
                transform.localPosition = new Vector3(
                    transform.localPosition.x, 
                    Mathf.Clamp(transform.localPosition.y, min_y, max_y), 
                    Mathf.Clamp(transform.localPosition.z, max_z , min_z)
                );
            }
            else
            {
                transform.localPosition += new Vector3(0, -_speedEff, _speedEff) * Time.deltaTime;
                transform.localPosition = new Vector3(
                    transform.localPosition.x, 
                    Mathf.Clamp(transform.localPosition.y, min_y, max_y),
                    Mathf.Clamp(transform.localPosition.z, max_z, min_z)
                );
            }
        }
    }
}
