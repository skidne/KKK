using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackgroundController : MonoBehaviour {

    public float generalSpeed;
    public BackgroundClass[] backgrounds;

    void Update()
    {
        Rect _uvRect;
        for (int i = 0; i < backgrounds.Length; i++)
        {
            _uvRect = backgrounds[i].backgroundGO.GetComponent<RawImage>().uvRect;
            if (backgrounds[i].bgDirection == BackgroundDirections.Horizontal)
                _uvRect.x += generalSpeed * backgrounds[i].parallax * Time.deltaTime;
            else
                _uvRect.y += generalSpeed * backgrounds[i].parallax * Time.deltaTime;
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