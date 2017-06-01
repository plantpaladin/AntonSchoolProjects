using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class RectangleGameManager : MonoBehaviour {
    public GameObject rectanglePrefab;
    public GameObject divisionPrefab;
    public GameObject timerPrefab;
    public GameObject inputFieldPrefab;
    public GameObject additionPrefab;
    public GameObject subtractionPrefab;
    public float countdownTime = 60f;
    public float wrongPenalty = 1f;
    public float rightReward = 5f;

    public string mode = "Rectangle";//defined at the beginning of the 
    public delegate void SpawnObject();

    private GameObject inputFieldObject;
    private GameObject timerObject;
    private GameObject currentObject;//refers to the current object on screen

    private SpawnObject currentSpawnObject;
    
    private InputField input;
    private Timer timer;
    private InputField inputObject;
    private int rightAnswer;
    
    bool randomBoolean ()
{
    if (Random.value >= 0.5)
    {
        return true;
    }
    return false;
}
	void Awake () 
    {
        timerObject = Instantiate(timerPrefab) as GameObject;
        timer = timerObject.GetComponentInChildren<Timer>();
        inputFieldObject = Instantiate(inputFieldPrefab) as GameObject;
        input = inputFieldObject.GetComponentInChildren<InputField>();
        input.onEndEdit.AddListener(submitAnswer);
        input.contentType = InputField.ContentType.IntegerNumber;
        input.Select();
        timer.startCountdown(countdownTime, onCountDownFinish);
        switch (mode)
        {//delegates is not able to be picked up from the inspector
            case "AreaAndPerimeter"://multiplication
                currentSpawnObject = spawnRectangle;
                break;
            case "Density"://division
                currentSpawnObject = spawnDivision;
                break;
            case "AddWeights":
                currentSpawnObject = spawnAddition;
                break;
            case "SubtractWeights":
                currentSpawnObject = spawnSubtraction;
                break;
        }
        if (currentSpawnObject != null)
        {
            currentSpawnObject();
        }
	}
    void spawnRectangle()//spawns a rectangle with random parameters for multiplication
    {
        int newWidth = Random.Range(2, 10);
        int newHeight = Random.Range(2, 10);//max values that are effective are 9 in width and 5 in height
        bool isArea = true;//randomBoolean();
        currentObject = Instantiate(rectanglePrefab) as GameObject;
        RectangleManager rectangle = currentObject.GetComponent<RectangleManager>();
        rectangle.transform.position = new Vector3(0, 0, 0);
        rectangle.setVariables(newWidth,newHeight);
        if (isArea)
        {//the right answer is the area of the object 
            rightAnswer = newWidth * newHeight;
            input.placeholder.GetComponent<Text>().text = "Write area";
            input.placeholder.GetComponent<Text>().color = Color.green;
        }
        else
        {//the right answer is the perimeter
            rightAnswer = newWidth * 2 + newHeight * 2;
            input.placeholder.GetComponent<Text>().text = "Write perimeter";
            input.placeholder.GetComponent<Text>().color = Color.blue;
        }
    }

    void spawnDivision()//spawns a rectangle with random parameters for division
    {
        int newVolume = Random.Range(2, 10);
        int density = Random.Range(2, 10);//max values that are effective are 9 in width and 5 in height
        int newWeight = newVolume * density;
        rightAnswer = density;
        currentObject = Instantiate(divisionPrefab) as GameObject;
        DivisionManager division = currentObject.GetComponent<DivisionManager>();
        division.transform.position = new Vector3(0, 0, 0);
        division.setVariables(newVolume,newWeight);
        //the right answer is the density of the object 
            input.placeholder.GetComponent<Text>().text = "Write density";
            input.placeholder.GetComponent<Text>().color = Color.blue;
    }
    void spawnAddition()//spawns rectangles with random weights for addition
    {
       
        rightAnswer = 0;
        int[] intArray = new int[3];
        for (int i = 0; i < 3; i++)
        {
            int a =  Random.Range(2, 15);
            intArray[i] = a;
            rightAnswer += a;
        }
        currentObject = Instantiate(additionPrefab) as GameObject;
        AdditionManager addition = currentObject.GetComponent<AdditionManager>();
        addition.transform.position = new Vector3(0, 0, 0);
        addition.setVariables(intArray);
        //the right answer is the density of the object 
        input.placeholder.GetComponent<Text>().text = "Write total weight";
        input.placeholder.GetComponent<Text>().color = Color.blue;
    }

    void spawnSubtraction()//spawns rectangles with random weights for addition
    {

        int total = 0;
        int[] intArray = new int[2];
        for (int i = 0; i < 2; i++)
        {
            int a = Random.Range(2, 15);
            intArray[i] = a;
            total += a;
        }
        int b = Random.Range(2, 15);
        rightAnswer = b;
        total += b;
        currentObject = Instantiate(subtractionPrefab) as GameObject;
        SubtractionManager subtraction = currentObject.GetComponent<SubtractionManager>();
        subtraction.transform.position = new Vector3(0, 0, 0);
        subtraction.setVariables(intArray,total);
        //the right answer is the density of the object 
        input.placeholder.GetComponent<Text>().text = "Write unknown";
        input.placeholder.GetComponent<Text>().color = Color.blue;
    }
    void onCountDownFinish()
    {
       SceneManager.LoadScene("Menu");
    }
	// Update is called once per frame
    void submitAnswer(string answer)
    {
        input.text = "";//resets the field
        input.ActivateInputField();
        input.Select();
        int number = 0;
        bool isValid = int.TryParse(answer,out number);
        if (isValid)
        {//if it is not a valid input then it is ignored
            if (number == rightAnswer)
            {//if it is the right number the player is rewarded
                GameObject.Destroy(currentObject);
                currentSpawnObject();
                timer.timePenalty(-rightReward);
            }//called with a negative number to give the player that much extra time
            else
            {//if it is the wrong answer then the player suffers a penalty
                timer.timePenalty(wrongPenalty);
            }
        }
    }
}
