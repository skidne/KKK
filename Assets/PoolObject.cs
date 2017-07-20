using UnityEngine;

public class PoolObject : MonoBehaviour
{
    GameManager _gameManager;
    AnimationManager _animationManager;
    //ObstaclesManager _obstManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _animationManager = _gameManager.gameObject.GetComponent<AnimationManager>();
        //_obstManager = FindObjectOfType<ObstaclesManager>();
    }

    public virtual void OnObjectReuse()
    {

    }

    void Update()
    {
        transform.Translate(0, -_gameManager.generalSpeed * 5, 0);
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
            {
                _gameManager.CurrentRunCoinsAdjust(1);
                AnimationManager.instance.PotatoIndicator();
            }
            else if (gameObject.tag == "PoisonedPotato")
            {
                _gameManager.CurrentRunCoinsAdjust(-(_gameManager.CurrentRunCoins / 3));
                AnimationManager.instance.PoisonedPotatoIndicator();
            }
            else if (gameObject.tag == "Cylinder")
            {
                _gameManager.O2AmountAdjust(1);
            }
            else if (gameObject.tag == "Obstacle")
            {
                _animationManager.CollisionShake();
                if (transform.position.y > _gameManager.playerHeight - 10)
                    _gameManager.ObstacleCollision(gameObject);
            }
        }
        this.Destroy();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        this.Destroy();
    }
}