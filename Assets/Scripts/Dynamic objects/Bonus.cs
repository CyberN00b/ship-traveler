using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : DynamicObject
{

    [SerializeField] private BonusPrefabs _bonusPrefabs;
    protected WorldGenerator generator = null;
    protected InterfaceGenerator interface_generator = null;
    protected float radius = 0;
    protected float _spawnY = 0;

    public float spawnY {
        get {return _spawnY;}
    }
    [SerializeField] public bool is_prefab = false;
    void Start(){
        if (!is_prefab) {
            base.Start();
            generator = GameObject.Find("Generator").GetComponent<WorldGenerator>();
            interface_generator = GameObject.Find("Generator").GetComponent<InterfaceGenerator>();
            radius = generator.game_radius;
        }

    }
    void Update(){
        if (!is_prefab) {
            base.Update();
            float x = transform.position.x, z = this.transform.position.z;
            if (radius * radius <= x * x + z * z) {
                Destroy(gameObject);
            }
        }
    }
}
