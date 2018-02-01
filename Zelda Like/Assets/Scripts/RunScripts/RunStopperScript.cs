using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunStopperScript : MonoBehaviour 
{

	public void OnTriggerEnter2D(Collider2D _other)
	{
		if (_other.tag == ("Wall") || _other.tag ==("River") || _other.tag ==("Destructible"))
		{
			GetComponentInParent<RunController> ().CanMove (false);
		}
	}

	public void OnTriggerExit2D(Collider2D _other)
	{
		if (_other.tag == ("Wall") || _other.tag ==("River") || _other.tag ==("Destructible"))
		{
			GetComponentInParent<RunController> ().CanMove (true);
		}
	}

}
