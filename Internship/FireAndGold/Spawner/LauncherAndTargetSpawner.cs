using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LauncherAndTargetSpawner : MonoBehaviour {
    public GameObject launcherPrefab;
    public GameObject targetPrefab;
    public GameObject goldSpawnerPrefab;

    public float spawnRate = 1;
    public float speed = 20;
    public float speedRange = 2;
    public float angleRange = 1;
    public Vector3 targetAim;
    public List<Vector3> launcherPositions;
    public List<Vector3> goldSpawnerPositions;
    private int numberOfLaunchers;

    private GameObject goldSpawner;
    private GameObject targetAimObject;
    private List<GameObject> launchers;
	// Use this for initialization
	void Start () {
        launchers = new List<GameObject>();

        goldSpawner = (GameObject)Instantiate(goldSpawnerPrefab);
        if (goldSpawnerPositions != null)
        {
            goldSpawner.GetComponent<GoldSpawner>().setVariables(goldSpawnerPositions);
        }
        targetAimObject = (GameObject)Instantiate(targetPrefab);
        targetAimObject.transform.position = targetAim;
        //initializing all the launchers
        numberOfLaunchers = launcherPositions.Count;
        
        for (int i = 0; i < numberOfLaunchers; i++)
        {
            GameObject newLauncher = (GameObject)Instantiate(launcherPrefab,launcherPositions[i],Quaternion.identity);
            newLauncher.GetComponent<LaunchPrefab>().setVariables(spawnRate, speed, speedRange, angleRange,targetAimObject.transform);
            launchers.Add(newLauncher);
        }
	}

    // Update is called once per frame
    void Update()
    {
	
	}
}
