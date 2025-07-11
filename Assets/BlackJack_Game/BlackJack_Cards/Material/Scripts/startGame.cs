using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void startGameNow()
    {
        Application.LoadLevel("GameScene");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
