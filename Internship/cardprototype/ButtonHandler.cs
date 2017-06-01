using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonHandler : MonoBehaviour {

    public GameObject buttonPrefab;
    public GameObject canvasPrefab;
    public GameObject textPrefab;
    private Text nameAndPoints;
    private GameObject canvasObject;
    private Canvas buttonCanvas;
    private string playerName;
    private int playerPoints;
    private int playerID;
    private int numberOfScores = 5;
    private bool currentPlayer;
    private Deck deck;
    private List<GameObject> buttons;

    void Awake()
    {
        
        buttons = new List<GameObject>();
        canvasObject = Instantiate(canvasPrefab) as GameObject;
        buttonCanvas = canvasObject.GetComponent<Canvas>();
        buttonCanvas.transform.position = new Vector3(0, 0, 0);
        
    }

    public void setVariables(string newName,int newScoreNumber,Deck newDeck,int newPlayerID)
    {
        playerID = newPlayerID;
        playerName = newName;
        numberOfScores = newScoreNumber;
        deck = newDeck;
        playerPoints = 0;
        for (int i = 0; i < numberOfScores; i++)
        {
            GameObject n = Instantiate(buttonPrefab) as GameObject;
            n.transform.SetParent(buttonCanvas.transform,true);
            n.transform.position = new Vector3(playerID*165-400,i * 30f,  0);
            n.GetComponentInChildren<Text>().text = "Give " + i;
            int j = i;//the delegate function passes by reference so a new variable has to be declared
            n.GetComponent<Button>().onClick.AddListener(delegate{ vote(j); });
            buttons.Add(n);
        }
        GameObject t = Instantiate(textPrefab) as GameObject;
        nameAndPoints = t.GetComponent<Text>();
        t.transform.SetParent(buttonCanvas.transform, true);
        t.transform.position = new Vector3(playerID*175-350,200,0);
        nameAndPoints.text = playerName + " points = " + playerPoints;
        setCurrent(false);
    }
    public void askForVote()
    {
        setCurrent(true);
    }
    void vote(int i)
    {
        if (currentPlayer == true)
        {
            deck.vote(i, playerID);
            setCurrent(false);
        }
    }
    public void givePoints(int newPoints)
    {
        playerPoints += newPoints;
        nameAndPoints.text = playerName + " points = " + playerPoints;
    }
    private void setCurrent(bool newCurrent)
    {//changes the state of the player to be either current or not
        currentPlayer = newCurrent;
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].SetActive(newCurrent);
        }

    }

}
