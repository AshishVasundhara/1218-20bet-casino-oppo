//using System;
//using UnityEngine;
//using GoogleMobileAds.Api;

//using UnityEngine.Advertisements;

//public class AdmobBannerController : MonoBehaviour 
//{
//	//	#if  GOOGLE_MOBILE_ADS
//	private static AdmobBannerController instance;
//	public static AdmobBannerController Instance{
//		get{ 
//			return instance;
//		}
//	}
//    private InterstitialAd interstitial;
//    private RewardedAd rewardedAd;
//    private float deltaTime = 0.0f;

//    //Insert your ads id here
//#if UNITY_IOS
//    public string appId = "";
//	public string Admob_InstertialID = "";
//    public string Admob_RewardedID= "";
//#elif UNITY_ANDROID
//    public string appId = "";
//	public string Admob_InstertialID = "";
//    public string Admob_RewardedID= "";
//#endif
//    public void Awake() {		
//		if (instance == null) {
//			instance = this;
//			DontDestroyOnLoad (gameObject);
//		} else if (instance != this) {
//			Destroy (gameObject);
//		}
//    }
//    void Start()
//    {
//        MobileAds.SetiOSAppPauseOnBackground(true);
//        // Initialize the Google Mobile Ads SDK.
//        MobileAds.Initialize(appId);
//        this.CreateAndLoadRewardedAd();
//        this.RequestInterstitial();
//    }
//    public void Update()
//    {
//        // Calculate simple moving average for time to render screen. 0.1 factor used as smoothing
//        // value.
//        this.deltaTime += (Time.deltaTime - this.deltaTime) * 0.1f;
//    }
//    // Returns an ad request with custom ad targeting.
//    private AdRequest CreateAdRequest()
//    {
//        return new AdRequest.Builder()
//            .AddTestDevice(AdRequest.TestDeviceSimulator)
//            .AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
//            .AddKeyword("game")
//            .SetGender(Gender.Male)
//            .SetBirthday(new DateTime(1985, 1, 1))
//            .TagForChildDirectedTreatment(false)
//            .AddExtra("color_bg", "9B30FF")
//            .Build();
//    }

//    public void RequestInterstitial() {
//        // Clean up interstitial ad before creating a new one.
//        if (this.interstitial != null)
//        {
//            this.interstitial.Destroy();
//        }

//        // Create an interstitial.
//        this.interstitial = new InterstitialAd(Admob_InstertialID);

//        // Register for ad events.
//        this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
//        this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
//        this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
//        this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
//        this.interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;

//        // Load an interstitial ad.
//        this.interstitial.LoadAd(this.CreateAdRequest());
//    }
//	public void ShowInterstitial() 
//	{
//        if (this.interstitial != null)
//        {
//            if (this.interstitial.IsLoaded())
//            {
//                this.interstitial.Show();
//                this.RequestInterstitial();
//            }
//            else
//            {
//                //AdManagerUnity.Instance.ShowAd("video");
//            }
//        }
//	}
//    public void CreateAndLoadRewardedAd()
//    {
//        // Create new rewarded ad instance.
//        this.rewardedAd = new RewardedAd(Admob_RewardedID);

//        // Called when an ad request has successfully loaded.
//        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
//        // Called when an ad request failed to load.
//        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
//        // Called when an ad is shown.
//        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
//        // Called when an ad request failed to show.
//        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
//        // Called when the user should be rewarded for interacting with the ad.
//        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
//        // Called when the ad is closed.
//        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

//        // Create an empty ad request.
//        AdRequest request = this.CreateAdRequest();
//        // Load the rewarded ad with the request.
//        this.rewardedAd.LoadAd(request);
//    }
//    public void ShowRewardedAd()
//    {
//        if (this.rewardedAd.IsLoaded())
//        {
//            this.rewardedAd.Show();
//            this.CreateAndLoadRewardedAd();
//        }
//        else
//        {
//            //AdManagerUnity.Instance.ShowAd("rewardedVideo");
//        }
//    }

//    #region Interstitial callback handlers

//    public void HandleInterstitialLoaded(object sender, EventArgs args)
//    {
//    }

//    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {
//    }

//    public void HandleInterstitialOpened(object sender, EventArgs args)
//    {
//    }

//    public void HandleInterstitialClosed(object sender, EventArgs args)
//    {
//    }

//    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
//    {
//    }

//    #endregion

//    #region RewardedAd callback handlers

//    public void HandleRewardedAdLoaded(object sender, EventArgs args)
//    {
//    }

//    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
//    {
//    }

//    public void HandleRewardedAdOpening(object sender, EventArgs args)
//    {
//    }

//    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
//    {
//    }

//    public void HandleRewardedAdClosed(object sender, EventArgs args)
//    {
//    }

//    public void HandleUserEarnedReward(object sender, Reward args)
//    {
//        string type = args.Type;
//        double amount = args.Amount;
//        if (DailyEvent.isFreeAd == 1)
//        {
//            GameObject.FindObjectOfType<DailyEvent>().FreeAdsCallBack();
//        }
//        else if (UIAnimation.isfreeads == 1)
//        {
//            GameObject.FindObjectOfType<UIAnimation>().ShowUnityAdsCallBack();
//        }
//        else if (DailyEvent.isFreeAd == 2)
//        {
//            DailyEvent.isFreeAd = 0;
//            DataManager.Instance.AddCoins(100);
//            GameObject.FindObjectOfType<UIAnimation>().ReshowStartBtn();
//        }
//        else if (DailyEvent.isFreeAd == 0 && SlotSettingUI.IsFreeAds == 0)
//        {
//            GameObject.FindObjectOfType<RewadControl>().GetMoreCoin();
//        }
//        else if (SlotSettingUI.IsFreeAds == 1)
//        {
//            Elona.Slot.Elos.checktut = false;
//            SlotSettingUI.IsFreeAds = 0;
//            DataManager.Instance.AddCoins(100);
//            SoundController.Sound.CallBackSuccess();
//        }
//    }

//    #endregion

//}
