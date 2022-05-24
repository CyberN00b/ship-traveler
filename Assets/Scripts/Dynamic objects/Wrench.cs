using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : Bonus
{
    private void Awake()
    {
        _spawnY = 1.5f;
    }
    private int _heal = 50;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Moving healthcontroller = other.GetComponent<Moving>();
            healthcontroller.health += _heal;
            print(healthcontroller.health);
            Destroy(gameObject);
        }
    }
}
