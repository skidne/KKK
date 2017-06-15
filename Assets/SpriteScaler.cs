using UnityEngine;
using System.Collections;

public class SpriteScaler : MonoBehaviour
{
    public GameObject[] sprites;

    void Start()
    {
        int i;

        for (i = 0; i < sprites.Length; i++)
            ResizeSpriteToScreen(sprites[i]);
    }

    void ResizeSpriteToScreen(GameObject go)
    {
        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        if (sr == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        go.transform.localScale = new Vector3(worldScreenWidth, worldScreenHeight);
    }
}
