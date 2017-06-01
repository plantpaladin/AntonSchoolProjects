using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class GMMsg
{
    public static short state = MsgType.Highest + 1;
    public static short cardShow = MsgType.Highest + 2;
    public static short initialize = MsgType.Highest + 3;
    public static short cardDraw = MsgType.Highest + 4;
    public static short scoreShow = MsgType.Highest + 5;
}
public class PlayerMsg
{
    public static short votePoints = MsgType.Highest + 6;
    public static short cardPlayed = MsgType.Highest + 7;
}

public class scoreMsg : MessageBase
    {
    public int numberOfPlayers;//not the same message as the initmsg in case handsize
    public int[] scoreArray = new int[4];//needs to be changed
    }

public class initMsg : MessageBase
    {
        public int playerNumber;
        public int[] hand = new int[4];//the handSize is set here
    }
public class playerInputMsg : MessageBase
{
    public int playerInput;
    public int playerNumber;
    public playerInputMsg(int pInp, int pNumb)
    {
        playerInput = pInp;
        playerNumber = pNumb;
    }
    public playerInputMsg()
    {//this is used for it to able to be read in the network
    }//needs public constructor without parameters
}
public enum playerState
{
    VOTE, PLAY, WAIT
}

public class Player : NetworkBehaviour {
    
    private SpriteRenderer currentCardRenderer;
    private playerState currentState;
    public List<Sprite> allCards;
    public GameObject cardPrefab;
    public GameObject canvasPrefab;
    private GameObject canvasObject;
    private DrawScore scoreDrawer;
    private List<GameObject> hand;
    private List<int> handNumbers;//this is in order to send the cards played
    private int playerNumber;
    private int cardPlace;

    private void changeState(playerState newState)
    {
        currentState = newState;
        scoreDrawer.showState(newState);
    }

	// Use this for initialization
    public override void OnStartLocalPlayer()
    {
        currentCardRenderer = GetComponent<SpriteRenderer>();
        Vector3 newPosition = transform.position;
        newPosition.y += 2;
        transform.position = newPosition;
        
        hand = new List<GameObject>();
        handNumbers = new List<int>();
        cardPlace = -1;
        connectionToServer.RegisterHandler(GMMsg.initialize, msgInitialize);
        connectionToServer.RegisterHandler(GMMsg.cardShow, msgCardShow);
        connectionToServer.RegisterHandler(GMMsg.state, msgUpdateState);
        connectionToServer.RegisterHandler(GMMsg.cardDraw, msgCardDrawn);
        connectionToServer.RegisterHandler(GMMsg.scoreShow, msgScoreShow);
	}

    void msgInitialize(NetworkMessage msg)
    {
        
        initMsg m = msg.ReadMessage<initMsg>();
        int[] msgHand = m.hand;
        playerNumber = m.playerNumber;
        
        for (int i = 0; i < msgHand.Length; i++)
        {//this is for adding the cards in the beginning
            GameObject n = Instantiate(cardPrefab) as GameObject;
            n.GetComponent<SpriteRenderer>().sprite = allCards[msgHand[i]];
            n.transform.position = new Vector3(-8 + i * 3f, -2, 0);
            handNumbers.Add(msgHand[i]);//this is stored to make client commands simpler
            hand.Add(n);//reliable sequenced ensures that if this function is delivered first
        }//it will be executed first
        canvasObject = Instantiate(canvasPrefab) as GameObject;
        scoreDrawer = canvasObject.GetComponent<DrawScore>();
        int[] initValues = new int[4]{0,0,0,0};
        scoreDrawer.recieveScore(4, initValues);//the number of players is set to 4 to keep it simple
        if (playerNumber == 0)
        {
            changeState(playerState.PLAY);
        }
        else
        {
            changeState(playerState.WAIT);
        }
    }
    public void msgCardShow(NetworkMessage msg)
    {
        IntegerMessage m = msg.ReadMessage<IntegerMessage>();
        int cardNumber = m.value;
        currentCardRenderer.sprite = allCards[cardNumber];

    }

    void msgUpdateState(NetworkMessage msg) 
    {
        IntegerMessage m = msg.ReadMessage<IntegerMessage>();
        changeState((playerState)m.value);
    }

    void msgCardDrawn(NetworkMessage msg)
    {
        IntegerMessage m = msg.ReadMessage<IntegerMessage>();
        int cardNumber = m.value;
                
        if (cardNumber != -1)//if the draw fails then the card should not be shown
        {
        hand[cardPlace].SetActive(true);
        hand[cardPlace].GetComponent<SpriteRenderer>().sprite = allCards[cardNumber];
        handNumbers[cardPlace] = cardNumber;
        }            
    }

    void msgScoreShow(NetworkMessage msg)
    {
        scoreMsg m = msg.ReadMessage<scoreMsg>();
        int numbPlayers = m.numberOfPlayers;
        int[] scoreArr = m.scoreArray;
        scoreDrawer.recieveScore(numbPlayers, scoreArr);

    }
    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer==true && currentState!=playerState.WAIT)
        {
                        if (Input.GetKeyDown(KeyCode.Alpha1))
                        {
                            inputNumber(1);
                        }
                        else if (Input.GetKeyDown(KeyCode.Alpha2))
                        {
                            inputNumber(2);
                        }
                        else if (Input.GetKeyDown(KeyCode.Alpha3))
                        {
                            inputNumber(3);
                        }
                        else if (Input.GetKeyDown(KeyCode.Alpha4))
                        {
                            inputNumber(4);
                        }
                        else if (Input.GetKeyDown(KeyCode.Alpha5))
                        {
                            inputNumber(5);
                        }
            }
        
    }

    void inputNumber(int i)
    {
        if (currentState == playerState.PLAY)
        {
            i--;
            if (i < hand.Count)
            {
                int cardNumber = handNumbers[i];
                if (cardNumber != -1)
                    {
                cardPlace = i;
                CmdChoose(cardNumber);
                changeState(playerState.WAIT);
                    }
                
            }
        }
        else if (currentState == playerState.VOTE)
        {
            Cmdvote(i);
            changeState(playerState.WAIT);
        }
        
    }

    void Cmdvote(int points)
    {
        playerInputMsg m = new playerInputMsg();
        m.playerInput = points;
        m.playerNumber = playerNumber;
        connectionToServer.Send(PlayerMsg.votePoints, m);
    }

    void CmdChoose(int cardChoice)
    {
            playerInputMsg m = new playerInputMsg();
            m.playerInput = cardChoice;
            m.playerNumber = playerNumber;
            connectionToServer.Send(PlayerMsg.cardPlayed, m);
            hand[cardPlace].SetActive(false);
            handNumbers[cardPlace] = -1;
    }


}
    //state card från servern connectionToClient 
    
