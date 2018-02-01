using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivesStill : MonoBehaviour {

    public bool rejectsObjects;
    public bool isRejecting;
    public bool detectsObject;
    public float rejectionDelay;
    public float floatAwayDelay;
    public GameObject detectedObject;
    public RiddleManager riddleManager;
    public ObjectsManager objectsManager;
    public int lightIndex;
    public int objectIndex;

    private void OnTriggerStay2D(Collider2D collision)
    {
        //stops object
        if(collision.tag == "MeditationObject")
        {
            if(collision.GetComponent<MovableObject>().state != ObjectState.held)
            {
                //indicate object detection, store object
                detectsObject = true;
                if (!isRejecting)
                {
                    detectedObject = collision.gameObject;
                }

                if (collision.GetComponent<MovableObject>().state == ObjectState.dropping || collision.GetComponent<MovableObject>().state == ObjectState.hovering)
                {
                    collision.GetComponent<MovableObject>().state = ObjectState.still;

                }

                if (rejectsObjects && !isRejecting && collision.GetComponent<MovableObject>().state != ObjectState.held)
                {
                    StartCoroutine("RejectObject");
                }
            }
        }
    }

    private IEnumerator RejectObject()
    {
        isRejecting = true;
        if (objectsManager.movableObjectsArray[objectIndex] == detectedObject)
        {
            riddleManager.Ignite(lightIndex);
        }
        yield return new WaitForSeconds(rejectionDelay);
        riddleManager.Extinguish(lightIndex);
        if (detectedObject.GetComponent<MovableObject>().state == ObjectState.still)
        {
            detectedObject.GetComponent<MovableObject>().floatingVector = Vector3.up;
            detectedObject.GetComponent<MovableObject>().state = ObjectState.floating;
        }
        yield return new WaitForSeconds(floatAwayDelay);
        if(detectedObject.GetComponent<MovableObject>().state == ObjectState.floating)
        {
            detectedObject.GetComponent<MovableObject>().state = ObjectState.hovering;
        }
        detectedObject = null;
        isRejecting = false;
        StartCoroutine("BlinkCollider");

    }

    private IEnumerator BlinkCollider()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForFixedUpdate();
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
