using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {
    public int index;
    Player holder;
    public void setVariables(Player newHolder, int newIndex)
    {
        holder = newHolder;
        index = newIndex;
    }
    void OnMouseDown()
    {
        holder.playCard(index);
    }
	
}
