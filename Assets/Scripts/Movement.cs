using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    void Start()
    {

    }

    [SerializeField]
    private GameObject _gameObject;
 
    [Range(0f,10f)]
    public float _fuel;

    public float speed;

    public float rotation;

    public float distance = 0;

    public int score = 0; 

    [Range(0f, 10f)]
    public float _turbo = 0;

    void Update()
    {

        float x0 = transform.position.x;
        float y0 = transform.position.y;
        if (Input.GetKey(KeyCode.W) && _fuel > 0)
        {
            transform.position += speed * Time.deltaTime * transform.forward;
        }
        if (Input.GetKey(KeyCode.A) && _fuel > 0)
        {
            transform.rotation *= Quaternion.Euler(0f, -rotation * Time.deltaTime, 0f);
        }
        if (Input.GetKey(KeyCode.D) && _fuel > 0)
        {
            transform.rotation *= Quaternion.Euler(0f, rotation * Time.deltaTime, 0f);
        }
        if (Input.GetKey(KeyCode.S) && _fuel > 0)
        {
            transform.position -= speed * Time.deltaTime * transform.forward;
        }
        if(Input.GetKey(KeyCode.F) && _turbo > 0)
        {
            transform.position += 10 * speed * Time.deltaTime * transform.forward;
            _turbo -= 1f * Time.deltaTime;
        }

        distance += Mathf.Sqrt(Mathf.Pow((transform.position.x - x0),2) + Mathf.Pow((transform.position.y - y0),2));

        if (distance > 10)
        {
            distance -= 10;
            score += 1;
            WorldGenerator.Instance.endPoint();
        }
        
        if (_fuel > 0)
        { _fuel -= 1f * Time.deltaTime; }
        else 
        { 
            if(speed > 0)
                speed -= 1f * Time.deltaTime;
        }
        
    }
}
