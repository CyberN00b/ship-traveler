using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCenter : MonoBehaviour
{
    Moving ship = null;
    Animation anim;
    bool is_back = false;
    void Awake()
    {
        anim = this.GetComponent<Animation>(); 
        ship = GameObject.Find("Player").GetComponent<Moving>();
    }
    void Update()
    {
        if (!is_back && ship.speed < -1) 
        {
            if (ship.rotation_direction == 1)
                anim.Play("CameraCenterLeftBack");
             else if (ship.rotation_direction == -1) 
                    anim.Play("CameraCenterRightBack");
                 else if (Random.Range(0, 2) == 1)
                        anim.Play("CameraCenterRightBack");
                    else
                        anim.Play("CameraCenterLeftBack");
            is_back = true;
        } else if (is_back && ship.speed > -0.5) 
        {
            if (ship.rotation_direction == 1)
                anim.Play("CameraCenterLeftForward");
             else if (ship.rotation_direction == -1) 
                    anim.Play("CameraCenterRightForward");
                 else if (Random.Range(0, 2) == 1)
                        anim.Play("CameraCenterRightForward");
                    else
                        anim.Play("CameraCenterLeftForward");
            is_back = false;
        }
    }
}
