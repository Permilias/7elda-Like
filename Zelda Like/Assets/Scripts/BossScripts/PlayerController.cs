using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum BossState {BossAlive,BossDead};

public class PlayerController : MonoBehaviour {

	[Header ("Les Inputs")]
	public KeyCode dash;
	public KeyCode attack;

	[Header ("Variables")]
	public float dashSpeed;
	public List<GameObject> allNodes;
	public float attackSpeed;

	[Header ("Pour la prog")]
	public Vector3 mousePos; //Position de la souris
	public LayerMask marcsLayer; //Le Layer sur lequel se trouve les Marks
	public GameObject[] possibleNodes; //Les Nodes sur lequel peut aller le joueur
	public GameObject clickedNode; 
    public GameObject cursor;
	public float clickCollider;
	public Vector2 aimPosition;
	public GameObject playerAttackZone;
	public GameObject playerAoeZone;
	public GameObject pivotPlayerAttackZone;

	private GameObject debugClickedNode;
	private float timerAttack;
	private float chargeDash;
	private float isDashingTooLong;
	private bool startColorizing = true;
	private bool hasChargedDash = false; //Vrai si le joueur a chargé son dash.
	private bool onceOnly;
	private float attackCharge;

	//La Boule de Feu
	public bool isStoringFire;
	public GameObject fireBall;


	public bool hasAtk = false;
	public bool isDashing = false;
	public bool isAttacking = false;
	public GameObject Exit;
	public Rigidbody2D rb;

	public BossState bossState;



    void Start ()
	{
		rb = GetComponent<Rigidbody2D>();

	}
	

	void Update ()
	{
		//Verification du Dash
		for(int i=0; i<possibleNodes.Length; i++)
		{
			if(possibleNodes[i] == debugClickedNode)  
			{
				clickedNode = debugClickedNode;
				isDashingTooLong = 0;
				isDashing = true;
				break;
			}
		}

		//Tous les Nodes deviennent blancs
		for(int j = 0; j < allNodes.Capacity; j++)   
		{

			allNodes[j].GetComponent<AvailableNodes> ().isDisponible = false;
		}

		//Les Nodes Disponibles sont éclairés.
		for (int i = 0; i < possibleNodes.Length; i++) {
			possibleNodes [i].GetComponent<AvailableNodes> ().isDisponible = true;

		}

		if (isDashing)
		{
			isDashingTooLong += Time.deltaTime; //Debug
		}


		if (isDashingTooLong>= 0.3f && isDashing)
		{
			isDashingTooLong = 0;
			isDashing = false;
		}


		//Le joueur se rapproche du cadavre du boss
		if (bossState == BossState.BossDead)
		{
			transform.position = Vector2.Lerp (transform.position, Exit.transform.position, 0.05f);

		}

        //exit
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

        mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition); //position actuelle de la souris
		aimPosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y)); //doublon

		pivotPlayerAttackZone.transform.position = transform.position;
		playerAoeZone.transform.position = transform.position;

		//Compte le temps du dash chargé
		if (Input.GetKey(dash) )
		{
			chargeDash += Time.deltaTime;
		}

		//reset la charge du dash si il relache le click
		if (Input.GetKeyUp(dash) )
		{
			chargeDash = 0;
		}

		//
	

		//Charger son dash plus de 0.5sec permet de sauter sur des marques situées plus loin.
		if (chargeDash >= 0.5)
		{
			hasChargedDash = true;
		}

		if (hasChargedDash)
		{
			possibleNodes = clickedNode.gameObject.GetComponent<AvailableNodes> ().availableNodesExtanded;

		}
		if (!hasChargedDash && onceOnly)
		{
			onceOnly = false;
			possibleNodes = clickedNode.gameObject.GetComponent<AvailableNodes> ().availableNodes;
		}

		//Le Dash
		if (Physics2D.OverlapCircle(mousePos, clickCollider, marcsLayer) && Input.GetKeyUp(dash) && !isDashing && !isAttacking && bossState != BossState.BossDead) //on récupére l'input du dash et on verifie que t pas déja en train de dash
		{
			debugClickedNode = Physics2D.OverlapCircle(mousePos, clickCollider, marcsLayer).gameObject;
		}

		//La frappe
			timerAttack += Time.deltaTime;


		//Boule De Feu Input
		if (Input.GetKeyDown(attack) && !isAttacking && isStoringFire)
		{
				SummonFireBall ();
		}

		if (Input.GetKey(attack) && !isAttacking && !isStoringFire)
		{
			attackCharge += Time.deltaTime;

		}


		if (Input.GetKeyUp(attack) && !isAttacking && !isStoringFire)
		{
			if (attackCharge <= 0.5) 
			{
				Attack ();
			}
			if (attackCharge >= 0.5) 
			{
				AttackAOE ();
			}
			attackCharge = 0;


		}


		//fin de l'attaque
		if (timerAttack >= attackSpeed && isAttacking  && !isDashing)
		{
			isAttacking = false;
			playerAttackZone.GetComponent<SpriteRenderer>().color += new Color (0, 0, 0, -0.5f);
			playerAttackZone.GetComponent<BoxCollider2D> ().enabled = false;
			playerAoeZone.GetComponent<SpriteRenderer>().color += new Color (0, 0, 0, -0.5f);
			playerAoeZone.GetComponent<BoxCollider2D> ().enabled = false;
		}


        
    }

	void FixedUpdate ()
	{
		if (isDashing)
		{
			Dash ();
		}
			

	

	}
		
	//Le Dash
	private void Dash()
	{
		transform.position = Vector2.MoveTowards (transform.position, clickedNode.transform.position, dashSpeed * Time.deltaTime);

	}

	//Fin du Dash
	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject == clickedNode)  //Si il touche le Node ciblé
		{
			possibleNodes = other.gameObject.GetComponent<AvailableNodes> ().availableNodes; //les Possibles Nodes deviennent les Nodes possibles.
		}



			
	}

	public void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject == clickedNode)  //Si il touche le Node ciblé
		{
			isDashing = false;
			hasChargedDash = false;
			onceOnly = true;


		}

	}




	//Lance l'attaque du joueur
	private void Attack()
	{
		hasAtk = false;
		isAttacking = true;
		playerAttackZone.transform.Rotate (Vector3.down*0.1f);
		playerAttackZone.transform.Rotate (Vector3.right * -0.1f);
		playerAttackZone.GetComponent<SpriteRenderer>().color += new Color (0, 0, 0, 0.5f);
		pivotPlayerAttackZone.transform.rotation = cursor.transform.rotation;
		playerAttackZone.GetComponent<BoxCollider2D> ().enabled = true;
		timerAttack = 0;
	}

	private void AttackAOE()
	{
		hasAtk = false;
		isAttacking = true;
		playerAoeZone.GetComponent<SpriteRenderer>().color += new Color (0, 0, 0, 0.5f);
		playerAttackZone.GetComponent<BoxCollider2D> ().enabled = true;
		timerAttack = 0;
	}

	private void SummonFireBall ()
	{
		isStoringFire = false;
		Instantiate (fireBall, transform.position, cursor.transform.rotation* Quaternion.Euler (0, 0, 90));
	}



}


