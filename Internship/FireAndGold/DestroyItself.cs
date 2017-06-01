using UnityEngine;
using System.Collections;

public class DestroyItself : MonoBehaviour {
    public float timeToLive = 2; 
	// Destroys the object after 
	void Start () {
        Destroy(gameObject, timeToLive);
	
	}
	
}
