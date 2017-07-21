using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class GameManager : NetworkManager 
{
    public GameObject deckPrefab;
    private static Deck deck;
    private GameObject deckObject;


    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player = (GameObject)Instantiate(playerPrefab);
        if (deck == null)
        {
           
            deckObject = Instantiate(deckPrefab) as GameObject;
            deck = deckObject.GetComponent<Deck>();
        }
        deck.addNewPlayer();
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        int i = NetworkServer.connections.Count - 1;
        initMsg m = new initMsg();
        m.playerNumber = i;
        for (int j = 0; j < m.hand.Length;j++)
        {
            m.hand[j] = deck.draw();
        }
        
        NetworkServer.connections[i].Send(GMMsg.initialize, m);
        NetworkServer.connections[i].RegisterHandler(PlayerMsg.cardPlayed, msgCardPlay);
        NetworkServer.connections[i].RegisterHandler(PlayerMsg.votePoints, msgVotePoints);

    }
    void msgVotePoints(NetworkMessage msg)
    {
        playerInputMsg m = msg.ReadMessage<playerInputMsg>();
        deck.recieveVotePoints(m.playerInput, m.playerNumber);
    }
    void msgCardPlay(NetworkMessage msg)
    {
        playerInputMsg m = msg.ReadMessage<playerInputMsg>();
        deck.recievePlay(m.playerInput, m.playerNumber);
    }
    
}
