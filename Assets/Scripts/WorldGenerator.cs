using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public static WorldGenerator Instance { get; private set; }

    [SerializeField] GameObject _plane; 

    [SerializeField] GameObject _endPoint;

    [SerializeField] private GameObject _person;

    [SerializeField] private int _radius;

    [SerializeField] private Bonus[] _bonusprefabs;

    private GameObject _endPointObject = null;

    private Bonus RandomBonus() => _bonusprefabs[UnityEngine.Random.Range(0, _bonusprefabs.Length)];

    private void Start()
    {
        Instance = this;
        var pos = _person.transform.position;
        for(int i = (int)pos.x - _radius/5; i < (int)pos.x + _radius/5; i++)
        {
            for(int j = (int)pos.z - _radius/5; j < (int)pos.z + _radius/5; j++)
            {
                Instantiate(_plane, new Vector3(10 * i + 5f, 0.25f, 10 * j + 5f), Quaternion.identity, transform);
                if(Random.Range(0, 4) == 0)
                {
                    Instantiate(RandomBonus(), new Vector3(10 * i + 5f + Random.Range(-5f, 5f), 1f, 10 * j + 5f + Random.Range(-5f, 5f)), Quaternion.identity, transform);
                }
            }
        }
    }
    public void endPoint()
    {
        if (_endPointObject != null) { return; }
        Movement end = _person.GetComponent<Movement>();
        bool flag = true;
        if(end.score > 10 && flag)
        {
            flag = false;
            var c = Random.Range(-10f, 10f);
            _endPointObject = Instantiate(_endPoint, new Vector3(_person.transform.position.x + c, 0.5f,
                _person.transform.position.z + Mathf.Sqrt(Mathf.Pow(26,2) - Mathf.Pow(c,2))), Quaternion.identity, transform);
        }
    }

}
