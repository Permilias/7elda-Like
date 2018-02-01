using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunEnemyScript : MonoBehaviour 
{

    //scrip a mettre sur les ennemis, il contiens la fonction de dégats

	public int damageDealt;

	
	public void DamageEnnemy()
	{
		Destroy ( gameObject );
	}

}
