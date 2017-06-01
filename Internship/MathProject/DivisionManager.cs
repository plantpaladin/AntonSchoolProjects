using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DivisionManager : MonoBehaviour {

    private Text volumeText;
    private Text weightText;
    private Image rectImage;

    finisher onRightAnswer;
	void Awake () 
    {
        Text[] textArr = GetComponentsInChildren<Text>();
        foreach(Text i in textArr)
        {
            if (i.tag == "Volume")
            {
                volumeText = i;
            }
            else if (i.tag == "Weight")
            {
                weightText = i;
            }
        }
        rectImage = GetComponentInChildren<Image>();
      }

    public void setVariables(int volume, int weight)
    {
        Vector3 newScale = rectImage.rectTransform.localScale;
        float a = Mathf.Sqrt((float)volume)*0.5f;
        newScale.x = a;//if the last side is 1 then the other sides is the sqrt of the volume, 
        newScale.y = a;//it's multiplied by 0.5 to make it the same scale as the other rectangle
        rectImage.rectTransform.localScale = newScale;
        volumeText.text = "Volume = " + volume;
        weightText.text = "weight = " + weight;
    }
	
}
