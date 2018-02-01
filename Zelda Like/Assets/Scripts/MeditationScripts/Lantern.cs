using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lantern : MonoBehaviour {

    public MeditationManager meditationManager;

    private void OnMouseUpAsButton()
    {
        if(meditationManager.isLit)
        {
            SceneManager.LoadScene("0_7_Village");
        }
    }
}
