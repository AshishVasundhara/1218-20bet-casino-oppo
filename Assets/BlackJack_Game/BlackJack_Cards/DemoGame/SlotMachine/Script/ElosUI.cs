//using System;
//using CSFramework;
//using DG.Tweening;
//using UnityEngine;
//using UnityEngine.UI;
//using Random = UnityEngine.Random;

//namespace Elona.Slot {
//	public class ElosUI : BaseSlotGameUI {
//		[Serializable]
//		public class Colors {
//			public Gradient freeSpinBG;
//			public Gradient freeSpinBGSlot;
//		}

//		[Header("Elos")] public Elos elos;
////		public ElosShop shop;
//		public Colors colors;

//		public Image background, highlightFreeSpin, backgroundSlot;
//		public Button buttonPlay;
////		public Slider sliderExp;
//		public GameObject payTable;
//		public GameObject[] BGs;
//		private int indexBG;

//		private Tweener _moneyTween;
//		private int lastBalance;

//		private Elos.Assets assets { get { return elos.assets; } }
//		private void OnEnable() {
//		}

//		public override void Initialize() {
//			base.Initialize();
//			slot.callbacks.onAddBalance.AddListener(OnAddBalance);
//			lastBalance = slot.gameInfo.balance;
//			elos.bonusGame.gameObject.SetActive(false);
//		}

//		public override void OnActivated() {
//			base.OnActivated();
//			if (!slot.debug.skipIntro) {
//				SoundController.Sound.Intro ();
//					assets.tweens.tsIntro1.Play ();
//			}
//		}

//		public override void OnRoundStart() {
//			base.OnRoundStart();
//			if (slot.currentMode != slot.modes.freeSpinMode) buttonPlay.interactable = true;
//		}

//		public override void OnReelStart(ReelInfo info) {
//			base.OnReelStart(info);
//			if (info.isFirstReel) {
//				SoundController.Sound.Spin ();
//				SoundController.Sound.SpinLoop ();
//			}
//		}

//		public override void OnReelStop(ReelInfo info) {
//			base.OnReelStop(info);
//			SoundController.Sound.ReelStop ();
//			if (info.isFirstReel) buttonPlay.interactable = false;
//			if (info.isLastReel) {
//				SoundController.Sound.Spin ();
//			}
//		}

//		public override void OnRoundComplete() 
//		{
//			if (slot.gameInfo.roundHits == 0) {
//				SoundController.Sound.Lose ();
//			}
//		}

//		public override void EnableNextLine() {
//			if (!slot.lineManager.EnableNextLine ()) 
//				SoundController.Sound.Beep ();
//			 else 
//				SoundController.Sound.Bet ();
			
//		}

//		public override void DisableCurrentLine() {
//			if (!slot.lineManager.DisableCurrentLine ()) 
//				SoundController.Sound.Beep ();
			
//			else {
//				SoundController.Sound.Bet ();

//			}
//		}

//		public override bool SetBet(int index) {
//			if (!base.SetBet(index)) {
//				SoundController.Sound.Beep ();
//				return false;
//			}
//			SoundController.Sound.Bet ();
//			return true;
//		}

//		public void TogglePayTable() {
//			SoundController.Sound.ClickBtn ();
//			payTable.SetActive(!payTable.activeSelf);
//		}

//		public void ToggleShop() {
//		}

//		public override void ToggleFreeSpin(bool enable) {
//			base.ToggleFreeSpin(enable);
//			if (enable) {
//				assets.particleFreeSpin.Play();
//				backgroundSlot.DOGradientColor(colors.freeSpinBGSlot, 0.6f);
//				background.DOGradientColor(colors.freeSpinBG, 0.6f);
//			} else {
//				assets.particleFreeSpin.Stop();
//				backgroundSlot.DOColor(Color.white, 2f);
//				background.DOColor(Color.white, 2f);
//			}
//		}

//		public override void OnProcessHit(HitInfo info) {
//			base.OnProcessHit(info);
//			SymbolHolder randomHolder = info.hitHolders[Random.Range(0, info.hitHolders.Count)];
//			ElosSymbol symbol = randomHolder.symbol as ElosSymbol;
//			Util.InstantiateAt<ElosEffectBalloon>(assets.effectBalloon, slot.transform.parent, randomHolder.transform).Play(symbol.GetRandomTalk());
//			foreach (SymbolHolder holder in info.hitHolders) info.sequence.Join(ShowWinAnimation(info, holder));
//		}

//		// Winning particle and audio effect when a line is a "hit"
//		public Tweener ShowWinAnimation(HitInfo info, SymbolHolder holder) {
//			return Util.Tween(() => {
//				int coins = (info.hitChains - 2)*(info.hitChains - 2)*(info.hitChains - 2) + 1;

//				if (info.hitSymbol.payType == Symbol.PayType.Normal) {
//					assets.particlePrize.transform.position = holder.transform.position;
//					Util.Emit(assets.particlePrize, coins);
//					if (info.hitChains <= 3)
//					{
//						SoundController.Sound.WinSmall ();
//					}
//					else if (info.hitChains == 4) 
//					{
//						SoundController.Sound.WinMedium ();
//					}
//					else {
//						SoundController.Sound.WinBig ();
//					}
//					if (info.hitChains >= 4) assets.tweens.tsWin.SetText(info.hitChains + "-IN-A-ROW!", info.hitChains*40).Play();
//				} else {
//					SoundController.Sound.WinSpecial ();
//					if (info.hitSymbol.payType == Symbol.PayType.FreesSpin) assets.tweens.tsWinSpecial.SetText("Free Spin!").Play();
//					else assets.tweens.tsWinSpecial.SetText("BONUS!").Play();
//				}
//			});
//		}

//		private int _lastBalance;

//		private void Update() {
//			textMoney.text = "" + DataManager.Instance.Coins;
//			if (_lastBalance != lastBalance) {
//				_lastBalance = lastBalance;
//			}
//		}

//		public override void RefreshMoney() { }

//		public void OnAddBalance(BalanceInfo info) {
//			if (info.amount == 0) return;

//			float duration = 1f;
//			if (info.amount < 0) {
//				SoundController.Sound.Pay ();
//				Util.Emit(assets.particlePay, 3);
//			} else {
//				if (info.hitInfo != null) {
//					if (info.hitInfo.hitChains <= 3) SoundController.Sound.EarnSmall ();
//					else SoundController.Sound.EarnBig ();
//					duration = slot.effects.GetHitEffect(info.hitInfo).duration*0.8f;
//				} else {
//					SoundController.Sound.EarnSmall ();
//				}
//			}

//			Util.InstantiateAt<ElosEffectMoney>(assets.effectMoney, transform).SetText(info.amount, info.hitInfo == null ? "" : info.hitInfo.hitChains + " in a row!").Play(100, 3f);

//			if (_moneyTween != null && _moneyTween.IsPlaying()) _moneyTween.Complete();
//			_moneyTween = DOTween.To(() => lastBalance, x => lastBalance = x, slot.gameInfo.balance, duration).OnComplete(() => { _moneyTween = null; });
//		}
//	}
//}