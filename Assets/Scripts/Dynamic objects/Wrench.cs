using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : Bonus
{
    bool is_taking = false;
    [SerializeField]
    public AudioClip sound_item = null;
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
            if (inventory.AddItem(new Item_wrench())) 
            {
                other.GetComponent<AudioSource>().PlayOneShot(sound_on_take);
                interface_generator.addEventText("You picked up the wrench!").disableAfterSec(1.5f);
                Destroy(gameObject);
            } else {
                is_taking = true;
                StartCoroutine(IsTaking(inventory));
            }
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if (other.name == "Player") 
        {
            is_taking = false;
        }
    }
    IEnumerator IsTaking(Inventory inventory) 
    {
        yield return new WaitUntil(() => !inventory.CanAddItem());
        if (is_taking)
        {
            inventory.AddItem(new Item_wrench());
            inventory.GetComponent<AudioSource>().PlayOneShot(sound_on_take);
            interface_generator.addEventText("You picked up the wrench!").disableAfterSec(1.5f);
            Destroy(gameObject);
        }
    }
}
