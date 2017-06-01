using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    public void setVariables(Color newColor)
    {
        Renderer paintRenderer = GetComponent<Renderer>();
        paintRenderer.material.color = newColor;
    }

    public void dePaint()
    {
        Destroy(gameObject);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
