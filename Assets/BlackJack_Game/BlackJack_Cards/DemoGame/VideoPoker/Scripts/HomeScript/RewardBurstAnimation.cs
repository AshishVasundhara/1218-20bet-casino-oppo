using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardBurstAnimation : MonoBehaviour {

	public Animator BurstAnimator;
	// Use this for initialization
	void Start () {
		if (BurstAnimator == null) {
			BurstAnimator = GetComponent<Animator> ();
		}
	}
	public void ShowStartAnimation()
	{
		if (BurstAnimator == null)
		{
			return;
		}
		BurstAnimator.SetTrigger ("Running");	
//		GameObject.Find ("PlayerStart").GetComponent<SpriteRenderer> ().enabled=true;

	}
	public void HideStartAnimation ()
	{
		BurstAnimator.SetBool ("Running", false);	
//		GameObject.Find ("PlayerStart").GetComponent<SpriteRenderer> ().enabled=false;
	}
}
