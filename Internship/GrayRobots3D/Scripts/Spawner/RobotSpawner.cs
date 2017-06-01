using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Core;

public class RobotSpawner : MonoBehaviour {
    public GameObject robotPrefab;
    public Vector3[] spawnPositions;
    public GameObject pathPrefab;
    private GameObject path;
	// Use this for initialization
	void Start () 
    {
        path = (GameObject)Instantiate(pathPrefab);
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            GameObject newRobot = (GameObject)Instantiate(robotPrefab, spawnPositions[i],Quaternion.identity);
            AIRig robotRig = newRobot.GetComponentInChildren<AIRig>();
            robotRig.AI.WorkingMemory.SetItem<GameObject>("currentPath", path);
        }
	}
	
}
