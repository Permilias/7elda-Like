using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAttackDetection : MonoBehaviour 
{


    //script a mettre sur les attaques du joueur qui va call les fonctions de domage des ennemis qu'elle touche

    public void OnTriggerEnter2D(Collider2D _other)
    {

		if (_other.tag == "Enemy") 
		{
			_other.GetComponent<RunEnemyScript> ().DamageEnnemy ();
		}
        else if (_other.tag == "Destructible")
        {
            _other.GetComponent<RunDestructibleScript>().DamageDestructible(1);
        }
	}


}
