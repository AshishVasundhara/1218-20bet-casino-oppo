//using UnityEngine;
//using System.Collections;
//using Holoville.HOTween;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;
////ads
//using UnityEngine.Advertisements;
//public class SlotSettingUI : MonoBehaviour 
//{
//	public Button VideoAdsBtn;
//	public GameObject finger,overlayMoney;
//	public GameObject overlay;
//	public GameObject OptionsObj;
//	public GameObject ExitGameObj;
//	public Transform target;
//	public EaseType easeTypeGoTo = EaseType.EaseInOutBack;

//	bool isOpen = false;
//	Vector3 oriPos = Vector3.zero;
//	public GameObject SettingBtn;
//	public GameObject QuitBtn;
//	public GameObject FreeCoins;
//	public static int IsFreeAds=0;
//	void Awake()
//	{
//		SettingBtn.SetActive (false);
//		QuitBtn.SetActive (false);
//		FreeCoins.SetActive (false);
//		Invoke ("SoundBtnFunction",2.5f);
//	}
//	void Update()
//	{
//        //ads
//        //if (Advertisement.IsReady("rewardedVideo"))
//        //{
//        //    VideoAdsBtn.interactable = true;
//        //}
//        //else
//        //    VideoAdsBtn.interactable = false;
//    }
//	public void SoundBtnFunction()
//	{
//		SettingBtn.SetActive (true);
//		QuitBtn.SetActive (true);
//		FreeCoins.SetActive (true);
//	}
//	public void LoadHomeScene()
//	{
//        //show ads
//        //AdmobBannerController.Instance.ShowInterstitial();
//        //AdManagerUnity.Instance.ShowAd("video");
//        SoundController.Sound.ClickBtn ();
//		SceneManager.LoadScene ("HomeScene");
//	}
//	public void Move(Transform tr) 
//	{
//		SoundController.Sound.ClickBtn ();

//		if (!isOpen)
//		{
//			if (tr.name == "Quit Game") 
//			{

//			}
//			overlay.SetActive(true);
//			oriPos = tr.position;
//			TweenParms parms = new TweenParms().Prop("position", target.position).Ease(easeTypeGoTo);
//			HOTween.To(tr, .7f, parms);
//			isOpen = true;
//			ExitGameObj.SetActive (true);
//			OptionsObj.SetActive (true);
//		}
//		else 
//		{
//			overlay.SetActive(false);
//			TweenParms parms = new TweenParms().Prop("position", oriPos).Ease(easeTypeGoTo).OnComplete(OnComplete);
//			HOTween.To(tr, .7f, parms);

//			ExitGameObj.SetActive (false);
//			OptionsObj.SetActive (false);
//		}

//	}
//	void OnComplete() 
//	{
//		isOpen = false;
//	}

//	public void BackToHomeScene()
//	{
//        //show ads
//        //AdManagerUnity.Instance.ShowAd("video");
//        //AdmobBannerController.Instance.ShowInterstitial();
//        SoundController.Sound.ClickBtn ();
//		SceneManager.LoadScene ("HomeScene");
//	}
//	public void ShowUnityAds()
//	{
//		if (DataManager.Instance.Coins < 5) {
//			IsFreeAds = 1;
//			SoundController.Sound.ClickBtn ();
//            //show ads
//            //if (DataManager.Instance._rewardAdsType == DataManager.RewardedVideoAdsType.UnityAds)
//            //    //AdManagerUnity.Instance.ShowAd("rewardedVideo");
//            //else
//            //    AdmobBannerController.Instance.ShowRewardedAd();
//        } else
//			SoundController.Sound.DisactiveButtonSound ();
//	}
//	public void ReshowStartBtn()
//	{
//		SoundController.Sound.CallBackSuccess ();
//		SceneManager.LoadScene ("SlotMachine");
//	}


//}
