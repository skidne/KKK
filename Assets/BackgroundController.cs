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

        for (int i = 0; i < backgrounds.Length; i++)
        {
            Vector2 offset;
            offset = backgrounds[i].backgroundGO.material.GetTextureOffset("_MainTex");
            if (backgrounds[i].bgDirection == BackgroundDirections.Horizontal)
                offset.x += _gameManager.generalSpeed * backgrounds[i].parallax * Time.deltaTime;
            else
                offset.y -= _gameManager.generalSpeed * backgrounds[i].parallax * Time.deltaTime;
            backgrounds[i].backgroundGO.material.SetTextureOffset("_MainTex", offset);
        }
    }
}

[System.Serializable]
public class BackgroundClass
{
    public Renderer backgroundGO;
    public float parallax;
    public BackgroundDirections bgDirection;
}

public enum BackgroundDirections
{
    Vertical,
    Horizontal
}