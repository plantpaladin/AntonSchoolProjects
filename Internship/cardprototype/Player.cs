using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    public GameObject cardPrefab;
    private Deck deck;
    private List<GameObject> hand;
    bool currentPlayer = false;

    private MeshRenderer playerNameRenderer;

	void Awake () 
    {
        hand = new List<GameObject>();
        playerNameRenderer = GetComponent<MeshRenderer>();
	}

    public void setVariables(List<Sprite> newHand,Deck newDeck, string newName) 
    {
        deck = newDeck;
        name = newName;
        TextMesh nameMesh = GetComponent<TextMesh>();
        nameMesh.text = name;
        for (int i = 0; i < newHand.Count; i++)
        {
            GameObject n = Instantiate(cardPrefab) as GameObject;
            n.GetComponent<SpriteRenderer>().sprite = newHand[i];
            n.transform.position = new Vector3(-8+i*3f,0,0);
            n.GetComponent<Card>().setVariables(this, i);
            hand.Add(n);
        }
        setCurrent(false);
    }

    public void setCurrent(bool newCurrent)
    {//changes the state of the player to be either current or not
            playerNameRenderer.enabled=newCurrent;
            currentPlayer = newCurrent;
            for (int i = 0; i < hand.Count; i++)
            {
                    hand[i].SetActive(newCurrent);                
            }
        
    }
    public void playCard(int i)
    {
        i = i % hand.Count;//prevents error from removing from the wrong place
        Sprite currentCard = hand[i].GetComponent<SpriteRenderer>().sprite;
        if (currentPlayer == true)
        {
            Sprite newSprite = deck.draw();
            if (newSprite != null)
            {
                GameObject n = Instantiate(cardPrefab) as GameObject;
                n.transform.position = new Vector3(-8 + i * 3f, 0, 0);
                n.GetComponent<SpriteRenderer>().sprite = newSprite;
                n.GetComponent<Card>().setVariables(this, i);
                Destroy(hand[i]);
                hand[i] = n;
            }
            else
            {
                Destroy(hand[i]);
                hand.RemoveAt(i);
            }
            setCurrent(false);//the player will shift after this
            deck.playCard(currentCard);
        }
    }
}
