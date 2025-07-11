using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//ads
using UnityEngine.Advertisements;

public class UIAnimation : MonoBehaviour 
{
	public Button VideoAdsBtn;
	public GameObject StartBtn;
	public GameObject finger,overlayMoney;
	public Animator StartAnimator;
	public GameObject overlay;
	public GameObject OptionsObj;
	public GameObject ExitGameObj;
	public Transform target;
	public EaseType easeTypeGoTo = EaseType.EaseInOutBack;

	bool isOpen = false;
	Vector3 oriPos = Vector3.zero;
	public static int isfreeads=0;
	void Update()
	{
        //ads
        //if (Advertisement.IsReady("rewardedVideo"))
        //{
        //    VideoAdsBtn.interactable = true;
        //}
        //else
        //    VideoAdsBtn.interactable = false;
    }
    public void Move(Transform tr) 
	{
		SoundController.Sound.ClickBtn ();

		if (!isOpen)
		{
			if (tr.name == "Quit Game") 
			{

			}
			overlay.SetActive(true);
			oriPos = tr.position;
			TweenParms parms = new TweenParms().Prop("position", target.position).Ease(easeTypeGoTo);
			HOTween.To(tr, .7f, parms);
			isOpen = true;
			ExitGameObj.SetActive (true);
			OptionsObj.SetActive (true);
		}
		else 
		{
			overlay.SetActive(false);
			TweenParms parms = new TweenParms().Prop("position", oriPos).Ease(easeTypeGoTo).OnComplete(OnComplete);
			HOTween.To(tr, .7f, parms);

			ExitGameObj.SetActive (false);
			OptionsObj.SetActive (false);
		}

	}
	void OnComplete() 
	{
		isOpen = false;
	}
	public void StartAnimation ()
	{ 

			StartAnimator.SetBool ("Off", false);
			StartAnimator.SetTrigger ("On");
			GameObject.FindObjectOfType<CardGame> ().ResetBtn ();
			if (SliderScriptBJ._sliderScipt.parentSlider.localScale.y > .9f) 
		    {
				SliderScriptBJ._sliderScipt.ClickBet ();
			}
	
			

	}

	public void HideAnimation ()
	{
		StartAnimator.SetBool ("On", false);
		StartAnimator.SetTrigger ("Off");

	}

	private void ResetAnimationParameters ()
	{
		if (StartAnimator == null) {
			return;
		}
		StartAnimator.SetBool ("On", false);
		StartAnimator.SetBool ("Off", false);
	}

	public void Show_Bar()
	{
		SliderScriptBJ._sliderScipt.ClickBet();
	}
	public void BackToHomeScene()
	{
        //show ads
        //AdmobBannerController.Instance.ShowInterstitial();
        //AdManagerUnity.Instance.ShowAd("video");
        SoundController.Sound.ClickBtn ();
		SceneManager.LoadScene ("HomeScene");
	}
	public void ShowUnityAds()
	{
		if (DataManager.Instance.Coins <= 0) {
			isfreeads=1;
			SoundController.Sound.ClickBtn ();
            //show ads
            //if (DataManager.Instance._rewardAdsType == DataManager.RewardedVideoAdsType.UnityAds)
            //    //AdManagerUnity.Instance.ShowAd("rewardedVideo");
            //else
            //    AdmobBannerController.Instance.ShowRewardedAd();
        } else
			SoundController.Sound.DisactiveButtonSound ();
	}
	public void ShowUnityAdsCallBack()
	{
		SceneManager.LoadScene ("Blackjack");
		isfreeads=0;
		SoundController.Sound.CallBackSuccess ();
		DataManager.Instance.AddCoins (Random.Range(50,100));
	
	}
	public void ReshowStartBtn()
	{
		SoundController.Sound.CallBackSuccess ();
		SceneManager.LoadScene ("Blackjack");
	}


}
