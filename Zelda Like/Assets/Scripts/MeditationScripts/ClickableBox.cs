using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableBox : MonoBehaviour
{
    [Header("Initial Settings")]
    public float leftOffset;
    public float rightOffset;
    public float downOffset;
    public float upOffset;

    [Header("Current Position")]
    public float leftCoordinate;
    public float rightCoordinate;
    public float downCoordinate;
    public float upCoordinate;

    private Vector3 currentMousePos;
    public Camera cam;

    public Transform parentTransform;
    public ObjectsManager objectsManager;

    public bool gives1 = false;

    private void Update()
    {
        //change box position depending on object position
        leftCoordinate = parentTransform.position.x - leftOffset;
        rightCoordinate = parentTransform.position.x + rightOffset;
        downCoordinate = parentTransform.position.y - downOffset;
        upCoordinate = parentTransform.position.y + upOffset;

        if(mouseIsInBox() && !gives1)
        {
            objectsManager.hoverCount += 1;
            gives1 = true;
        }
        else if(!mouseIsInBox() && gives1)
        {
            objectsManager.hoverCount -= 1;
            gives1 = false;
        }
    }

    public bool mouseIsInBox()
    {
        //returns true if mouse is inside the box
        Vector2 _mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        if(_mousePosition.x > leftCoordinate && _mousePosition.x < rightCoordinate && _mousePosition.y > downCoordinate && _mousePosition.y < upCoordinate)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
