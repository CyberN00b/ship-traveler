using System.Collections;
using System.Collections.Generic;
using Redcode.Extensions;
using UnityEngine;
using UnityEngine.Audio;
public class Moving : MonoBehaviour
{
    [SerializeField]
    private AudioClip ship_staying = null;
    [SerializeField]
    private AudioClip ship_moving = null;
    [SerializeField]
    private AudioClip ship_begin = null;
    [SerializeField]
    private AudioClip ship_stopping = null;
    [SerializeField]
    private AudioClip ship_stop_move = null;
    [SerializeField]
    private AudioClip ship_move_stop = null;

    private AudioSource sound = null;
    [SerializeField]
    private float _speed = 0;
    [SerializeField]
    private float _fuel = 100;
    private float _max_fuel = 100;
    private float _max_speed = 0;
    private float _fuel_decrease = 0.3f; 
    private int _max_health = 100;
    public int max_health {
        get {return _max_health;}
    }
    [SerializeField]
    private int _health = 0;
    private float _overheat = 0;
    private float _overheat_increase = 2f;
    private float _boost_amount = 0f;
    private float _mass = 5;
    private float _acceleration = 0;
    private float _force = 5f;
    private float _rotation = 0;
    private int _rotation_direction = 0;
    private float _max_rotation = 30;
    private float _rotation_N = 0;
    private float _percent_stop = 0.2f;
    public bool is_boosted = false;
    private bool _is_engine_started = false;
    private Coroutine sound_coroutine = null;
    private Controller controller = null;
    private InterfaceGenerator generator = null;
    private MenuController menu_controller = null;
    private AudioSource audio_source = null;
    private Inventory inventory = null;
    private Item_boost booster = null;
    private Item[] heal_items;
    private int _heal_item_selected = 0;
    public Item heal_item_selected 
    {
        get {return heal_items[_heal_item_selected];}
    }
    public float boost_amount 
    {
        get {return _boost_amount;}
        set { _boost_amount = Mathf.Max(Mathf.Min(value, 100), 0);}
    }
    public float overheat 
    {
        get {return _overheat;}
        set { _overheat = Mathf.Max(Mathf.Min(value, 100), 0);}
    }
    public float fuel 
    {
        get {return _fuel;}
        set {_fuel = Mathf.Max(Mathf.Min(value, _max_fuel), 0);}
    }
    public int health
    {
        get { return _health; }
        set { _health = Mathf.Max(Mathf.Min(value, _max_health), 0);}
    }
    public int rotation_direction 
    {
        get {return _rotation_direction;}
    }
    public float speed 
    {
        get {return _speed;}
        set {_speed = value;}
    }
    public float max_speed 
    {
        get {return _max_speed;}
    }

