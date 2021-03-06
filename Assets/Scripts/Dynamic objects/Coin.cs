using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Bonus
{
    private int value = 1;
    new void Awake() 
    {
        base.Awake();
        _spawnY = 1.5f;
        _frequency = 8;
        value = Random.Range(2, 6);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player") 
        {
            Inventory inventory = other.GetComponent<Inventory>();
            inventory.ChangeCash(value);
            interface_generator.addEventText("+" + value + " coin").disableAfterSec(1.5f);
            other.GetComponent<AudioSource>().PlayOneShot(sound_on_take);
            StartCoroutine(Anim());
        }
    }

    private IEnumerator Anim()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
