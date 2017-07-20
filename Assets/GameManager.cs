using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public LaneClass[] lanes;
    public Transform[] spawnPoints;

    public RectTransform canvas;
    public RectTransform deathZone;

    public ParticleSystem obstacleDestructionEffect;

    public GameObject gaugeBase;
    public GameObject gaugeArrow;
    

    public Transform topLimitObj;
    public Transform playerTransform;

    public GameZone currentGameZone;

    public Text gameOverText;
    public Text coinsCountText;
    public Text scoreText;

    private bool _invulnerability;

    private int _curRunCoins;
    public int CurrentRunCoins
    {
        get { return _curRunCoins; }
    }

    [HideInInspector]
    public float playerHeight;

    public float topLimitHeight;
    public float pickupDist;
    public float generalSpeed;
    public float obstacleDist;

    private UnityStandardAssets.ImageEffects.BlurOptimized blurComp;

    private int _o2charges;
    public int OxygenCharges
    {
        get { return _o2charges; }
    }

    public float distanceScore;

    float _maxSpeed;
    float _minSpeed;

    public float MaxSpeed
    {
        get { return _maxSpeed; }
    }

    private const float gaugeRotLimit = -240;
    RectTransform _gArrowRect;

    float savedSpeed;
    float savedMaxSpeed;

    void Start()
    {
        float wid;

        topLimitHeight = topLimitObj.position.y;
        _gArrowRect = gaugeArrow.GetComponent<RectTransform>();
        lanes = new LaneClass[3];
        for (int i = 0; i < 3; i++)
        {
            lanes[i] = new LaneClass();
            lanes[i].isFree = true;
        }
        obstacleDist = Screen.height * 35 / 100;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        wid = deathZone.parent.GetComponent<RectTransform>().rect.width;
        deathZone.sizeDelta = new Vector2(wid, 200);
        deathZone.GetComponent<BoxCollider2D>().size = deathZone.sizeDelta;
        _maxSpeed = 4f;
        _minSpeed = 0f;
        savedMaxSpeed = _maxSpeed;
        savedSpeed = generalSpeed;
        _curRunCoins = 0;
        _o2charges = 4;
        blurComp = Camera.main.GetComponent<UnityStandardAssets.ImageEffects.BlurOptimized>();
        blurComp.blurSize = 0f;
    }

    void Update()
    {
        generalSpeed -= Time.deltaTime * (_maxSpeed * 1 / 100);
        generalSpeed = Mathf.Clamp(generalSpeed, _minSpeed, _maxSpeed);
        _o2charges = Mathf.Clamp(_o2charges, 0, 4);
        _gArrowRect.localEulerAngles = new Vector3(_gArrowRect.localEulerAngles.x, _gArrowRect.localEulerAngles.y, generalSpeed * gaugeRotLimit / _maxSpeed);
        playerHeight = playerTransform.position.y;
        distanceScore += generalSpeed * 2 * Time.deltaTime;
        scoreText.text = ((int)distanceScore).ToString() + "m";
    }

    public void GameOver()
    {
        gameOverText.text = "GAME OVER";
    }

    public void CurrentRunCoinsAdjust(int cnt)
    {
        _curRunCoins += cnt;
        coinsCountText.text = _curRunCoins.ToString();
    }

    public void O2AmountAdjust(int cnt)
    {
        _o2charges += cnt;
    }

    public void SetO2AmountTo(int cnt)
    {
        _o2charges = cnt;
    }

    public void ObstacleCollision(GameObject _obstacle)
    {
        if (_invulnerability)
        {
            obstacleDestructionEffect.transform.position = new Vector3(_obstacle.transform.position.x, _obstacle.transform.position.y - 50, -260);
            obstacleDestructionEffect.Play();
        }
        else
            generalSpeed -= _maxSpeed / 3;
        //Debug.Log(_maxSpeed);
    }

    public void AccelerateSpeed()
    {
        _invulnerability = true;
        blurComp.blurSize = 1.5f;
        savedSpeed = generalSpeed;
        savedMaxSpeed = _maxSpeed;
        _maxSpeed = 8;
        generalSpeed = _maxSpeed;
    }

    public void ResetAcceleratedSpeed()
    {
        generalSpeed = savedSpeed;
        _maxSpeed = savedMaxSpeed;
        blurComp.blurSize = 0f;
        _invulnerability = false;
    }

    public bool IsFree(LaneClass lane)
    {
        if (lane.lastSpawn)
            if (lane.lastSpawn.transform.position.y > (topLimitHeight - obstacleDist))
                return false;
        return true;
    }
}

[System.Serializable]
public class LaneClass
{
    public GameObject lastSpawn;
    public bool isFree;
}