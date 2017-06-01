using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public Button[] sceneButtonArray;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < sceneButtonArray.Length; i++)
        {//the a variable is because we are using a delegate so the value has to be a temporal variable  
            int a = i + 1;//or the value at end will be copied(in this case the length) for all listeners
            sceneButtonArray[i].onClick.AddListener(delegate { loadFunc(a); });//the +1 is because menu is at 0
        }
    }

    public void loadFunc(int level)
    {
        SceneManager.LoadScene(level);//the menu is scene 0
    }

    // Update is called once per frame
    void Update()
    {

    }
}
