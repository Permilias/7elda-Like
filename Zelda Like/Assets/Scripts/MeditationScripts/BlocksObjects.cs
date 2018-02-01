using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksObjects : MonoBehaviour {

    public Transform target;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "MeditationObject")
        {
            collision.transform.position = Vector3.MoveTowards(collision.transform.position, target.position, 0.35f);
            collision.GetComponent<Rigidbody2D>().gravityScale = 0;
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "MeditationObject")
        {
            collision.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }



}
