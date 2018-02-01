using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBehavior : MonoBehaviour 
{

    public GameObject player;
    public GameObject rotator;
    public Animator localAnim;
    public bool hasAppeared = false;
    private Vector3 aimPosition;
    private Vector3 cursorPosition;
    public GameObject sprite;

    [Header("Target")]
    public bool hasTarget;
    public Transform targetTransform;


    private void Start()
    {
        //set normal cursor to invisible
        Cursor.visible = false;
        if(hasAppeared)
        {
            Appear();
        }
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && hasAppeared == false)
        {
            Appear();
        }
		aimPosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        //destroy cursor if player is destroyed
		if (player == null) 
		{
			Destroy (gameObject);
		}
        //get cursorPosition and go to it
		if (player != null) 
		{
			cursorPosition = new Vector3 (aimPosition.x, aimPosition.y, -3);
		} 
		else
		{
			cursorPosition = new Vector3 (0, 0, 0);
		}

        transform.position = cursorPosition;

        //rotate rotator according to position from player
		float angle = 0;
		if (player != null) 
		{
			angle = GetAngleForRotator();
		}
        rotator.transform.eulerAngles = new Vector3(0, 0, angle);

        //rotate sprite according to position from target
        if(hasTarget)
        {
            float targetAngle = GetAngleFromTarget();
            sprite.transform.eulerAngles = new Vector3(0, 0, targetAngle);
        }
    }

    public void Appear()
    {
        localAnim.SetTrigger("appear");
        hasAppeared = true;
        if(hasTarget)
        {
            localAnim.SetBool("isRound", false);
        }
        else
        {
            localAnim.SetBool("isRound", true);
        }
    }

    public float GetAngleForRotator()
    {
        //get angle from "up" line
        Vector2 toAngle = transform.position - player.transform.position;
        float angleToCursor = Vector2.Angle(player.transform.up, toAngle);

        //if behind player, get angle from "down" line and add the full "up" angle
        if (transform.position.x < player.transform.position.x)
            angleToCursor = Vector2.Angle(Vector3.down, toAngle) + 180;

        //reverse angle
        angleToCursor = angleToCursor * -1;

        return angleToCursor;
    }

    public float GetAngleFromTarget()
    {
        //get angle from "up" line
        Vector2 toAngle = transform.position - targetTransform.position;
        float angleToCursor = Vector2.Angle(targetTransform.up, toAngle);

        //if behind player, get angle from "down" line and add the full "up" angle
        if (transform.position.x < targetTransform.position.x)
            angleToCursor = Vector2.Angle(Vector3.down, toAngle) + 180;

        //reverse angle
        angleToCursor = angleToCursor * -1;

        return angleToCursor;
    }
}
