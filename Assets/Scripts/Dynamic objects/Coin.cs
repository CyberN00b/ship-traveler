using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Bonus
{
    void Awake() {
        _spawnY = 1.5f;
        _frequency = 8;
    }
    private int value = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player") {
            Inventory inventory = other.GetComponent<Inventory>();
            inventory.ChangeCash(value);
            interface_generator.addEventText("+" + value + " coin").disableAfterSec(1.5f);
            StartCoroutine(Anim());
        }
    }

    private IEnumerator Anim()
    {
        var audio = gameObject.GetComponent<AudioSource>();
        audio.Play();   
        gameObject.transform.GetChild(0).gameObject.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
