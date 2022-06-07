using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbo : Bonus
{
    bool is_taking = false;
    [SerializeField]
    public AudioClip sound_item = null;
    new void Awake()
    {
        base.Awake();
        _spawnY = 1f;
        _frequency = 8;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player") 
        {
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory.AddItem(new Item_boost())) 
            {
                other.GetComponent<AudioSource>().PlayOneShot(sound_on_take);
                interface_generator.addEventText("You picked up the booster!").disableAfterSec(1.5f);
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
            inventory.GetComponent<AudioSource>().PlayOneShot(sound_on_take);
            inventory.AddItem(new Item_boost());
            interface_generator.addEventText("You picked up the booster!").disableAfterSec(1.5f);
            Destroy(gameObject);
        }
    }
}
