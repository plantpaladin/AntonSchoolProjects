using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Core;

public class AdvancedRobotSpawner : MonoBehaviour
{
    public GameObject robotPrefab;
    public Vector3[] spawnPositions;
    public GameObject[] pathPrefabs;

    //public List<GameObject> paths;
    ///public List<GameObject> robots;
    void Awake()
    {
        //robots = new List<GameObject>();
        //paths = new List<GameObject>();
        Debug.Assert(spawnPositions.Length == pathPrefabs.Length);
        for (int i = 0; i < spawnPositions.Length; i++)
        {
           
            GameObject newRobot = (GameObject)Instantiate(robotPrefab,spawnPositions[i],Quaternion.identity);
            GameObject newPath = (GameObject)Instantiate(pathPrefabs[i]);
            AIRig robotRig = newRobot.GetComponentInChildren<AIRig>();
            robotRig.AI.WorkingMemory.SetItem<GameObject>("currentPath", newPath);
            //paths.Add(newPath);//necessary to add the objects to a list to prevent some strange removal
            //robots.Add(newRobot);//of variables
        }
    }

}
