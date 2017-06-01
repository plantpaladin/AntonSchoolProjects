using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAndUISpawner : MonoBehaviour {
    
    public GameObject playerPrefab;
    public GameObject uiPrefab;
    public GameObject labelPrefab;//all of them has to be gameobjects to work with 
    public GameObject spritePrefab;//the Instantiate/addchild methods
    public GameObject buttonPrefab;

    private int numberOfPlayers = 2;
    private bool pauseEnabled = false;
    private GameObject menuButton;
    private GameObject panelForUI;
    private List<UILabel> scoreWriteOutList;
    private List<UIFilledSprite> cooldownDisplayList;
    private List<PlayerCollider> playerScriptList;

	// Use this for initialization
	void Start () 
    {
        if (PlayerPrefs.HasKey("NumberOfPlayers"))
        {
            numberOfPlayers = PlayerPrefs.GetInt("NumberOfPlayers");
        }
        else
        {
            numberOfPlayers = 2;
        }
        pauseEnabled = false;
        Time.timeScale = 1;
        Screen.showCursor = false;
        panelForUI = (GameObject)Instantiate(uiPrefab);//creates the base UI that the rest of the 
        //objects are attached to
        menuButton = NGUITools.AddChild(panelForUI, buttonPrefab);
        UIEventListener.Get(menuButton).onClick += gotoMainMenu;
        NGUITools.SetActive(menuButton, false);

        playerScriptList = new List<PlayerCollider>();
        scoreWriteOutList = new List<UILabel>();
        cooldownDisplayList = new List<UIFilledSprite>();
        for (int i = 0; i < numberOfPlayers; i++)
        {
            GameObject newPlayer = (GameObject)Instantiate
                (playerPrefab, new Vector3(0, 2, i * 2), Quaternion.identity);
                PlayerCollider playerScript = newPlayer.GetComponent<PlayerCollider>();
            //gets the script so it can be used later on
                playerScriptList.Add(playerScript);
                Vector3 newUiPosition = new Vector3(0,0,0);
                Color newColor = Color.white;//gives values that will be overwritten
            switch (i)
                {
                    case (0):
                        {//using screen width and height variables to reduce the amount of problems coming from having different sizes
                            newUiPosition.x = -Screen.width / 2.7f;
                            newUiPosition.y = Screen.height /2.1f;
                            newColor = Color.white;
                            break;
                        }
                    case (1):
                        {
                            newUiPosition.x = Screen.width / 2.7f;
                            newUiPosition.y = Screen.height / 2.1f;
                            newColor = Color.blue;
                            break;
                        }
                    case (2):
                        {
                            newUiPosition.x = -Screen.width / 2.7f;
                            newUiPosition.y = -Screen.height / 2.1f;
                            newColor = Color.green;
                            break;
                        }
                    case (3):
                        {
                            newUiPosition.x = Screen.width / 2.7f;
                            newUiPosition.y = -Screen.height / 2.1f;
                            newColor = Color.yellow;
                            break;
                        }
                }
            playerScript.setVariables(i,newColor);
            UILabel newLabel = NGUITools.AddChild(panelForUI, labelPrefab).GetComponent<UILabel>();
            newLabel.transform.localPosition = newUiPosition;
            newLabel.transform.localScale = new Vector3(20,20,20);
            scoreWriteOutList.Add(newLabel);
            newUiPosition.y = newUiPosition.y * 0.94f;//moves the button closer to the screen
            UIFilledSprite newSprite = NGUITools.AddChild(panelForUI, spritePrefab).GetComponent<UIFilledSprite>();
            newSprite.transform.localPosition = newUiPosition;
            newSprite.transform.localScale = new Vector3(20, 20, 20);
            newSprite.color = newColor;
            cooldownDisplayList.Add(newSprite.GetComponent<UIFilledSprite>());
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {

            if (pauseEnabled == true)
            {
                NGUITools.SetActive(menuButton, false);
                pauseEnabled = false;
                Time.timeScale = 1;    
                Screen.showCursor = false;
            }

            else if (pauseEnabled == false)
            {

                NGUITools.SetActive(menuButton, true);
                pauseEnabled = true;
                Time.timeScale = 0;
                Screen.showCursor = true;
            }
        }
        for (int i = 0; i < numberOfPlayers; i++)
        {
            float score = playerScriptList[i].getScore();
            scoreWriteOutList[i].text = "Player "+(i+1)+ "$ = " + score;
            float cooldown = playerScriptList[i].getPercentageCooldownRemaining();
            cooldownDisplayList[i].fillAmount = 1 - cooldown;
        }
	
	}
    void gotoMainMenu(GameObject notUsed)
    {
        Application.LoadLevel("MenuScene");
 
    }
}

