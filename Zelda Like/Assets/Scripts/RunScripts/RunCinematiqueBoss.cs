using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunCinematiqueBoss : MonoBehaviour 
{
	public float wakingSpeed;
	public GameObject boss;
	void Start () 
	{
		
	}

	void Update () 
	{
		
	}

	public void OnTriggerEnter2D(Collider2D _other)
	{
		if (_other.tag == "Player") 
		{
			_other.GetComponent<RunController> ().CanMove (false);
			_other.GetComponent<RunPlayerDamage>().canAttack = false;
			_other.GetComponent<RunController>().canDash = false;

			StartCoroutine (ApparitionDuBoss (_other));
		}
	}

	IEnumerator ApparitionDuBoss(Collider2D _other)
	{
		boss.SetActive(true);
		yield return new WaitForSeconds (5f);
		_other.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * wakingSpeed);
	}
}
