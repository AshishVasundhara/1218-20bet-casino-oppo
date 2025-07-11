//using System;
//using CSFramework;
//using DG.Tweening;
//using UnityEngine;

//namespace Elona.Slot {
//	/// <summary>
//	/// A main class for Elona Slot(Demo) derived from BaseSlotGame.
//	/// For the most part, it's overriding the base methods to add visual/audio effects.
//	/// </summary>
//	public class Elos : MonoBehaviour {
//		public GameObject Finger;
//		public GameObject OverlayMoney;
//		public CustomSlot slot;
//		public static bool checktut=false;
//		[Serializable]
//		public class Assets {
//			[Serializable]
//			public class Tweens {
//				public TweenSprite BonusTxt, tsIntro1, tsWin, tsWinSpecial;
//			}

//			public Tweens tweens;
//			public ParticleSystem particlePay, particlePrize, particleFreeSpin;
//			public ElosEffectMoney effectMoney;
//			public ElosEffectBalloon effectBalloon;
//		}

//		[Serializable]
//		public class ElonaSlotSetting {
//			public bool allowDebt = true;
//		}

//		public Assets assets;
//		public ElonaSlotSetting setting;
//		public ElosUI ui;
//		public ElosBonusGame bonusGame;
//		public float transitionTime = 3f;
//		public CanvasGroup cg;
//		public GameObject mold;

//		protected void Awake() {
//			slot.callbacks.onReelStart.AddListener(OnReelStart);
//			slot.callbacks.onProcessHit.AddListener(OnProcessHit);
//			Initialize();
//		}

//		public void Initialize() {
//			mold.gameObject.SetActive(false);
//			if (!slot.debug.skipIntro) {
//				cg.alpha = 0;
//				cg.DOFade(1f, transitionTime*0.5f).SetDelay(transitionTime*0.5f);
//			}
//		}

//		private void Update() {
//			if (slot.debug.useDebugKeys) {
//				if (Input.GetKeyDown(KeyCode.Alpha1)) assets.tweens.BonusTxt.Play(0);
//				if (Input.GetKeyDown(KeyCode.Alpha2)) assets.tweens.tsWinSpecial.Play(0);
//				if (Input.GetKeyDown(KeyCode.Alpha3)) assets.tweens.tsIntro1.Play(0);
//				if (Input.GetKeyDown(KeyCode.F10)) slot.AddEvent(new SlotEvent(bonusGame.Activate));
//			}
//			if (checktut) {
//				Finger.SetActive (true);
//				OverlayMoney.SetActive (true);
//			} else {
//				Finger.SetActive (false);
//				OverlayMoney.SetActive (false);
//			}
//		}

//		public void Play() {
//			if (slot.state == CustomSlot.State.Idle && !setting.allowDebt && DataManager.Instance.Coins < slot.gameInfo.roundCost)
//			{
//				if(DataManager.Instance.Coins<5)
//				{
//					Finger.SetActive (true);
//					OverlayMoney.SetActive (true);
//					checktut = true;
//				}
//				SoundController.Sound.Beep ();
//				return;
//			}
//			slot.Play();
//		}

//		public void OnReelStart(ReelInfo info) {  }

//		public void OnProcessHit(HitInfo info) {
//			if (info.hitSymbol.payType == Symbol.PayType.Custom) slot.AddEvent(new SlotEvent(bonusGame.Activate));
//		}
//	}
//}