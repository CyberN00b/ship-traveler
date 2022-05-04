using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbo : Bonus
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject Person = GameObject.Find("Cube");
        Movement turbocontroller = Person.GetComponent<Movement>();
        turbocontroller._turbo += 1f;
        Destroy(gameObject);
    }
}
