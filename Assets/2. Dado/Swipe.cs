using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDraging = false;
    public bool isDiagonal = true;
    private Vector2 startTouch, swipeDelta;


    private void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        #region STANDALONE INPUTS
        if (Input.GetMouseButtonDown(0))
        {
            isDraging = true;
            tap = true;
            startTouch = Input.mousePosition;
        } else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            Reset();
        }
        #endregion

        #region MOBILE INPUTS
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isDraging = true;
                tap = true;
                startTouch = Input.touches[0].position;

            } else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }
        }
        #endregion

        //Calculate the distance
        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            } else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        //Did we cross the deadzone(?)
        if (swipeDelta.magnitude > 75)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (!isDiagonal)
            {
                #region Standard SWIPE DIRECTION CALCULATOR
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {

                    //Left OR Right
                    if (x < 0)
                    {
                        Debug.Log("Swipe Left");
                        swipeLeft = true;
                    }
                    else
                    {
                        Debug.Log("Swipe Right");
                        swipeRight = true;
                    }
                }
                else
                {
                    //Up OR Down
                    if (y < 0)
                    {
                        Debug.Log("Swipe Up");
                        swipeDown = true;
                    }
                    else
                    {
                        Debug.Log("Swipe Down");
                        swipeUp = true;
                    }
                }
                #endregion
            }
            else
            {
                #region DIAGONAL SWIPE DIRECTION CALCULATOR
                if (x > 0)
                {

                    //Down OR Right
                    if (y > 0)
                    {
                        Debug.Log("Swipe Right");
                        swipeRight = true;
                    }
                    else
                    {
                        Debug.Log("Swipe Down");
                        swipeDown = true;
                    }
                }
                else
                {
                    //Up OR Left
                    if (y < 0)
                    {
                        Debug.Log("Swipe Left");
                        swipeLeft = true;
                    }
                    else
                    {
                        Debug.Log("Swipe Up");
                        swipeUp = true;
                    }
                }
            }
            #endregion
            }
        }

        public void Reset()
        {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
        }


    public Vector2 StartTouch { get => StartTouch; set => StartTouch = value; }
    public Vector2 SwipeDelta { get => swipeDelta; set => swipeDelta = value; }
    public bool Tap { get => tap; set => tap = value; }
    public bool SwipeLeft { get => swipeLeft; set => swipeLeft = value; }
    public bool SwipeRight { get => swipeRight; set => swipeRight = value; }
    public bool SwipeUp { get => swipeUp; set => swipeUp = value; }
    public bool SwipeDown { get => swipeDown; set => swipeDown = value; }
}
