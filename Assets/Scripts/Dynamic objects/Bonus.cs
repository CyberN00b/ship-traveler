using System.Collections;
using System.Collections.Generic;
using Redcode.Extensions;
using UnityEngine;

public class Bonus : DynamicObject
{
    protected WorldGenerator generator = null;
    protected InterfaceGenerator interface_generator = null;
    protected float radius = 0;
    protected float radius_of_getting = 4f;
    protected float speed_of_getting = 0.5f;
    protected float _spawnY = 0;
    protected int _frequency = 0;
    public int frequency 
    {
        get {return _frequency;}
    }
    public float spawnY 
    {
        get {return _spawnY;}
    }
    [SerializeField] public bool is_prefab = false;
    new void Start()
    {
        if (!is_prefab) 
        {
            base.Start();
            generator = GameObject.Find("Generator").GetComponent<WorldGenerator>();
            interface_generator = GameObject.Find("Generator").GetComponent<InterfaceGenerator>();
            radius = generator.game_radius;
        }

    }
    new void Update()
    {
        if (!is_prefab) {
            base.Update();
            float x = transform.position.x, z = this.transform.position.z;
            if (radius * radius <= x * x + z * z)
                Destroy(gameObject);
            if (radius_of_getting * radius_of_getting >= x * x + z * z)
            {
                this.transform.SetPositionXZ(
                    x - x * speed_of_getting * Time.deltaTime,
                    z - z * speed_of_getting * Time.deltaTime
                );
            }
        }
    }
}
