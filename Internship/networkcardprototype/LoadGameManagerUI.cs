using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class LoadGameManagerUI : NetworkManagerHUD {

    void Awake()
    {
        manager = GetComponent<GameManager>();
    }
	
}
