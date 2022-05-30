using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bur : DynamicObject
{
    private float damage = 5;
    InterfaceGenerator interface_generator = null;
    new void Awake()
    {
        base.Awake();
        _spawnY = 1f;
        _frequency = 10;
        interface_generator = GameObject.Find("Generator").GetComponent<InterfaceGenerator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player") 
        {
            Moving ship = other.GetComponent<Moving>();
            int damage_take = (int)(damage * Mathf.Abs(ship.speed) / 2f);
            interface_generator.addEventText("You got " + damage_take + " damage on your ship by thurn!").disableAfterSec(2f);
            ship.decreaseHealth(damage_take);
        }
    }
}
