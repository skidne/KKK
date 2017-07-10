using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Accelerator : MonoBehaviour {

    public Sprite[] amountStates;
    private bool accelerating;
    private const float maxAmount = 100;
    private const float minAmount = 0;
    private float amountPerFrame;
    private Image amountIndicatorSprite;
    private GameManager _gameManager;

    void Start()
    {
        accelerating = false;
        amountPerFrame = 10f;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        amountIndicatorSprite = transform.FindChild("O2Amount").GetComponent<Image>();
        GetIndicatorVal();
    }

    void Update()
    {
        if (accelerating)
        {
            _gameManager.O2AmountAdjust(-amountPerFrame * Time.deltaTime);
            _gameManager.generalSpeed += Time.deltaTime * .2f;
            if (_gameManager.OxygenAmount <= 0.1)
            {
                accelerating = false;
                _gameManager.SetO2AmountTo(0);
            }
        }
        GetIndicatorVal();
    }
    /*
    void OnTriggerEnter2D(Collider2D col)
    {

    }
    */
    public void AcceleratorDown()
    {
        if (_gameManager.OxygenAmount > minAmount)
            accelerating = true;
    }

    public void AcceleratorUp()
    {
        accelerating = false;
    }

    void GetIndicatorVal()
    {
        amountIndicatorSprite.color = new Color(1, 1, 1, 1);
        if ((int)(_gameManager.OxygenAmount / 25f) >= 3)
        {
            amountIndicatorSprite.sprite = amountStates[0];
        }
        else if ((int)(_gameManager.OxygenAmount / 25f) == 2)
        {
            amountIndicatorSprite.sprite = amountStates[1];
        }
        else if ((int)(_gameManager.OxygenAmount / 25f) == 1)
        {
            amountIndicatorSprite.sprite = amountStates[2];
        }
        else if ((_gameManager.OxygenAmount / 25f) > 0 && _gameManager.OxygenAmount / 25f < 1)
        {
            amountIndicatorSprite.sprite = amountStates[3];
        }
        else if (_gameManager.OxygenAmount <= 0)
        {
            amountIndicatorSprite.color = new Color(0, 0, 0, 0);
            amountIndicatorSprite.sprite = null;
        }
    }
}