using System.Collections;
using System.Collections.Generic;
using Redcode.Extensions;
using UnityEngine;
public class Moving : MonoBehaviour
{
    private float speed = 0; // - ship speed
    private float speed_inc = 0.02f; // - ship increase speed (tmp const)
    private float speed_dec = 0.01f; // - ship decrease speed (tmp const)
    private float max_speed = 0.1f; // - ship max speed (tmp const)
    private float min_speed = -0.04f; // - ship min speed (tmp const)
    private float rotation = 0; // - ship rotation
    private float cur_rotation = 0; // - ship current rotation
    private float rotation_speed = 5; // - ship rotation speed (tmp const)
    private bool is_overheat = false;
    private Controller controller = null;
    void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<Controller>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) {
            if (speed <= max_speed) {
                speed += speed_inc;
                is_overheat = false;
            } else {
                speed -= (speed - max_speed + 0.5f) * 0.05f; // - change this const values
                is_overheat = true;
            }
        } else 
            if (Input.GetKeyDown(KeyCode.S)) {
                if (speed >= min_speed) {
                    speed -= speed_dec;
                    is_overheat = false;
                } else {
                    speed += (min_speed - speed + 0.3f) * 0.05f; // - change this const values
                    is_overheat = true;
                }
            }
        if (Input.GetKeyDown(KeyCode.A)){
            rotation = -rotation_speed;
        } else 
            if (Input.GetKeyDown(KeyCode.D)){
                rotation = rotation_speed; 
            }
        if ((Input.GetKeyUp(KeyCode.A) && rotation == -rotation_speed) 
            || (Input.GetKeyUp(KeyCode.D) && rotation == rotation_speed))
            rotation = 0;
        cur_rotation += rotation * speed;
        this.transform.SetEulerAnglesY(cur_rotation);
        controller.ChangePosition(speed, cur_rotation * Mathf.PI / 180);
        //this.transform.SetPositionXZ(controller.pos_x, controller.pos_z);
    }
}
