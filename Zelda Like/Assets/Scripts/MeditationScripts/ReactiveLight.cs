using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveLight : MonoBehaviour
{
    public bool isLit;
    public Animator localAnim;
    public RiddleManager riddleManager;

    public void Light()
    {
        isLit = true;
        localAnim.SetBool("isLit", true);
    }

    public void Unlight()
    {
        if(riddleManager.state == RiddleState.active)
        {
            StartCoroutine("Unlighting");
        }
    }

    private IEnumerator Unlighting()
    {
        localAnim.SetBool("isLit", false);
        yield return new WaitForSeconds(1.2f);
        isLit = false;
    }
}
