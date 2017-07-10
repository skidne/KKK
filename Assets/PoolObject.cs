using UnityEngine;

public class PoolObject : MonoBehaviour
{
    GameManager _gameManager;
    //ObstaclesManager _obstManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //_obstManager = FindObjectOfType<ObstaclesManager>();
    }

    public virtual void OnObjectReuse()
    {

    }

    void Update()
    {
        transform.Translate(0, -_gameManager.generalSpeed * 3, 0);
    }

    protected void Destroy()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.tag == "Potato")
                _gameManager.CurrentRunCoinsAdjust(1);
            else if (gameObject.tag == "PoisonedPotato")
            {
                _gameManager.CurrentRunCoinsAdjust(-(_gameManager.CurrentRunCoins / 3));
            }
            else if (gameObject.tag == "Cylinder")
            {
                _gameManager.O2AmountAdjust(25);
            }
            else if (gameObject.tag == "Obstacle")
            {
                if (transform.position.y > _gameManager.playerHeight - 10)
                    _gameManager.GameOver();
            }
        }
        this.Destroy();
    }
}