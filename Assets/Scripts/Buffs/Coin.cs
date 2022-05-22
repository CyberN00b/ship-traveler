using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Bonus
{
    void Awake() {
        _spawnY = 1.5f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player") {
            Inventory inventory = other.GetComponent<Inventory>();
            inventory.ChangeCash(1);
            Destroy(gameObject);
        }
    }
}
