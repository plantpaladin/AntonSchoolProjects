using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RectangleManager : MonoBehaviour {

    private Text widthText;
    private Text heightText;
    private Image rectImage;

    finisher onRightAnswer;
	void Awake () 
    {
        Text[] textArr = GetComponentsInChildren<Text>();
        foreach(Text i in textArr)
        {
            if (i.tag == "Width")
            {
                widthText = i;
            }
            else if (i.tag == "Height")
            {
                heightText = i;
            }
        }
        rectImage = GetComponentInChildren<Image>();
      }

    public void setVariables(int width, int height)
    {
        Vector3 newScale = rectImage.rectTransform.localScale;
        newScale.x = width*0.5f;
        newScale.y = height*0.5f;
        rectImage.rectTransform.localScale = newScale;
        widthText.text = "Width = " + width;
        heightText.text = "Height = " + height;
    }
	
}
