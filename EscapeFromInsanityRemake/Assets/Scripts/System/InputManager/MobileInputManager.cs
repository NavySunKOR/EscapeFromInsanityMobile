using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class MobileInputManager : MonoBehaviour {

    
    public float horizontal;
    public float vertical;
    public float viewHorizontal;
    public float viewVertical;
    public bool isAim;


    public RectTransform movementButtonTr;
    private const float MAX_VERTICAL_VALUE = 1f;
    private const float MIN_VERTICAL_VALUE = -0.5f;
    private const float MAX_HORIZONTAL_VALUE = 0.5f;
    private const float MIN_HORIZONTAL_VALUE = -0.5f;
    private bool IsMoving;
    private Vector2 movementTouchAxisPos;
    private Vector2 movementPos;
    private Rect movementRect;
    private Vector2 axisVector;
    private Vector2 rotateCenterAxis;

    // Use this for initialization
    private void Awake ()
    {
        if(movementButtonTr != null)
            movementTouchAxisPos = movementButtonTr.position;
        //movement
        movementPos = movementTouchAxisPos;
        horizontal = 0f;
        vertical = 0f;

        //rotate
        viewHorizontal = 0f;
        viewVertical = 0f;
        rotateCenterAxis = new Vector2(Screen.width / 2f, Screen.height / 2f);

        //aim
        isAim = false;

    }

    private void Update()
    {
        if(movementButtonTr != null)
            movementButtonTr.position = movementPos;

        SetMovementAxis();
        SetRotationAxis();
    }

    private void SetMovementAxis()
    {
        axisVector = movementPos - movementTouchAxisPos;
        if (IsRectSet())
        {
            horizontal = axisVector.x / (movementRect.width / 2f);
            vertical = axisVector.y / (movementRect.height / 2f);

            //set speed depends on aim status.
            if(!isAim)
            {
                horizontal = Mathf.Clamp(horizontal, -0.5f, 0.5f);
                vertical = Mathf.Clamp(vertical, -0.5f, 1f);
            }
            else
            {
                horizontal = Mathf.Clamp(horizontal, -0.25f, 0.25f);
                vertical = Mathf.Clamp(vertical, -0.25f, 0.5f);
            }
        }
    }

    private void SetRotationAxis()
    {
        if(Input.touchCount > 0)
        {
            if(Input.touchCount>= 2)
            {
                Touch touch = Input.GetTouch(1);
                if(touch.position.x > Screen.width /2f)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        rotateCenterAxis.x = touch.position.x;
                        rotateCenterAxis.y = touch.position.y;
                    }
                    else if (touch.phase == TouchPhase.Moved)
                    {
                        if (!movementRect.Contains(touch.position))
                        {
                            //rotation horizontal max boundary width is 20% of right screen.
                            viewHorizontal = (touch.position.x - rotateCenterAxis.x) / (Screen.width / 5f);
                            viewHorizontal = Mathf.Clamp(viewHorizontal, -1f, 1f);
                            //rotation vertical max boundary height is 20% of right screen.
                            viewVertical = (touch.position.y - rotateCenterAxis.y) / (Screen.height / 5f);
                            viewVertical = Mathf.Clamp(viewVertical, -1f, 1f);
                        }
                        else
                        {
                            viewHorizontal = 0f;
                            viewVertical = 0f;
                        }
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        viewHorizontal = 0f;
                        viewVertical = 0f;
                    }
                }
            }
            else
            {
                Touch touch = Input.GetTouch(0);
                if (touch.position.x > Screen.width / 2f)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        rotateCenterAxis.x = touch.position.x;
                        rotateCenterAxis.y = touch.position.y;
                    }
                    else if (touch.phase == TouchPhase.Moved)
                    {
                        if (!movementRect.Contains(touch.position))
                        {
                            viewHorizontal = (touch.position.x - rotateCenterAxis.x) / (Screen.width / 5f);
                            viewVertical = (touch.position.y - rotateCenterAxis.y)/ (Screen.height / 5f);
                            viewHorizontal = Mathf.Clamp(viewHorizontal, MIN_VERTICAL_VALUE, MAX_VERTICAL_VALUE);
                            viewVertical = Mathf.Clamp(viewVertical, MIN_HORIZONTAL_VALUE, MAX_HORIZONTAL_VALUE);
                        }
                        else
                        {
                            viewHorizontal = 0f;
                            viewVertical = 0f;
                        }
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        viewHorizontal = 0f;
                        viewVertical = 0f;
                    }
                }
            }
        }
        else
        {
            viewHorizontal = 0f;
            viewVertical = 0f;
        }
    }

    public void SetPosition(Vector2 pos)
    {
        movementPos = pos;
    }

    public void SetPosition(Vector2 pos,RectTransform rectTransform)
    {
        if (!IsRectSet())
        {
            movementRect = rectTransform.rect;
            movementRect.x += movementRect.width / 2f;
            movementRect.y += movementRect.height / 2f;
        }

        if (movementRect.Contains(pos))
            movementPos = pos;
    }

    public void ResetPosition()
    {
        movementPos = movementTouchAxisPos;
    }
    

    private bool IsRectSet()
    {
        return movementRect.width > 0f;
    }

    public void SetAim()
    {
        isAim = !isAim;
    }
}
