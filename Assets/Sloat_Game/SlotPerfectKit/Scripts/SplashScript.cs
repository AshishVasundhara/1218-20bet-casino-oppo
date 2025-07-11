using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
        SceneManager.LoadSceneAsync("Menu");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
