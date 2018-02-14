using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayerChanging : MonoBehaviour {

    public Transform playerTransform;

    public SpriteRenderer localSR;

	void FixedUpdate () {

        if (transform.position.y < playerTransform.position.y + 0.25f && localSR.sortingOrder != 2)
        {
            localSR.sortingOrder = 2;
        }
        if (transform.position.y > playerTransform.position.y + 0.25f && localSR.sortingOrder != 0)
        {
            localSR.sortingOrder = 0;
        }

    }
}
