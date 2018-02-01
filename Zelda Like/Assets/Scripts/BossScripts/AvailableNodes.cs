using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableNodes : MonoBehaviour 
{
	public GameObject[] availableNodes;
	public GameObject[] availableNodesExtanded;
	public Animator anim;
	public bool isDisponible;
	public GameObject Player;

	void Start ()
	{
		anim = gameObject.GetComponent<Animator> ();
		Player = GameObject.Find("Player");
	}

	void Update()
	{
		if(isDisponible) 
		{
			anim.SetBool ("isDisponible", true);
		}
		if(!isDisponible) 
		{
			anim.SetBool ("isDisponible", false);
		}




	}

	void ColorNode ()
	{

	}
}
	

