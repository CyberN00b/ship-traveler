using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : Bonus
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player") {
            Moving fuelconstroller = other.GetComponent<Moving>();
            fuelconstroller.fuel += 30f;
            Destroy(gameObject);
        }
    }
}
