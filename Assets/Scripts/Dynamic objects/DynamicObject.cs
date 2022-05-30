using System.Collections;
using System.Collections.Generic;
using Redcode.Extensions;
using UnityEngine;

public class DynamicObject : MonoBehaviour
{
    protected Controller controller = null;
    protected WorldGenerator generator = null;
    protected float radius = 0;
    protected float _spawnY = 0;
    protected int _frequency = 0;
    [SerializeField] public bool is_prefab = false;
    public int frequency 
    {
        get {return _frequency;}
    }
    public float spawnY 
    {
        get {return _spawnY;}
    }
    protected void Awake() 
    {
        controller = GameObject.Find("GameController").GetComponent<Controller>();
        generator = GameObject.Find("Generator").GetComponent<WorldGenerator>();
        radius = generator.game_radius;
    }

    protected void Update()
    {
        if (!is_prefab) 
        {
            float x = transform.position.x, z = this.transform.position.z;
            if (radius * radius <= x * x + z * z)
                Destroy(gameObject);
            ChangePosition();
        }
    }
    void ChangePosition()
    {
        this.transform.SetPositionXZ(
            this.transform.position.x - controller.delta_x - 
                controller.speed * Mathf.Sin(Mathf.Deg2Rad * controller.direction) * Time.deltaTime,
            this.transform.position.z - controller.delta_z - 
                controller.speed * Mathf.Cos(Mathf.Deg2Rad * controller.direction) * Time.deltaTime
        );
    }
}
