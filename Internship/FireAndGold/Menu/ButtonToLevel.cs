using UnityEngine;
using System.Collections;

public class ButtonToLevel : MonoBehaviour {
    public int sceneNumber = 1;//goes from 1 to 4 as scene 0 is the menu
    void onClick()
    {
        Application.LoadLevel(sceneNumber);
    }
	
}
