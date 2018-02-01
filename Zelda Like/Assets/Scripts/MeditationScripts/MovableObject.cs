using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectState
{
    dropping,
    held,
    hovering,
    still,
    floating
}

public class MovableObject : MonoBehaviour {

    public ObjectState state;
    public Camera cam;
    public Rigidbody2D localBody;
    public ObjectsManager objectsManager;

    [Header("Speed Settings")]
    public float rotatingRate;
    public float droppingRate;
    public Vector3 floatingVector;

    [Header("Clickable Box Settings")]
    public ClickableBox clickableBox;

    [Header("Lights Settings")]
    public int lightIndex;
    public RiddleManager riddleManager;

    private void Start()
    {
        clickableBox = GetComponent<ClickableBox>();
        state = ObjectState.dropping;
        localBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //check Input and change state
        if (clickableBox.gives1 && objectsManager.clicked)
        {
            if ((state == ObjectState.still || state == ObjectState.floating) && objectsManager.activeMovableObject == null)
            {
                state = ObjectState.held;
                objectsManager.activeMovableObject = gameObject;
            }
            else if (state == ObjectState.held)
            {
                state = ObjectState.hovering;
                objectsManager.activeMovableObject = null;
            }
            else if (state == ObjectState.hovering)
            {
                state = ObjectState.dropping;
            }
        }
    }

    void FixedUpdate()
    {
        //applies behavior depending on state
        if(state == ObjectState.held)
        {
            riddleManager.Ignite(lightIndex);
            localBody.velocity = Vector3.zero;
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + rotatingRate);
            Vector3 newPosition = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 1.2f);
        }
        if(state == ObjectState.hovering)
        {
            if(riddleManager.state == RiddleState.active)
            {
                riddleManager.Extinguish(lightIndex);
            }
            localBody.velocity = Vector3.zero;
            transform.position = transform.position;
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + rotatingRate);
        }
        if(state == ObjectState.dropping)
        {
            localBody.velocity = Vector3.down * droppingRate;
        }
        if(state == ObjectState.still)
        {
            localBody.velocity = Vector3.zero;
            transform.position = transform.position;
        }
        if(state == ObjectState.floating)
        {
            localBody.velocity = floatingVector;
        }
    }
}
