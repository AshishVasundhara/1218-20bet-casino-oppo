//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI;
//using UnityEngine.Advertisements;

//public class //AdManagerUnity : MonoBehaviour 
//{
////	#if UNITY_ADS
//	private static //AdManagerUnity instance;
//    public static //AdManagerUnity Instance
//    {
//        get
//        {
//            return instance;
//        }
//    }
//    //Insert you Unity Ads ID here
//#if UNITY_IOS
//	[SerializeField] string gameID = "1757295";
//#elif UNITY_ANDROID
//    [SerializeField] string gameID = "2652113";
//	#endif
//	void Awake()
//	{
//        if (instance == null)
//        {
//            instance = this;
//            DontDestroyOnLoad(gameObject);
//        }
//        else if (instance != this)
//        {
//            Destroy(gameObject);
//        }
//        Advertisement.Initialize(gameID, false);
//    }

//	public void ShowAd(string zone = "")
//	{
//		#if UNITY_EDITOR
//		StartCoroutine(WaitForAd ());
//		#endif

//		if (string.Equals (zone, ""))
//			zone = null;

//		ShowOptions options = new ShowOptions ();
//		options.resultCallback = AdCallbackhandler;

//		if (Advertisement.IsReady (zone))
//			Advertisement.Show (zone, options);
//	}

//	void AdCallbackhandler (ShowResult result)
//	{
//		switch(result)
//		{
//		case ShowResult.Finished:
//			Advertisement.Initialize (gameID, false);
//			if (DailyEvent.isFreeAd == 1)
//			{
//				GameObject.FindObjectOfType<DailyEvent> ().FreeAdsCallBack ();
//			} 
//			else if (UIAnimation.isfreeads == 1)
//			{	
//				GameObject.FindObjectOfType<UIAnimation> ().ShowUnityAdsCallBack ();
//			}
//			else if (DailyEvent.isFreeAd == 2)
//			{	
//				DailyEvent.isFreeAd = 0;
//				DataManager.Instance.AddCoins (Random.Range(50,100));
//				GameObject.FindObjectOfType<UIAnimation> ().ReshowStartBtn ();
//			}
//            else if (SlotSettingUI.IsFreeAds == 1)
//            {
//                Elona.Slot.Elos.checktut = false;
//                SlotSettingUI.IsFreeAds = 0;
//                DataManager.Instance.AddCoins(Random.Range(50, 100));
//                 SoundController.Sound.CallBackSuccess();
//            }
//            else if (DailyEvent.isFreeAd == 0 && SlotSettingUI.IsFreeAds==0 && QuitRateShareAds.Instance==null)
//			{	
//				GameObject.FindObjectOfType<RewadControl> ().GetMoreCoin ();
//			}
//			break;
//		case ShowResult.Skipped:
//			Advertisement.Initialize (gameID, false);
//			Debug.Log ("Ad skipped. Son, I am dissapointed in you");
//			break;
//		case ShowResult.Failed:
//			Advertisement.Initialize (gameID, false);
//            AdmobBannerController.Instance.ShowRewardedAd();
//            break;
//		}
//	}

//	IEnumerator WaitForAd()
//	{
//		float currentTimeScale = Time.timeScale;
//		Time.timeScale = 0f;
//		yield return null;

//		while (Advertisement.isShowing)
//			yield return null;

//		Time.timeScale = currentTimeScale;
//	}
////#endif
//}