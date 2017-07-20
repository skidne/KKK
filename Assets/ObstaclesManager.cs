using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    public Pool[] obstaclePools;

    public Transform obstacleParent;

    public GameObject potato;
    public GameObject poisonedPotato;
    public GameObject o2cylinder;

    private float checkDelay;
    private float checkCD;

    private float potatoDelay;

    private int[,] spawnPatterns = { {1, 0, 0},
                                     {0, 1, 0},
                                     {0, 0, 1},
                                     {1, 1, 0},
                                     {0, 1, 1},
                                     {1, 0, 1} };

    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        for (int i = 0; i < obstaclePools.Length; i++)
            PoolManager.instance.CreatePool(obstaclePools[i].prefab, obstaclePools[i].amount, obstacleParent);
        PoolManager.instance.CreatePool(potato, 200, obstacleParent);
        PoolManager.instance.CreatePool(o2cylinder, 10, obstacleParent);
        PoolManager.instance.CreatePool(poisonedPotato, 10, obstacleParent);
        PoolManager.instance.SortByAmount(ref obstaclePools);
        potatoDelay = Screen.height * 10 / 100;
        checkDelay = 0.5f;
    }

    void Update()
    {
        int howToSpawn;

        if (_gameManager.currentGameZone == GameZone.Obstacles)
        {
            howToSpawn = Random.Range(0, 6);
            checkCD -= Time.deltaTime;
            if (CanSpawnWave(howToSpawn) && checkCD <= 0)
                SpawnWave(howToSpawn);
        }
    }

    void SpawnWave(int pattern)
    {
        int i;

        //Debug.Break();
        //Debug.Log("----------------");
        for (i = 0; i < 3; i++)
        {
            if (spawnPatterns[pattern, i] == 1)
            {
                SpawnOnLane(i);
            }
            else
            {
                ProcessZero(i);
            }
        }
        checkCD = checkDelay;
       // Debug.Log("----------------");
    }

    void SpawnOnLane(int l)
    {
        int whatToSpawn;

        whatToSpawn = GetRandomPool();
        _gameManager.lanes[l].lastSpawn = PoolManager.instance.ReuseObject(obstaclePools[whatToSpawn].prefab, _gameManager.spawnPoints[l].position, Quaternion.identity);
       // Debug.Log("Order: " + debugOrder + "; Name: " + _gameManager.lanes[l].lastSpawn.name);
        //Debug.Break();
    }

    void ProcessZero(int l)
    {
        float zeroSpawn;
        float whatToSpawn;

        zeroSpawn = Random.value;
       // Debug.Log("I'm here");
        if (zeroSpawn >= .6f)
        {
           // Debug.Log("Now here");
            if (_gameManager.IsFree(_gameManager.lanes[l]))
            {
                //Debug.Log("And now here");
                whatToSpawn = Random.value;
                if (whatToSpawn <= .2f)
                {
                    SpawnCylinder(l);
                    //Spawn cylinder
                }
                else
                {
                    SpawnPotatos(l);
                    //Spawn potato
                }
                //Debug.Log(debugOrder + ": " + _gameManager.lanes[l].lastSpawn.name);
                //Debug.Break();
            }
        }
    }

    void SpawnCylinder(int whereToSpawn)
    {
        _gameManager.lanes[whereToSpawn].lastSpawn = PoolManager.instance.ReuseObject(o2cylinder, _gameManager.spawnPoints[whereToSpawn].position, Quaternion.identity);
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
            if (poisonChance > .05f)
                PoolManager.instance.ReuseObject(potato, nextPotatoPos, Quaternion.identity);
            else
                PoolManager.instance.ReuseObject(poisonedPotato, nextPotatoPos, Quaternion.identity);
            nextPotatoPos.y += potatoDelay;
        }
        _gameManager.lanes[whereToSpawn].lastSpawn = PoolManager.instance.ReuseObject(potato, nextPotatoPos, Quaternion.identity);
    }

    bool CanSpawnWave(int patternIndex)
    {
        int i;
        //Debug.Log("/*CHECK IF CAN SPAWN*/");
       // Debug.Log(_gameManager.lanes.Length);
        for (i = 0; i < _gameManager.lanes.Length; i++)
        {
            //Debug.Log("i = " + i);
            if (_gameManager.lanes[i].lastSpawn != null)
                //if (_gameManager.lanes[i].lastSpawn.transform.position.y > (_gameManager.topLimitHeight - _gameManager.obstacleDist) && _gameManager.lanes[i].lastSpawn.CompareTag("Obstacle"))
                if (!_gameManager.IsFree(_gameManager.lanes[i]) && _gameManager.lanes[i].lastSpawn.tag == "Obstacle")
                {
                 //   Debug.Log("/CAN'T SPAWN CUZ OF OBSTACLE DISTANCE\\");
                    return false;
                }
        }
        if (!CheckIntersections(patternIndex))
        {
          //  Debug.Log("/CAN'T SPAWN CUZ OF INTERSECTIONS\\");
            return false;
        }
      //  Debug.Log("/*SOMEHOW CAN SPAWN*\\");
        return true;
    }

    bool CheckIntersections(int pattern)
    {
        int i;

        for (i = 0; i < 3; i++)
        {
            if (spawnPatterns[pattern, i] == 1 && (_gameManager.lanes[i].lastSpawn && !_gameManager.lanes[i].lastSpawn.CompareTag("Obstacle")) && !_gameManager.IsFree(_gameManager.lanes[i]))
                return false;
        }
        return true;
    }

    int GetRandomPool()
    {
        int randVal;
        int sum;
        int i;

        sum = 0;
        for (i = 0; i < obstaclePools.Length; i++)
            sum += obstaclePools[i].amount;
        randVal = Random.Range(0, sum);
        sum = 0;
        for (i = 0; i < obstaclePools.Length - 1; i++)
        {
            sum += obstaclePools[i].amount;
            if (randVal < sum)
                break;
        }
        return (i);
    }
}