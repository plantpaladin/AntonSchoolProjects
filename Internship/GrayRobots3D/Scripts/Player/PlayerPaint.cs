using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaint : MonoBehaviour {
    public GameObject paintPrefab;
    public Color baseColor = Color.blue;

    public float cooldown = 2;
    private float cooldownLeft;

    private Renderer playerRenderer;
	// Use this for initialization
	void Awake () 
    {
        playerRenderer = GetComponent<Renderer>();
        playerRenderer.material.color = baseColor;
        cooldownLeft = 0;
	}
    public void setPaintColor(Color newColor)
    {
        baseColor = newColor;
        playerRenderer.material.color = newColor;
    }
	// Update is called once per frame
	void Update () 
    {
        if (cooldownLeft > 0)
        {
            cooldownLeft -= Time.deltaTime;
        }
	}

    public void paint()
    {
        if (cooldownLeft <= 0)
        {
            GameObject newPaint = (GameObject)Instantiate(paintPrefab, transform.position, Quaternion.identity);
            newPaint.GetComponent<Paint>().setVariables(baseColor);
            cooldownLeft = cooldown;
        }
    }
}
