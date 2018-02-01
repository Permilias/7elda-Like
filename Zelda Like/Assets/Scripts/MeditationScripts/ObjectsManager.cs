using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour {

    public RiddleManager riddleManager;

    [Header("Movable Objects")]
    public GameObject[] movableObjectsArray;
    public GameObject activeMovableObject;

    [Header("Detection")]
    public bool clicked = false;
    public bool mouseHoversAnyBox = false;
    public int hoverCount;

    void Update () {


        //get Input
        if(Input.GetMouseButtonDown(0) && riddleManager.state == RiddleState.active)
        {
            clicked = true;
        }	
        else
        {
            clicked = false;
        }

        //mouse hovers a box if hoverCount isn't zero
        if (hoverCount == 0)
        {
            mouseHoversAnyBox = false;
        }
        else
        {
            mouseHoversAnyBox = true;
        }

        //drop all objects if clicked while not hovering
        if(clicked && !mouseHoversAnyBox)
        {
            for(int i = 0; i < movableObjectsArray.Length; i++)
            {
                if(movableObjectsArray[i].GetComponent<MovableObject>().state == ObjectState.hovering)
                {
                    movableObjectsArray[i].GetComponent<MovableObject>().state = ObjectState.dropping;
                }
            }
        }
    }
}
