using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Speed_indicator : MonoBehaviour
{
    Moving player = null;
    float delta_speed = 0;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Moving>();
        StartCoroutine(NotStatic());
    }
    void Update()
    {
        this.GetComponent<TextMeshProUGUI>().text = "Speed: " + (player.speed * 4 + delta_speed).ToString("0.#") + " km/h";
    }

    IEnumerator NotStatic()
    {
        for(;;) {
            delta_speed = 0.1f * Random.Range(-Mathf.Abs(player.speed), Mathf.Abs(player.speed));
            yield return new WaitForSeconds(0.5f);
        }
    }
}
