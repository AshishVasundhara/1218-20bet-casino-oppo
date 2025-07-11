using UnityEngine;
using System.Collections;

public class QuitRateShareAds : MonoBehaviour
{
	public string URLRate;
    public static QuitRateShareAds Instance;
    void Start()
    {
        Instance = this;
    }
    public void QuitGame()
	{
		Application.Quit ();
	}
    public void ShowInsterstial()
    {
       //AdmobBannerController.Instance.ShowInterstitial();
    }
    public void ShowRewarded()
    {
        //AdmobBannerController.Instance.ShowRewardedAd();
    }
    public void RateGame()
	{
		//Insert your game on store
		SoundController.Sound.ClickBtn ();
		#if UNITY_ANDROID
		Application.OpenURL(URLRate);
		#elif UNITY_IOS
		Application.OpenURL (URLRate);
		#endif
		//
	}
}
