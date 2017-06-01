using UnityEngine;
using System.Collections;

public class CameraAndLightSpawner : MonoBehaviour {
    public GameObject cameraPrefab;
    public GameObject lightPrefab;

    private GameObject camera;
    private GameObject light;
	// Use this for initialization
	void Start () {
        camera = (GameObject)Instantiate(cameraPrefab);
        light = (GameObject)Instantiate(lightPrefab);
	
	}
}
