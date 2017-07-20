using UnityEngine;

public class GameZoneManager : MonoBehaviour {

    private float zoneChangeDelay;
    private float zoneChangeCD;
    private GameManager _gameManager;

    void Start()
    {
        zoneChangeDelay = 120;
        zoneChangeCD = zoneChangeDelay;
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager.currentGameZone = GameZone.Obstacles;
    }

    void Update()
    {
        if (zoneChangeCD > 0)
            zoneChangeCD -= Time.deltaTime;

        if (zoneChangeCD <= 0)
        {
            zoneChangeCD = zoneChangeDelay;
            ChangeZone();
        }

    }

    public void ChangeZone()
    {
        float zoneValue;

        zoneValue = Random.value;
        if (zoneValue >= 0.7f && zoneValue <= 0.8f)
            _gameManager.currentGameZone = GameZone.UFO;
        else
            _gameManager.currentGameZone = GameZone.Obstacles;
    }
}

public enum GameZone
{
    None,
    Obstacles,
    UFO
}