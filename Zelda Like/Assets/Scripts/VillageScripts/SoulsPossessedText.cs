using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoulsPossessedText : MonoBehaviour {

    public TextMeshProUGUI localText;
    private int soulsPossessed;

    private void Start()
    {
        
        localText.text = "Souls : " + GameManager.soulsPossessed.ToString();
    }

    void FixedUpdate () {

        if(soulsPossessed != GameManager.soulsPossessed)
        {
            soulsPossessed = GameManager.soulsPossessed;
            localText.text = "Souls : " + soulsPossessed.ToString();
        }
	}
}
