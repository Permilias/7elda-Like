using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunDestructibleScript : MonoBehaviour
{
    public float life;
    public Animator localAnim;
    public Collider2D localCollider;
    public bool isDestroyedOnDeath;

    public void DamageDestructible(float _damage)
    {
        life -= _damage;
        if(life <= 0)
        {
            StartCoroutine("Destruct");
        }
    }

    private IEnumerator Destruct()
    {
        localCollider.enabled = false;
        yield return new WaitForSeconds(1);
        if(isDestroyedOnDeath)
        {
            Destroy(gameObject);
        }
    }
}
