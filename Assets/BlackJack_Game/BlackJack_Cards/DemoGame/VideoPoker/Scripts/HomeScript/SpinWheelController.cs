using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpinWheelController : MonoBehaviour 
{
	public GameObject SpinNormal;
	public GameObject SpinAfterWatchAds;

	void Update () 
	{
		if (DataManager.Instance.Coins >= 100) {
			SpinNormal.SetActive (true);
			SpinAfterWatchAds.SetActive (false);
		} else 
		{
			SpinNormal.SetActive (false);
			SpinAfterWatchAds.SetActive (true);
		}
	}

	public void SpinResult1()
	{
		SoundController.Sound.VideoPoker_Win ();
		DataManager.Instance.AddCoins (100);
		Debug.Log ("1");
	}
	public void SpinResult2()
	{
		SoundController.Sound.VideoPoker_Lose ();
		Debug.Log ("2");
	}
	public void SpinResult3()
	{
		SoundController.Sound.VideoPoker_Win ();
		DataManager.Instance.AddCoins (500);
		Debug.Log ("3");
			
	}
	public void SpinResult4()
	{	
		SoundController.Sound.VideoPoker_Lose ();	
		Debug.Log ("4");
	}
	public void SpinResult5()
	{
		SoundController.Sound.VideoPoker_Win ();
		DataManager.Instance.AddCoins (1000);
		Debug.Log ("5");
	}
	public void SpinResult6()
	{
		SoundController.Sound.VideoPoker_Lose ();
		Debug.Log ("6");
	}
	public void SpinResult7()
	{	
		SoundController.Sound.VideoPoker_Win ();
		DataManager.Instance.AddCoins (50000);
		Debug.Log ("7");
	}
	public void SpinResult8()
	{
		SoundController.Sound.VideoPoker_Lose ();
		Debug.Log ("8");
	}
	public void SpinResult9()
	{
		SoundController.Sound.VideoPoker_Win ();
		DataManager.Instance.AddCoins (2000);
		Debug.Log ("9");
	}
	public void SpinResult10()
	{
		SoundController.Sound.VideoPoker_Lose ();
		Debug.Log ("10");
	}

}
