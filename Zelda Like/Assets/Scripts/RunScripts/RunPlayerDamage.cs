using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunPlayerDamage : MonoBehaviour 
{
	public int lifePoints;
	public KeyCode attack;
	public float attackTimer;
	public float timeBeforeAttackCharge;
	public float chargedAttackLenght;
	public float simpleAttackLenght;
	public bool canAttack = true;
	public bool isAttacking;


	public GameObject chargedAttack;
	public GameObject simpleAttack;
	public GameObject stopper;

    public CursorBehavior cursorBehavior;

    //scripte du joueur qui gére les dégats ifilgés et reçus


	void Start ()
	{
		

    }

	void Update()
	{
		if (Input.GetKey (attack) && canAttack) //le timer des attaques chargées
		{
			attackTimer += Time.deltaTime;
		}

		if (attackTimer > GetComponent<RunController> ().timeBeforeStopping && canAttack) //aprés un certain temps de charge le pj s'immobilise
		{
			GetComponent<RunController> ().CanMove (false);
		}

		if (Input.GetKeyUp (attack) && attackTimer > timeBeforeAttackCharge && canAttack)  //si le joueur relache aprés un certain temps on enclance l'attaque chargée
		{
			StartCoroutine (ChargedAttack (chargedAttackLenght));
		} 
		else if (Input.GetKeyUp (attack) && attackTimer < timeBeforeAttackCharge && canAttack) //sinon on enclanche l'attaque normale 
		{
			StartCoroutine(SimpleAttack (simpleAttackLenght));
			attackTimer = 0;
		}

		RotateStopper ();
    }

	IEnumerator SimpleAttack (float _attackLenght) //la coroutine de l'attaque simple
	{
        isAttacking = true;
        simpleAttack.SetActive(true);
        RotateSimpleAttack();
        yield return new WaitForSeconds(_attackLenght);
        simpleAttack.SetActive(false);
        GetComponent<RunController> ().CanMove (true);
	}

	IEnumerator ChargedAttack(float _attackLength) //la coroutine de l'attaque chargée
    {
		isAttacking = true;
		chargedAttack.SetActive (true);
		yield return new WaitForSeconds (_attackLength);
		chargedAttack.SetActive (false);
        attackTimer = 0;
        isAttacking = false;
        GetComponent<RunController>().CanMove(true);

    }

    public void RotateSimpleAttack()//on oriente l'attaque simple vers la souris
    {
        float angle = 0;
        angle = cursorBehavior.GetAngleForRotator();
        simpleAttack.transform.eulerAngles = new Vector3(0, 0, angle);
    }

	public void RotateStopper()//on oriente le stopper vers la souris
	{
		float angle = 0;
		angle = cursorBehavior.GetAngleForRotator();
		stopper.transform.eulerAngles = new Vector3(0, 0, angle);
	}

	public void OnTriggerEnter2D (Collider2D _other) //si l'on touche un ennemi lors d'un dash on le tue, si non on prends des dégas
	{
		if (_other.tag == "Enemy") 
		{
			if (GetComponent <RunController>().isDashing == true) 
			{
				_other.GetComponent<RunEnemyScript> ().DamageEnnemy ();
			}
			else
			{
				DamagePlayer (_other.GetComponent<RunEnemyScript> ().damageDealt);
			}
		}
	}


	public void DamagePlayer (int _damage) //fonction appelée quand le joueur prends des dégas
	{
		lifePoints -= _damage;

		if (lifePoints < 0)
		{
			KillPlayer ();
		}
	}

	public void KillPlayer () // fonction appelée quand le joueur n'as plu de pv
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

}
