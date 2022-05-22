using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Bonus
{
    void Awake() {
        _spawnY = 1.5f;
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
        gameObject.transform.GetChild(0).gameObject.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
