using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public LaneClass[] lanes;
    public Transform[] spawnPoints;

    public RectTransform canvas;
    public RectTransform deathZone;

    public GameObject gaugeBase;
    public GameObject gaugeArrow;

    public Transform topLimitObj;
    public Transform playerTransform;

    public Text gameOverText;
    public Text coinsCountText;

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

    private float _o2amount;
    public float OxygenAmount
    {
        get { return _o2amount; }
    }

    public const float maxSpeed = 2;
    public const float minSpeed = 0;

    private const float gaugeRotLimit = -240;
    private RectTransform _gArrowRect;

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
        obstacleDist = Screen.height * 30 / 100;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        wid = deathZone.parent.GetComponent<RectTransform>().rect.width;
        deathZone.sizeDelta = new Vector2(wid, 200);
        deathZone.GetComponent<BoxCollider2D>().size = deathZone.sizeDelta;

        //Initialize current run coint count with 0
        _curRunCoins = 0;
        coinsCountText.text = "0";

        _o2amount = 100;
    }

    void Update()
    {
        int i;
        float _dist;

        generalSpeed -= Time.deltaTime * .008f;
        generalSpeed = Mathf.Clamp(generalSpeed, minSpeed, maxSpeed);
        _gArrowRect.localEulerAngles = new Vector3(_gArrowRect.localEulerAngles.x, _gArrowRect.localEulerAngles.y, generalSpeed * gaugeRotLimit / maxSpeed);

        for (i = 0; i < 3; i++)
        {
            lanes[i].isFree = true;
            if (lanes[i].lastSpawn)
            {
                _dist = obstacleDist;
                if (lanes[i].lastSpawn.transform.position.y > (topLimitHeight - _dist - (generalSpeed * 20)))
                    lanes[i].isFree = false;
                if (lanes[i].lastSpawn.transform.position.y < (topLimitHeight - canvas.rect.height - 50))
                {
                    lanes[0].lastSpawn = null;
                    lanes[1].lastSpawn = null;
                    lanes[2].lastSpawn = null;
                }
            }
        }
        playerHeight = playerTransform.position.y;
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

    public void O2AmountAdjust(float cnt)
    {
        _o2amount += cnt;
    }

    public void SetO2AmountTo(float cnt)
    {
        _o2amount = cnt;
    }
}

[System.Serializable]
public class LaneClass
{
    public GameObject lastSpawn;
    public bool isFree;
}