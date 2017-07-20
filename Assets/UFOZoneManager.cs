using UnityEngine;
using System.Collections;

public class UFOZoneManager : MonoBehaviour {

    public GameObject ufoGO;
    public Positions currentUfoPos;
    public float descentSpeed;
    private Vector3[] states;
    private GameManager _gameManager;

    void Start()
    {
        Transform playerino = GameObject.Find("Astro").transform;
        float moveDistance = GameObject.Find("MainCanvas").GetComponent<RectTransform>().rect.width / 3;

        _gameManager = FindObjectOfType<GameManager>();
        descentSpeed = 2;
        states = new Vector3[3];
        states[(int)Positions.Center] = playerino.localPosition;
        states[(int)Positions.LeftSide] = playerino.localPosition - new Vector3(moveDistance, 0, 0);
        states[(int)Positions.RightSide] = playerino.localPosition + new Vector3(moveDistance, 0, 0);
    }

    void Update()
    {
        ufoGO.transform.Translate(0, -_gameManager.generalSpeed * 5, 0);
    }

    IEnumerator SmoothMove(Vector3 target, float delta, int dir)
    {
        float startTime = Time.time;
        //string _animatorVar = "GoRight";
        /*
        if (dir == 1)
            _animatorVar = "GoRight";
        else
            _animatorVar = "GoLeft";*/
        //playerAnimator.SetBool(_animatorVar, true);
        while (Time.time < startTime + delta)
        {
            transform.localPosition = Vector3.Lerp(ufoGO.transform.localPosition, target, (Time.time - startTime) / delta);
            yield return null;
        }
        //playerAnimator.SetBool(_animatorVar, false);
        transform.localPosition = target;
    }

    void Move(Positions dir)
    {
        float timeDelta = .1f;
        int nextDir;

        if ((int)dir > (int)currentUfoPos)
            nextDir = 1;
        else
            nextDir = -1;
        currentUfoPos = dir;
        StartCoroutine(SmoothMove(states[(int)dir], timeDelta, nextDir));
    }
}
