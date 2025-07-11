using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("roulettegame");
    }

    public void SetBackToIK()
    {
        PlayerPrefs.SetInt("totalMoney", 10000);
    }
}