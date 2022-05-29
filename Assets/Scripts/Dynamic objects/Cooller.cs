using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooller : Bonus
{
    new void Awake()
    {
        base.Awake();
        _spawnY = 1.5f;
        _frequency = 5;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player") {
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory.AddItem(new Item_cooller())) {
                interface_generator.addEventText("You picked up the cooller!").disableAfterSec(1.5f);
                Destroy(gameObject);
            }
        }
    }
}
