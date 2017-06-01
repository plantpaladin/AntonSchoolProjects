using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour 
{
    private List<Player> playerList;
    private List<ButtonHandler> playerButtonList;
    public List<Sprite> deckList;
    private List<bool> hasVotedList;
    public GameObject playerPrefab;
    private SpriteRenderer currentCardDrawer;

    public int playerNumber = 1;
    public int handsize = 3;
    public int scoreRange = 5;

    private int currentPlayer;
	// Use this for initialization
	void Start () {
        playerList = new List<Player>();
        playerButtonList = new List<ButtonHandler>();
        currentCardDrawer = GetComponent<SpriteRenderer>();
        currentCardDrawer.enabled = false;
        shuffle();
        for (int i = 0; i < playerNumber; i++)
        {
            GameObject a = Instantiate(playerPrefab) as GameObject;
            Player newPlayer = a.GetComponent<Player>();
            ButtonHandler newButtonPlayer = a.GetComponent<ButtonHandler>();
            List<Sprite> newHand = new List<Sprite>();
            for(int j = 0; j<handsize;j++)
            {//since the variables are set manually they can be assumed to be correct
                Sprite b = draw();
                newHand.Add(b);
            }
            newPlayer.setVariables(newHand,this,"Player " + i);
            newButtonPlayer.setVariables("Player " + i, scoreRange,this,i);
            playerButtonList.Add(newButtonPlayer);
            playerList.Add(newPlayer);
        }
        currentPlayer = 0;
        changePlayer();
	}

    void shuffle()
    {
        for (int i = 0; i < deckList.Count; i++)
        {//shuffles the deck
            Sprite temp = deckList[i];
            int randomIndex = Random.Range(i, deckList.Count);
            deckList[i] = deckList[randomIndex];
            deckList[randomIndex] = temp;
        }
    }

    public Sprite draw()
    {
        if(deckList.Count>0)
        {
            Sprite a = deckList[0];
            deckList.RemoveAt(0);
            return a;
        }
        else
        {//the draw fails
            return null;
        }
    }

    public void playCard(Sprite card)
    {
        currentCardDrawer.enabled = true;
        currentCardDrawer.sprite = card;
        
        hasVotedList = new List<bool>();
        for (int i = 0; i < playerNumber; i++)
        {
            if (i == currentPlayer)
            {
                hasVotedList.Add(true);
            }
            else
            {
                hasVotedList.Add(false);
                playerButtonList[i].askForVote();
            }
        }
    }

    public void vote(int points, int playerID)
    {
        hasVotedList[playerID] = true;
        playerButtonList[currentPlayer].givePoints(points);
        for (int i = 0; i < playerNumber; i++)
        {
            if (hasVotedList[i] == false)
            {//if there is a player waiting to vote then the game keeps on going
                return;
            }
        }
        currentCardDrawer.enabled = false;
        
            changePlayer();
    }

    private void changePlayer()
    {
        currentPlayer += 1;
        currentPlayer = currentPlayer % playerNumber;
        playerList[currentPlayer].setCurrent(true);
    }
}
