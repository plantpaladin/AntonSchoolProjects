using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public delegate void numberDelegate(int n);
public class PlayerAndUISpawner : MonoBehaviour {

    private int numberOfPlayers = 2;
    private bool pauseEnabled = false;

    public GameObject playerPrefab;
    public GameObject prefabUI;


    public Vector3[] playerPositions;
    private Color[] playerColors;
    private Text scoreText;
    private int score;
    private List<PlayerMovement> playerList;
    void scoreReport(int change)
    {
        score += change;
        if (change == 5)
        {
            SceneManager.LoadScene("MenuScene");//if they hit the generator then the player wins
        }
    }
    // Use this for initialization
    void Awake()
    {
        score = 0;
        playerList = new List<PlayerMovement>();
        playerColors = new Color[4]{Color.blue,Color.red,Color.yellow,Color.green};
        Vector2 anchorUI = new Vector2(0.5f,1f);
        if (PlayerPrefs.HasKey("NumberOfPlayers"))
        {
            numberOfPlayers = PlayerPrefs.GetInt("NumberOfPlayers");
        }
        else
        {
            numberOfPlayers = 2;
            PlayerPrefs.SetInt("NumberOfPlayers", 2);//ensures other functions can access it
        }
        GameObject newUi = (GameObject)Instantiate(prefabUI);
        scoreText = newUi.GetComponentInChildren<Text>();
        scoreText.text = "Score =";
        RectTransform newPos = scoreText.gameObject.GetComponent<RectTransform>();
        newPos.anchorMin = anchorUI;
        newPos.anchorMax = anchorUI;
        newPos.pivot = anchorUI;
        for (int i = 0; i < numberOfPlayers; i++)
        {
            GameObject newPlayer = (GameObject)Instantiate(playerPrefab);
            PlayerMovement playerScript = newPlayer.GetComponent<PlayerMovement>();
            playerScript.setVariables(playerPositions[i], i, playerColors[i],scoreReport);
            playerList.Add(playerScript);

           
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {

        scoreText.text = "Score = " + score;
        if (Input.GetKeyDown(KeyCode.P))
        {

            if (pauseEnabled == true)
            {
                pauseEnabled = false;
                Time.timeScale = 1;
            }

            else if (pauseEnabled == false)
            {

                pauseEnabled = true;
                Time.timeScale = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuScene");
        }
        
        for (int i = 0; i < numberOfPlayers; i++)
        {
            /*float cooldown = playerScriptList[i].getPercentageCooldownRemaining();
            cooldownDisplayList[i].fillAmount = 1 - cooldown;*/
        }

    }
}
