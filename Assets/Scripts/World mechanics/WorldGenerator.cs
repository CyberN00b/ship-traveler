using System.Collections;
using System.Collections.Generic;
using Redcode.Extensions;
using UnityEngine;

public class Struction 
{
    public float x, z;
    private float rand_range = 20;
    public Port port;
    public Port spawned_port = null;
    public Struction(float new_x, float new_z, Port new_port)
    {
        x = Random.Range(new_x - rand_range, new_x - rand_range);
        z = Random.Range(new_z - rand_range, new_z - rand_range);
        port = new_port;
    }
    public bool IsInDistance(float point_x, float point_z) 
    {
        point_x -= x;
        point_z -= z;
        return point_x * point_x + point_z * point_z <= port.spawn_distance * port.spawn_distance;
    }
}
class SpawnRect
{
    public int lx, rx, lz, rz, center_x, center_z;
    public int size = 500;
    public SpawnRect(int x, int z)
    {
        center_x = x;
        center_z = z;
        lx = x - size;
        rx = x + size;
        lz = z - size;
        rz = z + size;
    }
    public bool IsCollide(int x, int z)
    {
        return (x >= lx) && (x <= rx) && (z >= lz) && (z <= rz);
    } 
}
public class WorldGenerator : MonoBehaviour
{
    [SerializeField]
    private float _player_radius = 85;
    private float _game_radius = 90;
    private int sum_of_frequency = 0;
    private EndPoint _End = null;
    public float player_radius {
        get{return _player_radius;}
    }
    public float game_radius {
        get{return _game_radius;}
    }
    private Controller controller = null;
    private Moving ship = null;
    [SerializeField] private DynamicObject[] _bonusprefabs;
    [SerializeField] private EndPoint _endPoint;
    [SerializeField] private OilBase _oilBase;
    private List<Struction> _structions = new List<Struction>();
    public List<Struction> structions {
        get {return _structions;}
    }
    private List<SpawnRect> rects = new List<SpawnRect>();
    private SpawnRect current_rect = null;

    private void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<Controller>();
        ship = GameObject.Find("Player").GetComponent<Moving>();
        foreach (DynamicObject bonus in _bonusprefabs) 
        {
            sum_of_frequency += bonus.frequency;
        }
        _structions.Add(new Struction(controller.point_x, controller.point_z, _endPoint));
        StartCoroutine(BonusGeneration());
        StartCoroutine(StructionGenerator());
        StartCoroutine(StructionChecker());
    }
    void SpawnStructions()
    {
        if (
            current_rect == null ||
            Mathf.Abs(current_rect.center_x - controller.pos_x) > 350 || 
            Mathf.Abs(current_rect.center_z - controller.pos_z) > 350
            ) 
        {
            current_rect = new SpawnRect((int)controller.pos_x, (int)controller.pos_z);
            for (int i = current_rect.lx; i <= current_rect.rx; i += 200)
                for (int j = current_rect.lx; j <= current_rect.rx; j += 200)
                {
                    if (Mathf.Abs(i - controller.pos_x) + Mathf.Abs(j - controller.pos_z) < 100)
                        continue;
                    if (Random.Range(0, 5) == 0) 
                    {
                        bool flag = false;
                        foreach (SpawnRect rect in rects) 
                        {
                            if (rect.IsCollide(i, j)) {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag) 
                        {
                            _structions.Add(new Struction(i, j, _oilBase));
                        }
                    }
                }
        }
    }
    void CheckStructions() 
    {
        foreach (Struction struction in _structions)
        {
            if (struction.IsInDistance(controller.pos_x, controller.pos_z)) 
            {
                if (struction.spawned_port == null) 
                {
                    struction.spawned_port = (
                        Instantiate(
                            struction.port, 
                            new Vector3(struction.x - controller.pos_x, 0, struction.z - controller.pos_z), 
                            Quaternion.identity, transform
                        )
                    );
                    struction.spawned_port.is_prefab = false;
                }
            } else {
                if (struction.spawned_port != null)
                {
                    Destroy(struction.spawned_port);
                    struction.spawned_port = null;
                }
            }
        }
    }
    void SpawnBonus()
    {
        float ship_angle = controller.angle;
        if (ship.speed < 0) 
        {
            ship_angle += 180;
            if (Mathf.Abs(ship_angle) > 180f) 
            {
                if (ship_angle < 0)
                    ship_angle += 360;
                else
                    ship_angle -= 360;
            }
        }
        float point_angle = (controller.direction * controller.speed + ship.speed * ship_angle) / (controller.speed + ship.speed);
        for(float i = -_game_radius; i < _game_radius; i += 0.5f)
        {
            for(float j = -_game_radius; j < _game_radius; j += 0.5f)
            {
                float radius = j * j + i * i;
                if (_game_radius * _game_radius > radius && radius > _player_radius * _player_radius) 
                {
                    float angle = Mathf.Atan2(i, j) * Mathf.Rad2Deg - point_angle;
                    if (Mathf.Abs(point_angle) > 90 && ((angle >= 0 && point_angle < 0) || (angle < 0 && point_angle >= 0))) 
                    {
                        angle = 360 - Mathf.Abs(angle) - Mathf.Abs(point_angle);
                    } 
                    if (Random.Range(0, 5000) == 0 && angle > -90 && angle < 90)
                    {
                        var tmp = Instantiate(GetRandomBonus(), new Vector3(i, 0f, j), Quaternion.identity, transform);
                        tmp.transform.SetLocalPositionY(tmp.spawnY);
                        tmp.is_prefab = false;
                    }
                }
            }
        }
    }
    DynamicObject GetRandomBonus()
    {
        int rand = Random.Range(0, sum_of_frequency);
        foreach (DynamicObject bonus in _bonusprefabs) 
        {
            if (rand >= bonus.frequency) 
            {
                rand -= bonus.frequency;
            } else {
                return bonus;
            }
        }
        return null;
    }
    IEnumerator BonusGeneration() 
    {
        for (;;) 
        {
            SpawnBonus();
            yield return new WaitForSeconds((game_radius - player_radius) / ((ship.speed + controller.speed) * 2));
        }
    }
    IEnumerator StructionGenerator() 
    {
        for (;;) {
            SpawnStructions();
            yield return new WaitForSeconds(5f);
        }
    }
    IEnumerator StructionChecker() 
    {
        for (;;) {
            CheckStructions();
            yield return new WaitForSeconds(4f);
        }
    }
}