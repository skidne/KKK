using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

    public float generalSpeed;
    public BackgroundClass[] backgrounds;

    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            Vector3 newOffset = backgrounds[i].backgroundGO.GetComponent<MeshRenderer>().material.mainTextureOffset;
            if (backgrounds[i].bgDirection == BackgroundDirections.Horizontal)
                newOffset.x -= generalSpeed * backgrounds[i].parallax * Time.deltaTime;
            else
                newOffset.y -= generalSpeed * backgrounds[i].parallax * Time.deltaTime;
            backgrounds[i].backgroundGO.GetComponent<MeshRenderer>().material.mainTextureOffset = newOffset;
        }
    }
}

[System.Serializable]
public class BackgroundClass
{
    public Transform backgroundGO;
    public float parallax;
    public BackgroundDirections bgDirection;
}

public enum BackgroundDirections
{
    Vertical,
    Horizontal
}