using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GoldSpawner : MonoBehaviour {
    
    public GameObject spawnPrefab;
    public float speed = 10;
    public float spawnDelay = 1;//part of the movement which is random, a value of 1 is completly random
    public List<Vector3> positions;
    private int selected;
    private float maxSpeed;//used to reset the speed after it stops
    // Use this for initialization
	void Start () {
        if (positions.Count == 0)
        {//a safeguard against starting the game without initializing a set of points to follow
            positions.Add(new Vector3(0,0,0));
        }
        selected = 0;
        maxSpeed = speed;//
         
	}
	
	// Fixedupdate is used so movement doesnt depend on framerate
	void FixedUpdate () {
        float step = speed * Time.deltaTime;
        
        Vector3 newPosition = Vector3.MoveTowards(transform.position,positions[selected], step);
    
        transform.position = newPosition;
        if (Vector3.Distance(transform.position, positions[selected]) < step)
        {
            float notThesame = selected;//makes certain the object actually moves
            selected = Random.Range(0, positions.Count-1);
            if (selected == notThesame)
            {
                selected += 1;//takes the next one in array if it is the same 
                selected = selected % positions.Count;//next one becomes 0 if the current is the highest
            }
            speed = 0;
            Invoke("spawnObject", spawnDelay);
        }

	}
    void spawnObject()
    {
        speed = maxSpeed;
        Instantiate(spawnPrefab, transform.position, Quaternion.identity);
    }


    public void setVariables(List<Vector3> goldSpawnerPositions)
    {
        if (goldSpawnerPositions.Count!=0)
        {//checks if the list has been set in the inspector
            positions = goldSpawnerPositions;
        }
    }
}
