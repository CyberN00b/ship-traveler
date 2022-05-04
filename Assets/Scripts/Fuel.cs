using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : Bonus
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject Person = GameObject.Find("Cube");
        Movement fuelconstroller = Person.GetComponent<Movement>();
        fuelconstroller._fuel = 10f;
        Destroy(gameObject);
    }
}
