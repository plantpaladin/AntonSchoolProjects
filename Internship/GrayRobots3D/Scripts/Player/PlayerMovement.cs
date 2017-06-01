using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private string xAxisName = "Horizontal";
    private string zAxisName = "Vertical";
    private string paintAxisName = "Paint";
    private float currentSpeed = 4;
    private PlayerPaint painter;
    private Rigidbody body;
    private numberDelegate scoreReport;
    public Vector3 startPosition;
    
    void Awake()
    {
  
        body = GetComponent<Rigidbody>();
        painter = GetComponent<PlayerPaint>();
        startPosition = new Vector3(20, 0, 0);
        resetPosition();
    }
    public void setVariables(Vector3 newPosition,int playerID,Color col,numberDelegate scoreDel)
    {
        xAxisName += playerID;//this links the players to the input from axis0, axis 1, axis 2...
        zAxisName += playerID;
        paintAxisName += playerID;
        painter.setPaintColor(col);
        startPosition = newPosition;
        scoreReport = scoreDel;
        resetPosition();
    }
	// Update is called once per frame


    public void gotCaught()
    {
        scoreReport(-2);
        resetPosition();
    }

	void Update () 
    {
        move();
        if (Input.GetAxis(paintAxisName) == 1)
        {
            painter.paint();
        }
	}
    void move()
    {
        float xMove = Input.GetAxis(xAxisName);
        float zMove = Input.GetAxis(zAxisName);
        Vector3 newMovement = new Vector3(xMove, 0, zMove);
        newMovement.Normalize(); //ensures diagonal movement is not faster
        newMovement = newMovement * currentSpeed * Time.deltaTime;
        body.MovePosition(transform.position+newMovement);
        body.velocity = new Vector3(0, 0);
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Generator"))
        {
            scoreReport(5);
            resetPosition();
        }
        else if(other.gameObject.CompareTag("Checkpoint"))
        {
            Vector3 newRespawn = other.transform.position;
            newRespawn.y = startPosition.y;
            startPosition = newRespawn;
        }
    }

    void resetPosition()
    {
        transform.position = startPosition;
        body.position = startPosition;
        body.ResetInertiaTensor();
        body.ResetCenterOfMass();
        body.velocity = new Vector3(0,0);
    }
}
