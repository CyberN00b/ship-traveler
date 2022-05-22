using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coordinates_indicator : MonoBehaviour
{
    Controller controller = null;
    void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<Controller>();
    }
    void Update()
    {
        this.GetComponent<TextMeshProUGUI>().text = "Your coordinates - x: " + controller.pos_z.ToString("0.") + " z: " + controller.pos_x.ToString("0.");
    }
}
