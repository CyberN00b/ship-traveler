using System.Collections;
using System.Collections.Generic;
using Redcode.Extensions;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{

    private float _player_radius = 75;
    private float _game_radius = 80;
    private bool _is_endPoint_spawned = false;
    private EndPoint _End = null;
    public float player_radius {
        get{return _player_radius;}
    }
    public float game_radius {
        get{return _game_radius;}
    }
    private Controller controller = null;
    private Moving ship = null;
    [SerializeField] private Bonus[] _bonusprefabs;
    [SerializeField] private EndPoint _endPoint;
    private Bonus RandomBonus() => _bonusprefabs[UnityEngine.Random.Range(0, _bonusprefabs.Length)];

    private void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<Controller>();
        ship = GameObject.Find("Player").GetComponent<Moving>();
        StartCoroutine("BonusGeneration");
        StartCoroutine("EndPointGenerator");
    }
    void Update() {

    }
    void SpawnBonus(){
        float point_angle = (controller.direction * controller.speed + ship.speed * controller.angle) / (controller.speed + ship.speed);
        for(float i = -_game_radius; i < _game_radius; i += 0.5f)
        {
            for(float j = -_game_radius; j < _game_radius; j += 0.5f)
            {
                float radius = j * j + i * i;
                if (_game_radius * _game_radius > radius && radius > _player_radius * _player_radius) {
                    float angle = Mathf.Atan2(i, j) * Mathf.Rad2Deg - point_angle;
                    if (Mathf.Abs(point_angle) > 90 && ((angle >= 0 && point_angle < 0) || (angle < 0 && point_angle >= 0))) {
                        angle = 360 - Mathf.Abs(angle) - Mathf.Abs(point_angle);
                    } 
                    if (Random.Range(0, 5000) == 0 && angle > -90 && angle < 90)
                    {
                        var tmp = Instantiate(RandomBonus(), new Vector3(i, 0f, j), Quaternion.identity, transform);
                        tmp.transform.SetLocalPositionY(tmp.spawnY);
                        tmp.is_prefab = false;
                    }
                }
            }
        }
    }
    IEnumerator BonusGeneration() 
    {
        for(;;) 
        {
            SpawnBonus();
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator EndPointGenerator() 
    {
        for(;;) 
        {
            if (Mathf.Abs(controller.pos_x - controller.point_x) < 100 && Mathf.Abs(controller.pos_z - controller.point_z) < 100) {
                if (!_is_endPoint_spawned) {
                    _End = Instantiate(_endPoint, new Vector3(controller.point_x - controller.pos_x, 0, controller.point_z - controller.pos_z), Quaternion.identity, transform);
                    _End.is_prefab = false;
                    _is_endPoint_spawned = true;
                } 
            } else
                if (_is_endPoint_spawned) {
                    Destroy(_End);
                    _End = null;
                    _is_endPoint_spawned = false;
                }
            yield return new WaitForSeconds(2f);
        }
    }
}
