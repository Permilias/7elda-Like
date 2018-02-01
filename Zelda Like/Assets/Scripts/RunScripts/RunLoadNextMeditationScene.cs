using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunLoadNextMeditationScene : MonoBehaviour 
{
	public string nextLevel;

	void Update () 
	{
		
	}

	public void OnTriggerEnter2D(Collider2D _other)
	{
		if (_other.tag == "Player")
		{
			SceneManager.LoadScene (nextLevel);
		}
	}
}
