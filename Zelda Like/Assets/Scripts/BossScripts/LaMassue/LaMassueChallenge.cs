using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaMassueChallenge : MonoBehaviour 
{


	public GameObject attackZone;
	public float attackRythm;
	public float attackCharge;
	private float timer;
	public GameObject player; 
	public GameObject target;
	public bool invincible;
	public int bossHP;

	private bool telegraph = false;
	public SpriteRenderer attackPreview;

	private bool onceOnly = true;


	//Var pour les vases
	private float timerVase;
	public float respawnCD;
	public GameObject vase;


	// Use this for initialization
	void Start () 
	{

		timer = attackRythm;
		
		
	}
	
	// Update is called once per frame
	void Update () 
	{

		timerVase += Time.deltaTime;
		timer += Time.deltaTime;

		//reset d'attack, le telegraph disparait.
		if (telegraph == false && timer >= attackRythm/4 && onceOnly == false)
		{
			attackPreview.color += new Color (0, 0, 0, -1f);
			onceOnly = true;
			attackPreview.GetComponent<BoxCollider2D> ().enabled = false;
		}
			
		//timer du telegraph
		if (telegraph == false && timer >= attackRythm)
		{
			AttackTelegraph ();
		}

		//timer de l'attaque
		if (telegraph == true && timer >= attackCharge)
		{
			Attack ();
		}

		//Timer de respawn de vase tous les "respawnCD" secondes.
		if (timerVase >= respawnCD)
		{
			RespawnVase ();
		}	
		if(GameObject.FindGameObjectWithTag("vase") !=null)
		{
			invincible = true;
		}
		if(GameObject.FindGameObjectWithTag("vase") ==null)
		{
			invincible = false;
		}
		
	}


	//Lance le telegraph de l'attaque
	void AttackTelegraph ()
	{
		
		attackPreview.color += new Color (0, 0, 0, 0.1f);
		timer = 0;
		telegraph = true;
		target = player.GetComponent<PlayerController> ().clickedNode;
		attackZone.transform.right = target.transform.position - attackZone.transform.position;

	}

	//Lance l'attaque
	void Attack ()
	{
		attackPreview.color += new Color (0, 0, 0, 0.9f);
		attackPreview.GetComponent<BoxCollider2D> ().enabled = true;
		timer = 0;
		telegraph = false;
		onceOnly = false;
	}


	//faire respawn le vase
	public void RespawnVase ()
	{
		attackPreview.GetComponent<BoxCollider2D> ().enabled = false;
		timerVase = 0;
		Instantiate (vase);
	}

	//Le Boss prends des dégâts (si il n'est pas invincible), si c'était son dernier PV, il meurt.
	void BossTakeDamage()
	{
		if (!invincible)
		{
			bossHP = bossHP - 1;
			print(bossHP);
			RespawnVase ();
			if (bossHP <=0)
			{
				print("gg");
			}
		}
	
	}


	//Si il est touché, il prends des dégats.
	void OnTriggerEnter2D (Collider2D other)
	{
		
		if (other.tag == "playerAttack" && player.GetComponent<PlayerController>().isAttacking )
		{
			BossTakeDamage();
		}
	}
		
}
