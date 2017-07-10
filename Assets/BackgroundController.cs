using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackgroundController : MonoBehaviour {

    private GameManager _gameManager;
    public BackgroundClass[] backgrounds;

    void Start()
    {
        _gameManager = gameObject.GetComponent<GameManager>();
    }

    void Update()
    {
        Rect _uvRect;
        for (int i = 0; i < backgrounds.Length; i++)
        {
            _uvRect = backgrounds[i].backgroundGO.GetComponent<RawImage>().uvRect;
            if (backgrounds[i].bgDirection == BackgroundDirections.Horizontal)
                _uvRect.x += _gameManager.generalSpeed * backgrounds[i].parallax * Time.deltaTime;
            else
                _uvRect.y += _gameManager.generalSpeed * backgrounds[i].parallax * Time.deltaTime;
            backgrounds[i].backgroundGO.GetComponent<RawImage>().uvRect = _uvRect;
        }
    }
}

[System.Serializable]
public class BackgroundClass
{
    public RawImage backgroundGO;
    public float parallax;
    public BackgroundDirections bgDirection;
}

public enum BackgroundDirections
{
    Vertical,
    Horizontal
}