using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooller : Bonus
{
    bool is_taking = false;
    new void Awake()
    {
        base.Awake();
        _spawnY = 1.5f;
        _frequency = 5;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player") 
        {
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory.AddItem(new Item_cooller())) 
            {
                interface_generator.addEventText("You picked up the cooller!").disableAfterSec(1.5f);
                Destroy(gameObject);
            } else {
                is_taking = true;
                StartCoroutine(IsTaking(inventory));
            }
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if (other.name == "Player" && is_taking) 
        {
            is_taking = false;
        }
    }
    IEnumerator IsTaking(Inventory inventory) 
    {
        yield return new WaitUntil(() => !inventory.CanAddItem());
        if (is_taking) 
        {
            inventory.AddItem(new Item_cooller());
            interface_generator.addEventText("You picked up the cooller!").disableAfterSec(1.5f);
            Destroy(gameObject);
        }
    }
}
