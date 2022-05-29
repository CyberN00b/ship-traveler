using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruction_script : MonoBehaviour
{
    private GameObject instruction = null;
    void Start()
    {
        instruction = GameObject.Find("Instruction");
        GameObject.Find("Generator").GetComponent<InterfaceGenerator>().addEventText("Press P to see control instruction").disableAfterSec(4f);
        instruction.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            instruction.SetActive(!instruction.activeSelf);
        }
    }
}
