using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineUpgrade : Bonus
{
    void OnTriggerEnter(Collider other)
    {
        GameObject Person = GameObject.Find("Cube");
        Movement speedcontroller = Person.GetComponent<Movement>();
        speedcontroller.speed += 2f;
        Destroy(gameObject);
    }
    
}
