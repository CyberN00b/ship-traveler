using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class End_indicator : MonoBehaviour
{
    Controller controller = null;
    void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<Controller>();
        this.GetComponent<TextMeshProUGUI>().text = "End on coordinates - x: " + controller.point_z.ToString("0.") + " z: " + controller.point_x.ToString("0.");
    }
}
