using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OilBase_indicator : MonoBehaviour
{
    Controller controller = null;
    WorldGenerator generator = null;
    void Start()
    {
        generator = GameObject.Find("Generator").GetComponent<WorldGenerator>();
        controller = GameObject.Find("GameController").GetComponent<Controller>();
        StartCoroutine(CheckDistance());
    }
    IEnumerator CheckDistance()
    {
        for (;;)
        {
            bool flag = false;
            float x = 0, z = 0, distance = 100000;
            foreach(Struction struction in generator.structions)
            {
                if (struction.port.GetType().Name != "OilBase")
                    continue;
                float delta_distance = (
                    (struction.x - controller.pos_x) * (struction.x - controller.pos_x) + 
                    (struction.z - controller.pos_z) * (struction.z - controller.pos_z)
                );
                if (delta_distance < distance) 
                {
                    x = struction.x;
                    z = struction.z;
                    distance = delta_distance;
                    flag = true;
                } 
            }
            if (flag)
                this.GetComponent<TextMeshProUGUI>().text = (
                    "Nearest oil base - x: " + z.ToString("0.") + 
                    " z: " + x.ToString("0.")
                );
            else
                this.GetComponent<TextMeshProUGUI>().text = "Nearest oil base is not found";
            yield return new WaitForSeconds(3f);
        }
    }
}
