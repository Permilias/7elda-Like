using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchetypeBoss : MonoBehaviour {

	// Ce Script est un archétype, au moment de créer un nouveau Boss, Copier/Coller ce script est recommandé
	//Veillez a ne pas coder un boss en particulier dans ce script, mais à rester le plus global possible.

	[Header ("Quelques variables Générales")]
	public int attackNB; //Le Type de Sa prochaine attaque (1 pour attack1, 3 pour attack2, etc.)
	public bool invincible = false; //Tant que c'est faux, le boss ne peut pas pendre de dégâts.
	public int bossHP; //Les points de vie du boss, si il arrive à 0, c'est la fin du combat.

	[Header ("Attack 1")]
	//Pour Attack1
	public float attack1Rythm; //Le temps en seconde, entre la fin d'une attaque et le début du télégraph de la suivante
	public float attack1Charge; //Le temps, en seconde, entre le début du télégraph et le moment où l'attaque prend effet.
	public GameObject attack1Zone; //La Hitbox de l'attaque, pour la modifier, il suffit de modifier le gameobject
	public GameObject attack1Pivot; //Le Pivot de l'attaque. Lorsqu'on veut déplacer l'attaque, on déplace son pivot.

	[Header ("Attack 3")]
	//Pour Attack2
	public float attack2Rythm; //Le temps en seconde, entre la fin d'une attaque et le début du télégraph de la suivante
	public float attack2Charge; //Le temps, en seconde, entre le début du télégraph et le moment où l'attaque prend effet.
	public GameObject attack2Zone; //La Hitbox de l'attaque, pour la modifier, il suffit de modifier le gameobject
	public GameObject attack2Pivot; //Le Pivot de l'attaque. Lorsqu'on veut déplacer l'attaque, on déplace son pivot.


	//Pour Attack3, et ainsi de suite.



	//Qq autres variables
	private bool tgOn = false; //Vérifie que le télégraph a bien eu lieu, si c'est le cas, il lance l'attaque.
	private float timer; //Le Timer
	private bool onceOnly = true; //Le Reset d'attaque n'a lieu qu'une seule fois


	void Start () {
		
	}

	void Update () {

		timer += Time.deltaTime;

		//Si sa prochaine attack doit être de type 1
		if (attackNB == 1)
		{
			if (timer >= attack1Rythm/5 && !tgOn && onceOnly) //Met fin à l'attaque.
			{
				onceOnly = false;
				Attack1Reset ();
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

		//Si sa prochaine attack doit être de type 3
		if (attackNB == 3)
		{
			if (timer >= attack2Rythm/5 && !tgOn && onceOnly) //Met fin à l'attaque.
			{
				onceOnly = false;
				Attack2Reset ();
			}

			if (timer >= attack2Rythm && !tgOn) //Lance le télégraph (TG) de l'attaque 3, si il n'a pas deja eu lieu
			{
				Attack2TG ();
			}
			if (timer >= attack2Charge && tgOn)  //Lance l'attaque 3, si le TG a deja eu lieu
			{
				Attack2 ();
			}

		}





	}

	//A call lorsque le Boss se fait attaquer. Cette fonction vérifie que le boss est vincible.
	void BossTakeDamage()
	{
		if (!invincible)
		{
			bossHP = bossHP - 1;
			print(bossHP);
			if (bossHP <=0)
			{
				print("gg");
			}
		}
	}

	//Les Fonctions de l'Attaque 1
	void Attack1TG ()  //Lance Le TG de l'attaque
	{
		attack1Zone.GetComponent<SpriteRenderer>().color += new Color (0, 0, 0, 0.1f); //Provisoire : Indication visuelle du TG
		timer = 0; //reset le timer
		tgOn = true; //Le Télégraph a bel et bien eu lieu, l'attack est prete à être lancé !!
		//Intégrez ici la position de l'attaque
	}

	void Attack1 ()  //Lance l'Attaque
	{
		attack1Zone.GetComponent<SpriteRenderer>().color += new Color (0, 0, 0, 0.9f); //Provisoire : Indication visuelle de l'attaque
		attack1Zone.GetComponent<BoxCollider2D> ().enabled = true; //La Zone d'ataqu est activée.
		timer = 0; //reset le timer
		tgOn = false; //L'attaque a bel et bien eu lieu.
		onceOnly = true; //permet de reset l'attaque.
	}

	void Attack1Reset () //Reset l'Attaque.
	{
		attack1Zone.GetComponent<SpriteRenderer>().color += new Color (0, 0, 0, -1f);
		attack1Zone.GetComponent<BoxCollider2D> ().enabled = false;
	}



	//les Fonctions de l'Attaque 3
	void Attack2TG ()  //Lance Le TG de l'attaque
	{
		attack2Zone.GetComponent<SpriteRenderer>().color += new Color (0, 0, 0, 0.1f); //Provisoire : Indication visuelle du TG
		timer = 0; //reset le timer
		tgOn = true; //Le Télégraph a bel et bien eu lieu, l'attack est prete à être lancé !!
		//Intégrez ici la position de l'attaque
	}

	void Attack2 ()  //Lance l'Attaque
	{
		attack2Zone.GetComponent<SpriteRenderer>().color += new Color (0, 0, 0, 0.9f); //Provisoire : Indication visuelle de l'attaque
		attack2Zone.GetComponent<BoxCollider2D> ().enabled = true; //La Zone d'ataqu est activée.
		timer = 0; //reset le timer
		tgOn = false; //L'attaque a bel et bien eu lieu.
		onceOnly = true; //permet de reset l'attaque.
	}

	void Attack2Reset () //Reset l'Attaque.
	{
		attack2Zone.GetComponent<SpriteRenderer>().color += new Color (0, 0, 0, -1f);
		attack2Zone.GetComponent<BoxCollider2D> ().enabled = false;
	}
}
