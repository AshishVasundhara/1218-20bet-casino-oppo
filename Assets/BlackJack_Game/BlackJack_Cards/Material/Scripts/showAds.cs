using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using ChartboostSDK;

public class showAds : MonoBehaviour {

    public static showAds instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

	// Use this for initialization
	void Start () {
        //Chartboost.cacheInterstitial(CBLocation.Default);
        //ShowInterstitial();
	}

    public void ShowInterstitialFreeMoney()
    {
        //if (Chartboost.hasInterstitial(CBLocation.Default))
        //{
        //    Chartboost.showInterstitial(CBLocation.Default);
            int atmMoney = PlayerPrefs.GetInt("playerCoin", 1000);
            atmMoney += 200;
            PlayerPrefs.SetInt("playerCoin", atmMoney);
      //  }
    }

    //public void ShowInterstitial()
    //{
    //    if (Chartboost.hasInterstitial(CBLocation.Default))
    //    {
    //        Chartboost.showInterstitial(CBLocation.Default);
    //    }
    //}

	// Update is called once per frame
	void Update () {
		
	}
}
