using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jetonScript : MonoBehaviour {

    public int value;
    int val = 0;
	// Use this for initialization
	void Start () {
                PlayerPrefs.SetInt("v",0);
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)){
            // Whatever you want it to do.
            if (gameController.instance.playerCoin - value >= 0)
            {

                if(PlayerPrefs.GetInt("v") < 500)
                {
                    PlayerPrefs.SetInt("v",PlayerPrefs.GetInt("v",0)+value);
                }
               
                val += value;

                print("PAID : " + value);
                print("Val : " + PlayerPrefs.GetInt("v"));

                
                if (PlayerPrefs.GetInt("v") < 500)
                {
                    gameController.instance.addBet(value);
                }
                else if (PlayerPrefs.GetInt("v") == 500 && gameController.instance.betMaxText.activeSelf == false)
                {

                    gameController.instance.addBet(value);
                    gameController.instance.betMaxText.SetActive(true);
                }
                else if (gameController.instance.betMaxText.activeSelf == false)
                {
                    PlayerPrefs.SetInt("v", PlayerPrefs.GetInt("v", 0) - value);
                   
                }
            }
        }
    }
}
