using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{

    private float _player_radius = 50;
    private float _game_radius = 60;
    public float player_radius {
        get{return _player_radius;}
    }
    public float game_radius {
        get{return _game_radius;}
    }
    private Controller controller = null;
    [SerializeField] private Bonus[] _bonusprefabs;
    private Bonus RandomBonus() => _bonusprefabs[UnityEngine.Random.Range(0, _bonusprefabs.Length)];

    private void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<Controller>();
        StartCoroutine("BonusGeneration");
    }
    void Update() {

    }
    void SpawnBonus(){
        float tan = Mathf.Tan(Mathf.Deg2Rad * controller.angle);
        for(float i = -_game_radius; i < _game_radius; i += 0.5f)
        {
            for(float j = -_game_radius; j < _game_radius; j += 0.5f)
            {
                float radius = j * j + i * i;
                if (_game_radius * _game_radius > radius && radius > _player_radius * _player_radius) {
                    float angle = Mathf.Atan2(i, j) * Mathf.Rad2Deg - controller.angle;
                    if (Random.Range(0, 5000) == 0 && angle > -90 && angle < 90)
                    {
                        Instantiate(RandomBonus(), new Vector3(i, 1f, j), Quaternion.identity, transform).is_prefab = false;
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
}
