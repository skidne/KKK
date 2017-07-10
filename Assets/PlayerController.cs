using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Transform player;
    private float minDragDistance;  //minimum distance for a swipe
    private float moveDistance;
    private Vector3 firstTouchPos;   //First touch position
    private Vector3 lastTouchPos;   //Last touch position
    private Positions currentPos;
    //private bool canMove;
    private Animator playerAnimator;
    private Vector3[] states;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        minDragDistance = Screen.height * 7 / 100;
        moveDistance = GameObject.Find("MainCanvas").GetComponent<RectTransform>().rect.width / 3;
        //canMove = true;
        states = new Vector3[3];
        states[(int)Positions.Center] = player.localPosition;
        states[(int)Positions.LeftSide] = player.localPosition - new Vector3(moveDistance, 0, 0);
        states[(int)Positions.RightSide] = player.localPosition + new Vector3(moveDistance, 0, 0);
        currentPos = Positions.Center;
    }

    void Update()
    {
        //Debug.Log(player.localPosition.x);
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                firstTouchPos = touch.position;
                lastTouchPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                lastTouchPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                lastTouchPos = touch.position;
                if (Mathf.Abs(lastTouchPos.x - firstTouchPos.x) > minDragDistance || Mathf.Abs(lastTouchPos.y - firstTouchPos.y) > minDragDistance)
                {
                    if (Mathf.Abs(lastTouchPos.x - firstTouchPos.x) > Mathf.Abs(lastTouchPos.y - firstTouchPos.y))
                    {
                        if ((lastTouchPos.x > firstTouchPos.x))
                        {
                            if (currentPos == Positions.LeftSide)
                                Move(Positions.Center);
                            else if (currentPos == Positions.Center)
                                Move(Positions.RightSide);
                            //player.GetComponent<Animator>().SetTrigger("GoRight");
                            Debug.Log("Right Swipe");
                        }
                        else
                        {
                            if (currentPos == Positions.RightSide)
                                Move(Positions.Center);
                            else if (currentPos == Positions.Center)
                                Move(Positions.LeftSide);
                           // player.GetComponent<Animator>().SetTrigger("GoLeft");
                            Debug.Log("Left Swipe");
                        }
                    }
                }
            }
        }
    }

    void Move(Positions dir)
    {
        float timeDelta = .1f;
        int nextDir;

        if ((int)dir > (int)currentPos)
            nextDir = 1;
        else
            nextDir = -1;
        currentPos = dir;
        StartCoroutine(SmoothMove(states[(int)dir], timeDelta, nextDir));
    }

    IEnumerator SmoothMove(Vector3 target, float delta, int dir)
    {
        float startTime = Time.time;
        string _animatorVar = "GoRight";

        if (dir == 1)
            _animatorVar = "GoRight";
        else
            _animatorVar = "GoLeft";
        playerAnimator.SetBool(_animatorVar, true);
        while (Time.time < startTime + delta)
        {
            transform.localPosition = Vector3.Lerp(player.localPosition, target, (Time.time - startTime) / delta);
            yield return null;
        }
        playerAnimator.SetBool(_animatorVar, false);
        transform.localPosition = target;
    }
}



public enum Positions
{
    LeftSide,
    Center,
    RightSide
}