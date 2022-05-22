using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Bonus
{
    protected bool _isTriggered = false;
    void Awake() {
        _spawnY = 1.5f;
        StartCoroutine(Anim());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player") {
            _isTriggered = true;
            Inventory inventory = other.GetComponent<Inventory>();
            inventory.ChangeCash(1);
        }
    }

    private IEnumerator Anim()
    {
        while (!_isTriggered)
        {
            yield return new WaitForSeconds(0.5f);
        }
        gameObject.transform.GetChild(0).gameObject.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
