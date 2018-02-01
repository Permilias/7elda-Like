using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour 
{
	private bool ShieldUp;

	void Start ()
	{
		
	}

	private float timer;
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		
		if (other.tag == "Attack" )
		{
			
			if (ShieldUp)
			{
				ShieldUp = false;
			}
			else 
			{
				Death ();
			}
					
		}
	}

	void Death()
	{
		timer = 0;

		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
}
