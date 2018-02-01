using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunController : MonoBehaviour 
{
	[Header("Controls")]
	public KeyCode dash;


	[Header("Dash Values")]
	public float simpleDashLength;
	public float chargedDashLength;
	public float dashSpeed;
	public float timeBeforeStopping;
	public float timeBeforeCharge;
	public float dashTimer;
	public float dashCoolDown;
	public float dashCoolDownValue;

	[Header("Movement Values")]
	public float moveSpeed;

	[Header("Pour la prog")]
	public Vector3 mousePos;
	public GameObject cursor;
	public bool canMove = true;
	public bool canDash = true;
	public bool isDashing = false;
	public float moveSpeedStart;
	public GameObject river;

	private Vector2 arrivalPos;
	private Rigidbody2D playerRb;
	private Vector3 normalisedDirection;
	private float distance;

//	private float riverTimer;
//	private bool riverStartTimer;
    

	void Start ()
	{
		playerRb = GetComponent<Rigidbody2D>();

        GetSpeed();
	}
		

	void Update () 
	{

        //exit
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

//		if (riverStartTimer) 
//		{
//			riverTimer += Time.deltaTime;
//		}

        // position of the cursor on the screen and angle between the player and the cursor, normalised
        mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		normalisedDirection = new Vector3 (mousePos.x - transform.position.x, mousePos.y - transform.position.y, mousePos.z - transform.position.z).normalized;
		// player movement
		distance = Vector2.Distance (transform.position, mousePos);
		if (distance >= 0.7f && canMove == true)
		{
			transform.position = Vector2.MoveTowards (transform.position, mousePos, moveSpeed * Time.deltaTime);
		}

		// player dash
		dashCoolDown -= Time.deltaTime;
		if (Input.GetKey (dash) && dashCoolDown < 0f && canDash) 
		{
			dashTimer += Time.deltaTime;
		}

		if (dashTimer > timeBeforeStopping) 
		{
			moveSpeed = 0;
		}

		if (Input.GetKeyUp (dash) && dashTimer > timeBeforeCharge && canDash)
		{
			ChargedDash ();
		}
		else if (Input.GetKeyUp (dash) && dashTimer < timeBeforeCharge && canDash)
		{
			SimpleDash ();
		}

		if (isDashing == true) {
			river.gameObject.GetComponent<Collider2D> ().enabled = false;
		}
		else if (isDashing == false && canDash) 
		{
			river.gameObject.GetComponent<Collider2D>().enabled = true;
		}


    }

	public void SimpleDash() // le dash simple est appelé, et le cool down est enclanché
	{
		StartCoroutine (DashCoroutine(simpleDashLength));
		dashTimer = 0f;
		dashCoolDown = dashCoolDownValue;
	}

	public void ChargedDash()  // le dash chargé est appelé, et le cool down est enclanché
    {
		StartCoroutine (DashCoroutine(chargedDashLength));
		dashTimer = 0f;
		dashCoolDown = dashCoolDownValue;
	}

	IEnumerator DashCoroutine(float _dashLength)
	{
		CanMove (false);
		isDashing = true;
		Vector2 _direction = new Vector2 (mousePos.x, mousePos.y) - playerRb.position; 
		playerRb.velocity = _direction.normalized * dashSpeed;
		yield return new WaitForSeconds (_dashLength);
		playerRb.velocity = Vector3.zero;
		CanMove (true);
		isDashing = false;
	}



//	public void OnCollisionEnter2D (Collision2D _other)
//	{
//		if (_other.gameObject.tag == "River" && isDashing == true) 
//		{
//			_other.gameObject.GetComponent<Collider2D>().enabled = false;
//			riverStartTimer = true;
//		}
//	}
//
//	public void OnCollisionExit2D (Collision2D _other)
//	{
//		if (_other.gameObject.tag == "River" &&  riverTimer > 0.01f )
//		{
//			_other.gameObject.GetComponent<Collider2D> ().enabled = true;
//			riverTimer = 0f;
//			riverStartTimer = false;
//		}
//	}

	public void CanMove(bool _canMove)//une fonction pour permettre au joueur de bouger ou non
	{
		if (_canMove) 
		{
            GetSpeed();
		}
		else
		{
			moveSpeed = 0;
		}
	}

    public Vector3 GetMousePos() // renvoie la valeur mouse pos
    {
        return mousePos;
    }

    public void GetSpeed()
    {
        moveSpeed = GameManager.moveSpeed;
    }
}
