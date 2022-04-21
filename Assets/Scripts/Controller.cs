using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private int distance = 100; // - distance of lvl (tmp const)
    public float pos_x = 0; // - ship coord x
    public float pos_z = 0; // - ship coord z
    int t = 0;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ChangePosition(float speed, float angle) {
        pos_x += Mathf.Cos(angle) * speed;
        pos_z -= Mathf.Sin(angle) * speed;
        if (t % 60 == 0) {
            Debug.Log("Current pos - x:" + pos_x + " z:" + pos_z);
            Debug.Log("Current rot - " + angle);
        }
        t++;
        
        if (pos_x >= distance) {
            End();
        }
    }
    void End(){
        Debug.Log("Level ended!");
        Application.Quit();
    }
}
