using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lantern : MonoBehaviour {

    public MeditationManager meditationManager;
<<<<<<< HEAD
    public string sceneLoaded;
=======
    public string loadedScene;
>>>>>>> Martin

    private void OnMouseUpAsButton()
    {
        print("prout");
        if (meditationManager.isLit)
        {
<<<<<<< HEAD

            SceneManager.LoadScene(sceneLoaded);
=======
            SceneManager.LoadScene(loadedScene);
>>>>>>> Martin
        }
    }
}
