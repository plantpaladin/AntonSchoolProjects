using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class HealthBarText : MonoBehaviour {
    public GameObject sliderPrefab;
    private GameObject sliderObject;
    private Slider sliderUI;
    private Text textSliderValue;
    private int health;
    private string nameOfHealth = "Health = ";

	// Use this for initialization
	void Start () {
        textSliderValue = GetComponent<Text>();
     }

    public void setVariables(string newNameOfHealth,int maxHealth)
    {//called by the object that created the health bar
        nameOfHealth = newNameOfHealth;
        textSliderValue = GetComponent<Text>();
        sliderObject = Instantiate(sliderPrefab);
        sliderObject.GetComponent<Transform>().SetParent(transform,false);
        Vector3 newVector = new Vector3();
        newVector = sliderObject.transform.localPosition;
        newVector.y -= 15;
        sliderObject.transform.localPosition = newVector;
        sliderUI = sliderObject.GetComponent<Slider>();
        sliderUI.maxValue = maxHealth;
        setSliderValue(maxHealth);
    }

    public void setSliderValue(int newValue)
    {
        health = newValue;
        sliderUI.value = newValue;
        string sliderMessage = nameOfHealth + health;
        textSliderValue.text = sliderMessage;
    }
}
