using UnityEngine;
using System.Collections;

public class LaunchPrefab : MonoBehaviour {
    //launches the prefab 
    public GameObject spawnPrefab;
    private float speed = 20;
    private float speedRange = 2;
    private float angleRange = 1;
    private Transform targetAim;//this is the target the different shooters are aiming at, it should be above
    //the target beacuse the fire is affected by gravity

	// Use this for initialization
	public void setVariables (float spawnRate,float newSpeed,float newSpeedRange,float newAngleRange,Transform newTargetAim) {
        speed = newSpeed;
        speedRange = newSpeedRange;
        angleRange = newAngleRange;
        targetAim = newTargetAim;
        InvokeRepeating("SpawnObject", 1, spawnRate);
	}
    void SpawnObject() {
        float randomAngleChange = Random.Range(-angleRange, angleRange);
        transform.LookAt(targetAim);
        transform.Rotate(Vector3.up * randomAngleChange);
        GameObject newShot = (GameObject)Instantiate(spawnPrefab, transform.position, transform.rotation);
        
        
        newShot.rigidbody.velocity = transform.forward * (speed + Random.Range(-speedRange, speedRange));
        
        //the launcher is looking towards the target so the launched object inherits its rotation
        //it is then given a velocity
    }

	// Update is called once per frame
	void Update () {
	
	}
}
