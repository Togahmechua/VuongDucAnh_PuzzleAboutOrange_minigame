using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private OrangePieceController orangePiece;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float minSwipeDistance = 50f;
    private bool isSwiping = false;

    public static event Action CheckWin;

    private void Start()
    {
        if (orangePiece != null)
        {
            return;
        }

        orangePiece = GetComponent<OrangePieceController>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
                isSwiping = true;
            }

            if (touch.phase == TouchPhase.Ended && isSwiping)
            {
                isSwiping = false;
                endTouchPosition = touch.position;

                Vector2 inputVector = endTouchPosition - startTouchPosition;

                if (inputVector.magnitude < minSwipeDistance) return;

                if (Mathf.Abs(inputVector.x) > Mathf.Abs(inputVector.y))
                {
                    if (inputVector.x > 0)
                        RightSwipe();
                    else
                        LeftSwipe();
                }
                else
                {
                    if (inputVector.y > 0)
                        UpSwipe();
                    else
                        DownSwipe();
                }

                CheckWin?.Invoke();
            }
        }
    }

    private void DownSwipe()
    {
        print("down");
        orangePiece.Move(Vector2.down);
    }

    private void UpSwipe()
    {
        print("up");
        orangePiece.Move(Vector2.up);
    }

    private void LeftSwipe()
    {
        print("left");
        orangePiece.Move(Vector2.left);
    }
    private void RightSwipe()
    {
        print("right");
        orangePiece.Move(Vector2.right);
    }
}
