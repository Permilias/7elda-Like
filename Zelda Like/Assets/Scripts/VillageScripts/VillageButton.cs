using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageButton : MonoBehaviour {

    public SpiritLadder localSpiritLadder;
    public bool goesUp;

    private void OnMouseUpAsButton()
    {
        if(goesUp)
        {
            localSpiritLadder.GoUp();
        }
        else
        {
            localSpiritLadder.GoDown();
        }
    }
}
