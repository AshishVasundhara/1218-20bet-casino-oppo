using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class LevelManager : MonoBehaviour
{
	public static LevelManager THIS;
	Animator animconvert;
	public GameObject bgOverlay;
	void Start () 
	{
		THIS = this;
		animconvert = GetComponentInChildren<Animator> ();
		StartCoroutine (InLoadScene ());

	}
	void Update()
	{

	}

	//-------------AUTO LOAD SCENE WHEN FIRST SCENCE------------------
	public IEnumerator InLoadScene()
	{
		animconvert.SetTrigger ("First");
		bgOverlay.SetActive(true);
		SoundController.Sound.Out ();
		yield return new WaitForSeconds (1f);
		animconvert.SetTrigger ("Out");
		SoundController.Sound.In ();
		MusicController.Music.BG_Home ();
		bgOverlay.SetActive(false);
	}

	public void VideoPokerScene()
	{
		MusicController.Music.BG_VideoPoker ();
		SoundController.Sound.ClickBtn ();
		SceneManager.LoadScene ("JackOrBetter");
		Debug.Log ("VideoPoker Scene");
	}
	public void BlackJackScene()
	{
		MusicController.Music.BG_BlackJack ();
		SoundController.Sound.ClickBtn ();
		SceneManager.LoadScene ("Blackjack");
	}
	public void SlotMachineScene()
	{
		MusicController.Music.BG_SlotMachine ();
		SoundController.Sound.ClickBtn ();
		SceneManager.LoadScene ("SlotMachine");
		Debug.Log ("SlotMachine Scene");
	}

	public void HomeScene()
	{
		MusicController.Music.BG_Home ();
		SoundController.Sound.ClickBtn ();
		SceneManager.LoadScene ("HomeScene");
	}



}
