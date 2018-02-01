using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaPelleScript : MonoBehaviour {

	// Ce Script est un archétype, au moment de créer un nouveau Boss, Copier/Coller ce script est recommandé
	//Veillez a ne pas coder un boss en particulier dans ce script, mais à rester le plus global possible.




	[Header ("Quelques variables Générales")]
	public int attackNB; //Le Type de Sa prochaine attaque (1 pour attack1, 2 pour attack2, etc.)
	public bool invincible = false; //Tant que c'est faux, le boss ne peut pas pendre de dégâts.
	public float bossHP; //Les points de vie du boss, si il arrive à 0, c'est la fin du combat.
	public GameObject target;

	[Header ("Attack 1")]
	//Pour Attack1 (zone distance)
	public float attack1Rythm; //Le temps en seconde, entre la fin d'une attaque et le début du télégraph de la suivante
	public float attack1Charge; //Le temps, en seconde, entre le début du télégraph et le moment où l'attaque prend effet.
	public GameObject attack1Zone; //La Hitbox de l'attaque, pour la modifier, il suffit de modifier le gameobject
	public GameObject attack1Pivot; //Le Pivot de l'attaque. Lorsqu'on veut déplacer l'attaque, on déplace son pivot.
	public GameObject Trig1;
	public GameObject Trig2;

	[Header ("Attack 2")]
	//Pour Attack2 (zone carré)
	public float attack2Rythm; //Le temps en seconde, entre la fin d'une attaque et le début du télégraph de la suivante
	public float attack2Charge; //Le temps, en seconde, entre le début du télégraph et le moment où l'attaque prend effet.
	public GameObject attack2Zone; //La Hitbox de l'attaque, pour la modifier, il suffit de modifier le gameobject
	public GameObject attack2Pivot; //Le Pivot de l'attaque. Lorsqu'on veut déplacer l'attaque, on déplace son pivot.




	[Header ("Attack 3")]
	//Pour Attack3 (zone pelle)
	public float attack3Rythm; //Le temps en seconde, entre la fin d'une attaque et le début du télégraph de la suivante
	public float attack3Charge; //Le temps, en seconde, entre le début du télégraph et le moment où l'attaque prend effet.
	public GameObject attack3Zone; //La Hitbox de l'attaque, pour la modifier, il suffit de modifier le gameobject
	public GameObject attack3Pivot; //Le Pivot de l'attaque. Lorsqu'on veut déplacer l'attaque, on déplace son pivot.
	public GameObject target1;
	public GameObject target2;
	public GameObject Trig3;
	public GameObject Trig4;



	//Qq autres variables
	private bool tgOn = false; //Vérifie que le télégraph a bien eu lieu, si c'est le cas, il lance l'attaque.
	private float timer; //Le Timer
	private bool onceOnly = true; //Le Reset d'attaque n'a lieu qu'une seule fois


	private float fightStart = 0; // a 0, le combat n'a pas commencé


	private Color tmp;
	public GameObject player;
	private int attackCount = 0;

	void Start () {

		Color tmp = attack1Zone.GetComponent<SpriteRenderer>().color;
		tmp.a = 0f;
		attack1Zone.GetComponent<SpriteRenderer>().color = tmp;
		attack2Zone.GetComponent<SpriteRenderer>().color = tmp;
		attack3Zone.GetComponent<SpriteRenderer>().color = tmp;
		AttackReset ();
	}

	void Update () {

		if (fightStart == 0 && player.GetComponent<PlayerController>().isDashing == true)   //Lance le combat lors du 1er dash 
		{
			fightStart = 0.5f;
		}
		if (fightStart == 0.5f && player.GetComponent<PlayerController>().isAttacking == true)   //Lance le combat lors du 1er dash 
		{
			fightStart = 1;
		}


		if (fightStart == 1 && target.GetComponent<PlayerController>().bossState != BossState.BossDead)  //Vrai lorsque le combat est en train d'avoir lieu.
		{
			timer += Time.deltaTime;
		}

		if (attackNB == 0)
		{
			if (target.GetComponent<PlayerController>().clickedNode == Trig1 || target.GetComponent<PlayerController>().clickedNode == Trig2)
			{
				attackNB = 1;
				attackCount = attackCount + 1;
			} 
			else 
			{
				if (attackCount <= 4) 
				{
					attackNB = 3;
					attackCount = attackCount +1;
					
				} 
				else 
				{
					attackCount = 0;
					attackNB = 2;
				}
			}
		}
			




		//Si sa prochaine attack doit être de type 1
		if (attackNB == 1)
		{
			if (timer >= attack1Rythm/5 && !tgOn && onceOnly) //Met fin à l'attaque.
			{
				onceOnly = false;
				AttackReset ();
			}

			if (timer >= attack1Rythm && !tgOn) //Lance le télégraph (TG) de l'attaque 1, si il n'a pas deja eu lieu
			{
				
				Attack1TG ();
			}
			if (timer >= attack1Charge && tgOn)  //Lance l'attaque 1, si le TG a deja eu lieu
			{
				
				Attack1 ();
			}

		}

		//Si sa prochaine attack doit être de type 2
		if (attackNB == 2)
		{
			if (timer >= attack2Rythm/5 && !tgOn && onceOnly) //Met fin à l'attaque.
			{
				onceOnly = false;
				AttackReset ();
			}

			if (timer >= attack2Rythm && !tgOn) //Lance le télégraph (TG) de l'attaque 2, si il n'a pas deja eu lieu
			{
				
				Attack2TG ();
			}
			if (timer >= attack2Charge && tgOn)  //Lance l'attaque 2, si le TG a deja eu lieu
			{
				
				Attack2 ();
			}

		}

		//Si sa prochaine attack doit être de type 3
		if (attackNB == 3)
		{
			if (timer >= attack3Rythm/5 && !tgOn && onceOnly) //Met fin à l'attaque.
			{
				onceOnly = false;
				AttackReset ();
			}

			if (timer >= attack3Rythm && !tgOn) //Lance le télégraph (TG) de l'attaque 3, si il n'a pas deja eu lieu
			{

				Attack3TG ();
			}
			if (timer >= attack3Charge && tgOn)  //Lance l'attaque 3, si le TG a deja eu lieu
			{

				Attack3 ();
			}

		}





	}




	//Les Fonctions de l'Attaque 1
	void Attack1TG ()  //Lance Le TG de l'attaque
	{
		tmp.a = 0.3f;
		attack1Zone.GetComponent<SpriteRenderer>().color = tmp;
		 //Provisoire : Indication visuelle du TG
		timer = 0; //reset le timer
		tgOn = true; //Le Télégraph a bel et bien eu lieu, l'attack est prete à être lancé !!
		attack1Pivot.transform.right = target.transform.position -attack1Pivot.transform.position;

	}

	void Attack1 ()  //Lance l'Attaque
	{
		tmp.a = 1f;
		attack1Zone.GetComponent<SpriteRenderer>().color = tmp;
		 //Provisoire : Indication visuelle de l'attaque
		attack1Zone.GetComponent<BoxCollider2D> ().enabled = true; //La Zone d'ataqu est activée.
		timer = 0; //reset le timer
		tgOn = false; //L'attaque a bel et bien eu lieu.
		onceOnly = true; //permet de reset l'attaque.
		attackNB = 0;
	}





	//les Fonctions de l'Attaque 2
	void Attack2TG ()  //Lance Le TG de l'attaque
	{
		tmp.a = 0.3f;
		attack2Zone.GetComponent<SpriteRenderer>().color = tmp;
		 //Provisoire : Indication visuelle du TG
		timer = 0; //reset le timer
		tgOn = true; //Le Télégraph a bel et bien eu lieu, l'attack est prete à être lancé !!
		//Intégrez ici la position de l'attaque
	}

	void Attack2 ()  //Lance l'Attaque
	{
		tmp.a = 1f;
		attack2Zone.GetComponent<SpriteRenderer>().color = tmp;
		 //Provisoire : Indication visuelle de l'attaque
		attack2Zone.GetComponent<BoxCollider2D> ().enabled = true; //La Zone d'ataqu est activée.
		timer = 0; //reset le timer
		tgOn = false; //L'attaque a bel et bien eu lieu.
		onceOnly = true; //permet de reset l'attaque.
		attackNB = 0;
	}

	//les Fonctions de l'Attaque 3
	void Attack3TG ()  //Lance Le TG de l'attaque
	{
		tmp.a = 0.3f;
		attack3Zone.GetComponent<SpriteRenderer>().color = tmp;
		//Provisoire : Indication visuelle du TG
		timer = 0; //reset le timer
		tgOn = true; //Le Télégraph a bel et bien eu lieu, l'attack est prete à être lancé !!
		int _randomiser = Random.Range (1, 3);

		if (target.GetComponent<PlayerController>().clickedNode == Trig3)
		{
			
			attack3Pivot.transform.right = target2.transform.position - attack3Pivot.transform.position;
	
		}
		else if (target.GetComponent<PlayerController>().clickedNode == Trig4)
			{
			attack3Pivot.transform.right = target1.transform.position - attack3Pivot.transform.position;

			}
			else if (_randomiser == 1) 
			{
			attack3Pivot.transform.right = target1.transform.position - attack3Pivot.transform.position;

			}
			else
			{
			attack3Pivot.transform.right = target2.transform.position - attack3Pivot.transform.position;
			}

	}

	void Attack3 ()  //Lance l'Attaque
	{
		tmp.a = 1f;
		attack3Zone.GetComponent<SpriteRenderer>().color = tmp;
		//Provisoire : Indication visuelle de l'attaque
		attack3Zone.GetComponent<BoxCollider2D> ().enabled = true; //La Zone d'ataqu est activée.
		timer = 0; //reset le timer
		tgOn = false; //L'attaque a bel et bien eu lieu.
		onceOnly = true; //permet de reset l'attaque.
		attackNB = 0;
	}



	void AttackReset () //Reset l'Attaque.
	{
		tmp.a = 0f;
		attack2Zone.GetComponent<SpriteRenderer>().color = tmp;
		attack2Zone.GetComponent<BoxCollider2D> ().enabled = false;
		attack1Zone.GetComponent<BoxCollider2D> ().enabled = false;
		attack1Zone.GetComponent<SpriteRenderer>().color = tmp;
		attack3Zone.GetComponent<SpriteRenderer>().color = tmp;
		attack3Zone.GetComponent<BoxCollider2D> ().enabled = false;

	}

	void OnTriggerStay2D (Collider2D other)
	{

		if (other.tag == "playerAttack" && target.GetComponent<PlayerController>().isAttacking && !target.GetComponent<PlayerController>().hasAtk)
		{
			BossTakeDamage();
		}
	}
	void BossTakeDamage()
	{
		
		if (!invincible)
		{
			target.GetComponent<PlayerController> ().hasAtk = true;
			bossHP = bossHP - 1;
			print(bossHP);
			if (bossHP <=0)
			{

				target.GetComponent<PlayerController> ().bossState = BossState.BossDead;
				End ();
			}
		}

	}

	public void End()
	{
		SceneManager.LoadScene("0_3_Run");
	}

	}
