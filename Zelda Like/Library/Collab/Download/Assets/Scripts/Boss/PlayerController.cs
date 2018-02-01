using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour {

	[Header ("Les Inputs")]
	public KeyCode dash;
	public KeyCode attack;

	[Header ("Variables")]
	public float dashSpeed;
	public List<GameObject> allNodes;

	[Header ("Pour la prog")]
	public Vector3 mousePos; //Position de la souris
	public LayerMask marcsLayer; //Le Layer sur lequel se trouve les Marks
	public GameObject[] possibleNodes; //Les Nodes sur lequel peut aller le joueur
	public GameObject clickedNode; 
    public GameObject cursor;
	public float clickCollider;
	public Vector2 aimPosition;
	public GameObject playerAttackZone;
	public GameObject pivotPlayerAttackZone;




	private GameObject debugClickedNode;
	private float timerAttack;
	private float chargeDash;


	public bool isDashing = false;
	public bool isAttacking = false;

    void Start ()
	{
		isAttacking = false;
		playerAttackZone.GetComponent<BoxCollider2D> ().enabled = false;
		
	}
	

	void Update ()
	{
        //exit
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

        mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition); //position actuelle de la souris
		aimPosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y)); //doublon

		pivotPlayerAttackZone.transform.position = transform.position;

		if (Input.GetKey(dash) )
		{
			chargeDash += Time.deltaTime;
		}
		if (Input.GetKeyUp(dash) )
		{
			chargeDash = 0;
		}

		//Charger son dash plus de 0.5sec permet de sauter sur des marques situées plus loin.
		if (chargeDash >= 0.5)
		{
			
		}

		//Le Dash
		if (Physics2D.OverlapCircle(mousePos, clickCollider, marcsLayer) && Input.GetKeyUp(dash) && !isDashing && !isAttacking) //on récupére l'input du dash et on verifie que t pas déja en train de dash
		{
			debugClickedNode = Physics2D.OverlapCircle(mousePos, clickCollider, marcsLayer).gameObject;
			for(int i=0; i<possibleNodes.Length; i++)
			{
				if(possibleNodes[i] == debugClickedNode)
				{
					clickedNode = debugClickedNode;
					for(int j = 0; j < allNodes.Capacity; j++)
					{
						
						allNodes[j].GetComponent<SpriteRenderer> ().color = Color.white;
					}
					isDashing = true;
					break;
				}
			}
		}

		//La frappe
			timerAttack += Time.deltaTime;
		if (Input.GetKeyDown(attack) && !isAttacking)
		{
			Attack ();
		}
		//fin de l'attaque
		if (timerAttack >= 0.5 && isAttacking  && !isDashing)
		{
			isAttacking = false;
			playerAttackZone.GetComponent<SpriteRenderer>().color += new Color (0, 0, 0, -0.5f);
			playerAttackZone.GetComponent<BoxCollider2D> ().enabled = false;
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
		if (other.gameObject == clickedNode)
		{
			isDashing = false;
			possibleNodes = other.gameObject.GetComponent<AvailableNodes> ().availableNodes;

		}
		//colore les Nodes Possibles
		for(int i=0; i<possibleNodes.Length; i++)
		{
			possibleNodes [i].GetComponent<SpriteRenderer> ().color = Color.blue;
	
		}
	}
	//Renvoi l'angle entre le player et le cursor

	//Lance l'attaque du joueur
	private void Attack()
	{
		isAttacking = true;
		playerAttackZone.GetComponent<SpriteRenderer>().color += new Color (0, 0, 0, 0.5f);
		pivotPlayerAttackZone.transform.rotation = cursor.transform.rotation;
		playerAttackZone.GetComponent<BoxCollider2D> ().enabled = true;
		timerAttack = 0;
	}
}


