using System.Collections;
using System.Collections.Generic;
using Redcode.Extensions;
using UnityEngine;

public class FuelBuy : MonoBehaviour
{
    [SerializeField]
    private float destroy_y = -1;
    [SerializeField]
    public bool is_prefab = false;
    private bool spawned = false;
    protected Controller controller = null;
    protected InterfaceGenerator interface_generator = null;
    private Fuel fuel = null;
    void Start()
    {
        fuel = GameObject.Find("Fuel").GetComponent<Fuel>();
        controller = GameObject.Find("GameController").GetComponent<Controller>();
        interface_generator = GameObject.Find("Generator").GetComponent<InterfaceGenerator>();
    }
    void Update()
    {
        if (is_prefab)
            return;
        ChangePosition();
        if (!spawned) 
        {
            if (this.transform.position.y <= destroy_y) 
            {
                fuel = Instantiate(fuel, this.transform.position, Quaternion.identity, GameObject.Find("Generator").transform);
                fuel.is_prefab = false;
                spawned = true;
            }
        } else {
            if (fuel != null && fuel.spawnY > fuel.transform.position.y)
                fuel.transform.SetLocalPositionY(fuel.transform.position.y + 1f * Time.deltaTime);
            else {
                Destroy(this);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player") {
            Moving fuelcontroller = other.GetComponent<Moving>();
            fuelcontroller.fuel += fuel.count_of_gas;
            interface_generator.addEventText("+" + fuel.count_of_gas + " fuel").disableAfterSec(1.5f);
            Destroy(gameObject);
        }
    }
    void ChangePosition(){
        this.transform.SetPositionXZ(
            this.transform.position.x - controller.delta_x,
            this.transform.position.z - controller.delta_z
        );
    }
}