    void Awake() 
    {
        controller = GameObject.Find("GameController").GetComponent<Controller>();
        generator = GameObject.Find("Generator").GetComponent<InterfaceGenerator>();
        menu_controller = GameObject.Find("GameController").GetComponent<MenuController>();
        sound = this.GetComponent<AudioSource>();
        audio_source = this.GetComponent<AudioSource>();
        inventory = this.GetComponent<Inventory>();
        _max_speed = _force / (_percent_stop * _mass);
        _rotation_N = _max_rotation / _max_speed;
        _health = _max_health;
        booster = new Item_boost();
        heal_items = new Item[]{null, new Item_wrench(), new Item_cooller()};
    }
    void Start()
    {
        StartCoroutine(SpeedWork());
        StartCoroutine(OverheatDamage());
        sound_coroutine = StartCoroutine(Sound());
    }    
    void Update()
    {
        controller.ChangePosition(_speed, _rotation);
        if (menu_controller.IsEnabled())
            return;
        if (_fuel > 0 && _health > 0) 
        {
            if (Input.GetKeyDown(KeyCode.W))
                _acceleration = _force / _mass;
            else 
                if (Input.GetKeyDown(KeyCode.S))
                    _acceleration = -_force / (_mass * 1.5f);
            if ((Input.GetKeyUp(KeyCode.S) && _acceleration < 0) 
                || (Input.GetKeyUp(KeyCode.W) && _acceleration > 0))
                _acceleration = 0;
        } else 
        {
            _acceleration = 0;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (_boost_amount <= 0)
                inventory.UseItem("boost");
            else
                is_boosted = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            _heal_item_selected++;
            _heal_item_selected %= heal_items.Length;
        } else 
            if (Input.GetKeyDown(KeyCode.DownArrow)) 
            {
                _heal_item_selected--;
                if (_heal_item_selected < 0)
                    _heal_item_selected += heal_items.Length;
            }
        if (Input.GetKeyDown(KeyCode.H)) 
        {
            if (heal_items[_heal_item_selected] != null)
                inventory.UseItem(heal_items[_heal_item_selected].item_name);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && is_boosted == true) 
        {
            is_boosted = false;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            _rotation_direction = -1;
        } else 
            if (Input.GetKeyDown(KeyCode.D))
            {
                _rotation_direction = 1; 
            }
        if ((Input.GetKeyUp(KeyCode.A) && _rotation_direction < 0)
            || (Input.GetKeyUp(KeyCode.D) && _rotation_direction > 0))
            _rotation_direction = 0;
        fuel -= (_fuel_decrease + Mathf.Abs(_speed / 10)) * Time.deltaTime;
        BoostWork();
        this.transform.SetEulerAnglesY(controller.angle);
        this.transform.SetEulerAnglesZ(_rotation / 5);
    }
    void BoostWork() 
    {
        if (is_boosted)
        {
            boost_amount -= 100f / booster.time_of_boost * Time.deltaTime;;
            if (_boost_amount <= 0) 
            {
                if (!inventory.UseItem("boost"))
                    is_boosted = false;
            }
            if (_overheat < 100f)
                _overheat += (_overheat_increase + _overheat * 0.05f) * Time.deltaTime;
            else
                _overheat = 100f;
        } else 
        {
            if (_overheat > 0)
            {
                _overheat -= _overheat_increase * Time.deltaTime * 0.5f;
                if (_overheat < 0)
                {
                    _overheat = 0;
                }
            }
        }
    }
    IEnumerator SpeedWork() 
    {
        for(;;)
        {
            yield return new WaitForSeconds(0.05f);
            if (Mathf.Abs(_rotation + (((_speed < 0)? -1f : 1f) * _speed * 
                _rotation_N * _rotation_direction - _rotation / 2) / 60) < _max_rotation)
                _rotation += (((_speed < 0)? -1f : 1f) * _speed * _rotation_N * _rotation_direction - _rotation / 2) / 60;
            _speed += (_acceleration * ((is_boosted) ? 1.3f : 1) * ((!_is_engine_started) ? 0 : 1) - _speed * _percent_stop) / 6;
        }
    }
    public void decreaseHealth(int damage) 
    {
        _health -= damage;
        if (_health <= 0) 
        {
            _health = 0;
            GameObject.Find("GameOver").GetComponent<GameOver>().EndOfHealth();
        }
    }
    public void NoFuel() 
    {
        StopCoroutine(sound_coroutine);
        _is_engine_started = false;
        sound.clip = null;
        sound.PlayOneShot(ship_stopping);
        StartCoroutine(AfterFuelRestartSound());
    }
    IEnumerator AfterFuelRestartSound() 
    {
        yield return new WaitUntil(() => fuel > 0);
        sound_coroutine = StartCoroutine(Sound());
    }
    IEnumerator Sound() 
    {
        sound.clip = ship_staying;
        sound.PlayOneShot(ship_begin);
        yield return new WaitForSeconds(ship_begin.length - 0.55f);
        _is_engine_started = true;
        sound.Play();
        for (;;) 
        {
            yield return new WaitUntil(() => Mathf.Abs(_acceleration) > 0.5f);
            sound.Pause();
            sound.PlayOneShot(ship_stop_move);
            yield return new WaitForSeconds(ship_stop_move.length - 0.2f);
            sound.clip = ship_moving;
            sound.Play();
            yield return new WaitUntil(() => Mathf.Abs(_acceleration) < 0.5f);
            sound.Pause();
            if (fuel <= 0)
                break;
            sound.PlayOneShot(ship_move_stop);
            yield return new WaitForSeconds(ship_move_stop.length - 0.2f);
            sound.clip = ship_staying;
            sound.Play();
        }
    }
    IEnumerator OverheatDamage() 
    {
        for (;;) 
        {
            int damage = 0;
            if (50 <= _overheat && _overheat < 75)
                damage = Random.Range(1, 4);
            else 
                if (75 <= _overheat && _overheat < 90)
                    damage = Random.Range(5, 8);
                else 
                    if (90 <= _overheat && _overheat <= 100)
                        damage = Random.Range(8, 10);
            if (damage > 0) 
            {
                generator.addEventText("You got " + damage + " damage on your ship by overheat!").disableAfterSec(2f);
                decreaseHealth(damage);
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
 