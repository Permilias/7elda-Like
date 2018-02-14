using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lantern : MonoBehaviour {

    public MeditationManager meditationManager;
<<<<<<< HEAD
    public string sceneLoaded;

    private void OnMouseUpAsButton()
    {
        print("prout");
        if (meditationManager.isLit)
        {

            SceneManager.LoadScene(sceneLoaded);
        }
    }
}
