using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunCameraController : MonoBehaviour 
{
	public GameObject player;
	public GameObject cursor;

	public float coefPlayer;
	public float coefCurseur;
	public float smoothness;

	private Vector3 target;

	void Start () 
	{
		
	}
	

	void Update () 
	{
		target = (coefCurseur * cursor.transform.position + coefPlayer * player.transform.position) / (coefCurseur + coefPlayer);
		target.z = -10f;
		transform.position = Vector3.Lerp (transform.position, target, smoothness * Time.deltaTime);
	}
}
