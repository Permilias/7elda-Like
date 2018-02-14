using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RiddleState
{
    inactive,
    active,
    solved
}

public class RiddleManager : MonoBehaviour {

    public RiddleState state = RiddleState.inactive;

    public ReactiveLight[] lightsArray;

    public MeditationManager meditationManager;

    public bool canWin = true;

    public void Ignite(int _lightIndex)
    {
        if (_lightIndex > lightsArray.Length)
        {
            Debug.Log("Light Index is out of range !");
        }
        else
        {
            if (lightsArray[_lightIndex].isLit == false)
            {
                lightsArray[_lightIndex].Light();
            }
        }
    }

    public void Extinguish(int _lightIndex)
    {
        if(_lightIndex > lightsArray.Length)
        {
            Debug.Log("Light Index is out of range !");
        }
        else
        {
            if (lightsArray[_lightIndex].isLit == true && state == RiddleState.active)
            {
                lightsArray[_lightIndex].Unlight();
            }
        }
    }

    private void FixedUpdate()
    {
        if (lightsArray[4].isLit && lightsArray[5].isLit && lightsArray[6].isLit && lightsArray[7].isLit && state == RiddleState.active && canWin)
        {
            canWin = false;
            StartCoroutine("Victory");
        }
    }

    private IEnumerator Victory()
    {
        state = RiddleState.solved;
        yield return new WaitForSeconds(0.3f);
        Ignite(0);
        Ignite(4);
        yield return new WaitForSeconds(0.3f);
        Ignite(1);
        Ignite(5);
        yield return new WaitForSeconds(0.3f);
        Ignite(2);
        Ignite(6);
        yield return new WaitForSeconds(0.3f);
        Ignite(3);
        Ignite(7);
        yield return new WaitForSeconds(0.3f);
        Ignite(8);
        meditationManager.lanternAnim.SetTrigger("light");
        meditationManager.isLit = true;
    }
}
