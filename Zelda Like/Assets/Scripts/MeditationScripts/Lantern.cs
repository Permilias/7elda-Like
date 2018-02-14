using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lantern : MonoBehaviour {

    public MeditationManager meditationManager;
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
