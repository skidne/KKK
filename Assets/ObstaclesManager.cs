using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    public Pool[] obstaclePools;

    public GameObject potato;
    public GameObject poisonedPotato;
    public GameObject o2cylinder;

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
            PoolManager.instance.CreatePool(obstaclePools[i].prefab, obstaclePools[i].amount, _gameManager.canvas.transform);
        PoolManager.instance.SortByAmount(ref obstaclePools);
    }

    void Update()
    {
        int howToSpawn;
        int i;

        howToSpawn = Random.Range(0, 6);
        Debug.Log("0WTF????");
        if (CanSpawnWave(howToSpawn))
        {
            Debug.Log("1WTF????");
            for (i = 0; i < 3; i++)
            {
                if (spawnPatterns[howToSpawn, i] == 1)
                {
                    Debug.Log("2WTF????");
                    //  Debug.Log("jmm");
                    SpawnOnLane(i);
                }
                else
                    ProcessZero(i);
            }
        }
    }

    void SpawnOnLane(int l)
    {
        int whatToSpawn;

        whatToSpawn = GetRandomPool();
        _gameManager.lanes[l].lastSpawn = PoolManager.instance.ReuseObject(obstaclePools[whatToSpawn].prefab, _gameManager.spawnPoints[l].position, Quaternion.identity);
    }

    void ProcessZero(int l)
    {
        float zeroSpawn;
        float whatToSpawn;

        zeroSpawn = Random.value;
        if (zeroSpawn >= .6f)
        {
            whatToSpawn = Random.value;
            if (whatToSpawn <= .2f)
            {
                SpawnCylinder(l);
                //Spawn cylinder
            }
            else
            {
                if (_gameManager.lanes[l].isFree)
                    SpawnPotatos(l);
                //Spawn potato
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
            if (poisonChance < .7f || poisonChance > .8f)
                PoolManager.instance.ReuseObject(potato, nextPotatoPos, Quaternion.identity);
            else
                PoolManager.instance.ReuseObject(poisonedPotato, nextPotatoPos, Quaternion.identity);
            nextPotatoPos.y += 80;
        }
        _gameManager.lanes[whereToSpawn].lastSpawn = PoolManager.instance.ReuseObject(potato, nextPotatoPos, Quaternion.identity);
    }

    bool CanSpawnWave(int patternIndex)
    {
        int i;
        bool _canSpawn;

        _canSpawn = true;
        for (i = 0; i < _gameManager.lanes.Length; i++)
        {
            if (_gameManager.lanes[i].lastSpawn != null)
            {
                if (!_gameManager.lanes[i].isFree && _gameManager.lanes[i].lastSpawn.CompareTag("Obstacle"))
                    _canSpawn = false;
            }
        }
        Debug.Log("01: " + _canSpawn);
        if (_canSpawn == true)
        {
            //if ()
            if (CheckIntersections(patternIndex))
            {
                _canSpawn = true;
            }
            else
                _canSpawn = false;
        }
        Debug.Log("02: " + _canSpawn + " | " + CheckIntersections(patternIndex));
        return _canSpawn;
    }

    bool CheckIntersections(int pattern)
    {
        int i;
        bool goodIntersetction;

        goodIntersetction = true;
        for (i = 0; i < 3; i++)
        {
            if (spawnPatterns[pattern, i] == 1)
            {
                if (_gameManager.lanes[i].lastSpawn != null)
                {
                    if (!_gameManager.lanes[i].isFree && (_gameManager.lanes[i].lastSpawn.CompareTag("Potato") || _gameManager.lanes[i].lastSpawn.CompareTag("PoisonedPotato")))
                        goodIntersetction = false;
                }
            }
        }
        return goodIntersetction;
        /*
        goodIntersetction = false;
        for (i = 0; i < 3; i++)
        {
            Debug.Log("Entered in checker");
            if (spawnPatterns[pattern, i] == 0)
            {
                if (_gameManager.lanes[i].lastSpawn != null)
                {
                    if (_gameManager.lanes[i].isFree && (_gameManager.lanes[i].lastSpawn.CompareTag("Potato") || _gameManager.lanes[i].lastSpawn.CompareTag("PoisonedPotato")))
                    {
                        Debug.Log("1: Zdesi");
                        goodIntersetction = true;
                    }
                    else
                    {
                        if (_gameManager.lanes[i].isFree)
                            goodIntersetction = true;
                    }
                }
                else
                {
                    Debug.Log("2 Zdesi");
                    goodIntersetction = true;
                }
            }
        }
        return goodIntersetction;*/
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

/*
public class ObstaclesManager : MonoBehaviour {

    public Pool[] obstaclePools;
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Transform _mainCanvas = GameObject.Find("MainCanvas").transform;
        for (int i = 0; i < obstaclePools.Length; i++)
        {
            if (!obstaclePools[i].parent)
                obstaclePools[i].parent = _mainCanvas;
            PoolManager.instance.CreatePool(obstaclePools[i].prefab, obstaclePools[i].amount, obstaclePools[i].parent);
        }
        PoolManager.instance.SortByAmount(ref obstaclePools);
    }

    void Update()
    {
        int whereToSpawn;
        int whatToSpawn;

        whereToSpawn = Random.Range(0, 3);
        if (whereToSpawn > 2) whereToSpawn = 2;
        whatToSpawn = GetRandomPool();
        //_cd -= Time.deltaTime;
        if (CheckSpawnPosibility(whereToSpawn))
            if (PoolManager.instance.CanPool(obstaclePools[whatToSpawn].prefab))
                if (_gameManager.lanes[whereToSpawn] != null)
                    _gameManager.lanes[whereToSpawn].lastSpawn = PoolManager.instance.ReuseObject(obstaclePools[whatToSpawn].prefab, _gameManager.spawnPoints[whereToSpawn].position, Quaternion.identity);
    }

    bool CheckSpawnPosibility(int l)
    {
        bool _canSpawn;
        bool _selectedLane;
        bool _neighbors;
        int i;

        _canSpawn = false;
        _selectedLane = false;
        _neighbors = false;
        if (_gameManager.lanes[l].isFree)
            _selectedLane = true;
        for (i = 0; i < 3; i++)
            if (i != l)
            {
                ;
                if (_gameManager.lanes[i].isFree)
                    _neighbors = true;
            }
        if (_selectedLane && _neighbors)
            _canSpawn = true;
        return _canSpawn;
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
                break ;
        }
        return (i);
    }
}*/