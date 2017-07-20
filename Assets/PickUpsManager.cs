using UnityEngine;
using System.Collections;

public class PickUpsManager : MonoBehaviour {
    /*
    public GameObject potato;
    public GameObject poisonedPotato;
    public GameObject o2cylinder;
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        PoolManager.instance.CreatePool(potato, 200, _gameManager.canvas);
        PoolManager.instance.CreatePool(o2cylinder, 10, _gameManager.canvas);
        PoolManager.instance.CreatePool(poisonedPotato, 10, _gameManager.canvas);
        //cylinderSpawnCD = 10;
        //potatoSpawnCD = 1;
    }
    /*
    void Update()
    {
        float whatToSpawn;
        int whereToSpawn;

        whatToSpawn = Random.value;
        whereToSpawn = Random.Range(0, 3);
        cylinderSpawnCD -= Time.deltaTime;
        potatoSpawnCD -= Time.deltaTime;
        if (whereToSpawn > 2) whereToSpawn = 2;
        if (whatToSpawn <= .3f)
        {
            if (cylinderSpawnCD <= 0 && !_gameManager.lanes[whereToSpawn].lastSpawn.CompareTag("Potato"))
            {
                SpawnO2();
                cylinderSpawnCD = Random.Range(20, 45);
            }
        }
        else
        {
            if (potatoSpawnCD <= 0 && !_gameManager.lanes[whereToSpawn].lastSpawn.CompareTag("Potato"))
            {
                SpawnPotatos(whereToSpawn);
                potatoSpawnCD = Random.Range(10, 15);
            }
        }
    }
    
    void SpawnPotatos(int whereToSpawn)
    {
        int nr;
        int i;
        float poisonChance;
        Vector3 initSpawnPos;

        nr = Random.Range(5, 15);
        initSpawnPos = _gameManager.spawnPoints[whereToSpawn].position;
        Vector3 nextPotatoPos = initSpawnPos;
        for (i = 0; i < nr - 1; i++)
        {
            poisonChance = Random.value;
            nextPotatoPos.y += 80;
            if (poisonChance <= .8f || poisonChance >= .9f)
                PoolManager.instance.ReuseObject(potato, nextPotatoPos, Quaternion.identity);
            else
                PoolManager.instance.ReuseObject(poisonedPotato, nextPotatoPos, Quaternion.identity);
    }
        _gameManager.lanes[whereToSpawn].lastSpawn = PoolManager.instance.ReuseObject(potato, nextPotatoPos, Quaternion.identity);
    }

    void SpawnO2()
    {

    }
    /*
    int GetRandomPool()
    {
        int randVal;
        int sum;
        int i;

        sum = 0;
        for (i = 0; i < consumablesPools.Length; i++)
            sum += consumablesPools[i].amount;
        randVal = Random.Range(0, sum);
        sum = 0;
        for (i = 0; i < consumablesPools.Length - 1; i++)
        {
            sum += consumablesPools[i].amount;
            if (randVal < sum)
                break;
        }
        return (i);
    }*/
}
