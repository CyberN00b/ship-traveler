using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilBase : Port
{
    private static int _barrel_cost = 5;
    private InterfaceGenerator interface_generator = null;
    private WorldGenerator generator = null;
    private Inventory inventory = null;
    private MenuController menu_controller = null;
    [SerializeField]
    private FuelBuy barrel = null;
    private bool waiting = false;
    private float time_of_wait = 0.5f;
    private Vector3 spawn;
    private AudioSource _sale;
    public static int barrel_cost {
        get {return _barrel_cost;}
    }
    new void Start()
    {
        _distance = 10;
        _collide_zone = 7;
        base.Start();
        this.GetComponent<SphereCollider>().radius = _collide_zone;
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        interface_generator = GameObject.Find("Generator").GetComponent<InterfaceGenerator>();
        generator = GameObject.Find("Generator").GetComponent<WorldGenerator>();
        menu_controller = GameObject.Find("GameController").GetComponent<MenuController>();
        StartCoroutine(CheckBase());
    }
    new void Update()
    {   
        base.Update();
        if (_is_activated) {
            if (Input.GetKey(KeyCode.F) && !waiting && 
                inventory.cash >= _barrel_cost && !menu_controller.IsEnabled()
            )
                BuyOil();
        }
    }
    private void BuyOil() 
    {
        _sale = GetComponent<AudioSource>();
        _sale.Play();
        inventory.ChangeCash(-_barrel_cost);
        StartCoroutine(Wait());
        spawn = this.transform.GetChild(2).transform.position;
        Instantiate(barrel, spawn, Quaternion.Euler(90, 0, 0), generator.transform).is_prefab = false;
    }
    
    private IEnumerator CheckBase()
    {
        yield return new WaitUntil(() => _is_activated == true);
        EventText text = interface_generator.addEventText(
            "Press F to buy barrel of oil (" + _barrel_cost + " coins)", "oil_base"
        );
        yield return new WaitUntil(() => _is_activated == false);
        text.hideAndDisable();
        StartCoroutine(CheckBase());
    }
    private IEnumerator Wait() 
    {
        waiting = true;
        yield return new WaitForSeconds(time_of_wait);
        waiting = false;
    }
}
