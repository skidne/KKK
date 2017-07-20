using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Accelerator : MonoBehaviour {

    public Sprite[] amountStates;
    private float immortalityCD;
    private float immortalityDelay;
    private bool accelerating;
    private const float maxAmount = 4;
    private const float minAmount = 0;
    private Image amountIndicatorSprite;
    private Image _buttonImg;
    private GameManager _gameManager;
    private AnimationManager _animationManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        amountIndicatorSprite = transform.FindChild("O2Amount").GetComponent<Image>();
        _buttonImg = GameObject.Find("O2Indicator").GetComponent<Image>();
        _animationManager = _gameManager.GetComponent<AnimationManager>();
        immortalityDelay = 1f;
        accelerating = false;
}

    void Update()
    {
        GetIndicatorVal();

        _buttonImg.fillAmount = 1 - (immortalityCD / immortalityDelay);
        if (immortalityCD > 0)
            immortalityCD -= Time.deltaTime;
        else
        {
            if (accelerating)
            {
                _animationManager.AcceleratorEffectOff();
                _gameManager.ResetAcceleratedSpeed();
                immortalityCD = 0;
                accelerating = false;
            }
        }
        /*
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
        */
    }

    public void AcceleratorDown()
    {
        if (_gameManager.OxygenCharges > 0 && !accelerating)
            Accelerate();
    }

    void Accelerate()
    {
        accelerating = true;
        _gameManager.O2AmountAdjust(-1);
        _gameManager.generalSpeed += _gameManager.MaxSpeed / 4;
        _animationManager.AcceleratorEffectOn();
        Camera.main.GetComponent<CameraShaker>().ShakeCamera(2f, 1f * Time.deltaTime);
        immortalityCD = immortalityDelay;
        _gameManager.AccelerateSpeed();
    }

    void GetIndicatorVal()
    {
        amountIndicatorSprite.color = new Color(1, 1, 1, 1);
        if (_gameManager.OxygenCharges == 4)
        {
            amountIndicatorSprite.sprite = amountStates[0];
        }
        else if (_gameManager.OxygenCharges  == 3)
        {
            amountIndicatorSprite.sprite = amountStates[1];
        }
        else if (_gameManager.OxygenCharges == 2)
        {
            amountIndicatorSprite.sprite = amountStates[2];
        }
        else if (_gameManager.OxygenCharges == 1)
        {
            amountIndicatorSprite.sprite = amountStates[3];
        }
        else if (_gameManager.OxygenCharges <= 0)
        {
            amountIndicatorSprite.color = new Color(0, 0, 0, 0);
            amountIndicatorSprite.sprite = null;
        }
    }
}