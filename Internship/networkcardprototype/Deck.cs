using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
public class Deck : NetworkBehaviour {
    int numberOfPlayers;
    int currentPlayer;
    bool pickCardState;//if false the game is in the voteState
    public List<int> deckList;
    private List<int> playerPointsList;
    private List<bool> hasVotedList;

    public void addNewPlayer() 
    {
        if (numberOfPlayers == 0)
        {
            
            hasVotedList = new List<bool>();
            playerPointsList = new List<int>();
            numberOfPlayers = 0;
            currentPlayer = 0;
            pickCardState = true;
        }
        numberOfPlayers++;
        playerPointsList.Add(0);
        hasVotedList.Add(true);
    }
    public int draw()
    {
        if (deckList.Count > 0)
        {
            int a = deckList[0];
            shuffle();
            return a;
        }
        else
        {
            return -1;//the caller will answer that
        }


    }
    void shuffle()
    {
        for (int i = 0; i < deckList.Count; i++)
        {//shuffles the deck
            int temp = deckList[i];
            int randomIndex = Random.Range(i, deckList.Count);
            deckList[i] = deckList[randomIndex];
            deckList[randomIndex] = temp;
        }
    }
    public void recieveVotePoints(int points,int player)
    {
        if (hasVotedList[player] == false || numberOfPlayers == 1)
        {
            playerPointsList[currentPlayer] += points;
            hasVotedList[player] = true;
            bool allvoted = true;
            for (int i = 0; i < numberOfPlayers; i++)
            {
                if(hasVotedList[i]==false)
                {
                    allvoted = false;
                    i = numberOfPlayers;//will cause loop to exit
                }
            }
            int[] scoreArr = new int[numberOfPlayers];
            scoreArr = playerPointsList.ToArray();
            showScoreToAll(scoreArr);
            if (allvoted)
            {
                pickCardState = true;
                currentPlayer++;
                currentPlayer = currentPlayer%numberOfPlayers;//ensures the the next player is chosen
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    if (i != currentPlayer)
                    {//tells the others to wait
                        changeState(2, i);
                    }
                    else
                    {   //and the next player to pick a card
                        changeState(1, currentPlayer);
                    }
                }
            }
        }
    }
    /*
     enum playerState
    {
        VOTE, PLAY, WAIT // 0 1 2
    }
    */
    public void recievePlay(int card,int player)
    {
        if (player == currentPlayer && pickCardState == true)
        {//shows the current card and gives the player a new card
            pickCardState = false;
            int a = draw();
            sendCardDrawn(a, currentPlayer);
            showCardToAll(card);
            hasVotedList[currentPlayer] = true;
            for (int i=0; i < numberOfPlayers; i++)
            {
                if (i != currentPlayer)
                {//tells the others to vote
                    hasVotedList[i]=false;
                    changeState(0, i);
                }
            }
            if (numberOfPlayers == 1)//just for testing with 1 player
            {
                changeState(0, 0);
            }
        }
    }
    private void showScoreToAll(int[] scoreArray)
    {
        scoreMsg m = new scoreMsg();
        m.scoreArray = scoreArray;
        m.numberOfPlayers = numberOfPlayers;
        NetworkServer.SendToAll(GMMsg.scoreShow, m);
    }

    private void showCardToAll(int card)
    {
        IntegerMessage m = new IntegerMessage(card);
        NetworkServer.SendToAll(GMMsg.cardShow, m);
    }

    private void changeState(int newState,int player)
    {
        IntegerMessage m = new IntegerMessage(newState);
        NetworkServer.connections[player].Send(GMMsg.state, m);
    }

    private void sendCardDrawn(int card, int player)
    {
        IntegerMessage m = new IntegerMessage(card);
        NetworkServer.connections[player].Send(GMMsg.cardDraw, m);
    }

}
