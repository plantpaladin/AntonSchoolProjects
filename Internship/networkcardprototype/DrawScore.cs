using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public static class HelperMethods
{
    public static List<GameObject> GetChildren(this GameObject go)
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform tran in go.transform)
        {
            children.Add(tran.gameObject);
        }
        return children;
    }
}
public class DrawScore : NetworkBehaviour {
    private Canvas myCanvas;
    private int numberOfPlayers=0;
    private List<GameObject> textObjectList;
    private List<Text> textList;
    private Text stateText;
    
	public void recieveScore(int newNumberOfPlayers, int[] scoreArray) 
    {
        if (numberOfPlayers == 0)
        {
            textObjectList = new List<GameObject>();
            textObjectList = gameObject.GetChildren();
            textList = new List<Text>();
            myCanvas = GetComponent<Canvas>();
            transform.position = new Vector3(0, 0, 0);
            for (int i = 0; i < 4; i++)//adds the max number of players
            {
                textList.Add(textObjectList[i].GetComponent<Text>());
                textList[i].transform.position = new Vector3(i * 175 - 350, Screen.height*0.4f, 0);
            }
            stateText = textObjectList[4].GetComponent <Text>();
            
        }
        numberOfPlayers = newNumberOfPlayers;
        for (int i = 0; i < numberOfPlayers; i++)//adds the max number of players
        {
            textList[i].text = "Player " + i + " Score = " + scoreArray[i];
        }
	}
    public void showState(playerState state)
    {
        stateText.text = state.ToString();
    }
    
 
  

}
