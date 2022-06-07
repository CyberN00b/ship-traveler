using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruction_script : MonoBehaviour
{
    private GameObject instruction = null;
    private AudioSource source = null;
    void Start()
    {
        instruction = GameObject.Find("Instruction");
        GameObject.Find("Generator").GetComponent<InterfaceGenerator>().addEventText("Press P to see control instruction").disableAfterSec(4f);
        instruction.SetActive(false);
        source = this.GetComponent<AudioSource>();
        source.Play();
        source.Pause();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            instruction.SetActive(!instruction.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (source.isPlaying) 
            {
                source.Pause();
            } else {
                source.UnPause();
            }
        }
    }
}
