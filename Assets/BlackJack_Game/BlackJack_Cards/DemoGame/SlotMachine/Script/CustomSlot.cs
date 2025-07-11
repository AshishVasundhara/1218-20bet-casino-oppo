//using System;
//using System.Collections.Generic;
//using DG.Tweening;
//using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.UI;

//namespace CSFramework {
//	/// <summary>
//	/// The main class of the CustomSlots Framework.
//	/// </summary>
//	public class CustomSlot : MonoBehaviour {
//		public Button AutoSpinBtn ;
//		public GameObject AutoSpinBtn1;
//		public enum State {
//			NotStarted,
//			Idle,
//			SpinStarting,
//			Spinning,
//			SpinStopping,
//			Result,
//		}
//		public CustomSlot slot;
//		[Serializable]
//		public class Callbacks {
//			public UnityEvent onActivated;
//			public UnityEvent onDeactivated;
//			public UnityEvent onRoundStart;
//			public ReelInfo onReelStart;
//			[Hide] public UnityEvent onNewSymbolAppear;
//			public ReelInfo onReelStop;
//			[Hide] public UnityEvent onRoundInterval;
//			public HitInfo onProcessHit;
//			public UnityEvent onRoundComplete;
//			[Hide] public UnityEvent onSlotStateChange;
//			public SlotModeInfo onSlotModeChange;
//			[Hide] public BalanceInfo onAddBalance;
//			public LineInfo onLineSwitch;
//		}

//		public SkinManager skin;
//		public LineManager lineManager;
//		public SymbolManager symbolManager;

//		[Hide] public SymbolGen symbolGen;
//		[Hide] public GridLayoutGroup layoutReel;
//		[Hide] public GridLayoutGroup layoutRow;
//		[Hide] public RectTransform mainScreen;

//		[Space] public SlotConfig config;
//		[Space] public SlotModeManager modes;
//		[Space] public SlotEffectManager effects;
//		[Space] public Callbacks callbacks;
//		[Space] public SlotLayouter layout;
//		[Space] public SlotDebug debug;

//		[HideInInspector] public Reel[] reels;
//		[HideInInspector] public Row[] rows;

//		private bool isInitialized;
//		private int currentReelIndex = 0;
//		private Queue<SlotEvent> events = new Queue<SlotEvent>();

//		public GameInfo gameInfo { get; private set; }
//		public State state { get; private set; }
//		public SlotEvent currentEvent { get; private set; }
//		public SlotMode currentMode { get { return modes.current; } }
//		public bool isIdle { get { return !isLocked && state == State.Idle; } }
//		public bool isLocked { get { return currentEvent != null || events.Count > 0; } }
//		private void Awake() { if (config.autoActivate) Activate(); }

//		public void Initialize() {
//			if (isInitialized) return;
//			isInitialized = true;
//			gameInfo = new GameInfo(this);
//			if (!config.advanced.skipStartupValidation) Validate();
//			modes.Initialize();
//			layout.SetActiveLayout(false);
//			lineManager.SwitchAllLines(false, true);
//		}

//		public void Activate() {
//			Initialize();
//			Time.timeScale = 1f;
//			gameObject.SetActive(true);
//			effects.transitionIn.Play(this, false, _Activate);
//		}

//		private void _Activate() {
//			if (!debug.skipIntro) effects.introAnimation.Play(this);
//			if (state == State.NotStarted && config.autoStartRound) AddEvent(StartRound);
//			callbacks.onActivated.Invoke();
//		}

//		public void Deactivate(bool destroy = false) { effects.transitionOut.Play(this, true, () => { _Deactivate(destroy); }); }

//		private void _Deactivate(bool destroy) {
//			callbacks.onDeactivated.Invoke();
//			if (destroy) Destroy(gameObject);
//			else gameObject.SetActive(false);
//		}

//		/// <summary>
//		/// Validate() should be called when a slot needs to refresh its data and layout.
//		/// (Number of reels and rows, adding/removing symbols and lines etc)
//		/// </summary>
//		public void Validate() {
//			symbolManager.Validate(this);
//			lineManager.Validate(this);
//			reels = layoutReel.transform.GetComponentsInChildren<Reel>();
//			rows = layoutRow.transform.GetComponentsInChildren<Row>();
//			foreach (Reel reel in reels) reel.Validate(this);
//		}

//		/// <summary>
//		/// Queue an event to CustomSlot's event system.
//		/// </summary>
//		public SlotEvent AddEvent(SlotEvent e) {
//			events.Enqueue(e);
//			return e;
//		}

//		public SlotEvent AddEvent(Sequence sequence) { return AddEvent(new EventTweenSequence(sequence)); }
//		public SlotEvent AddEvent(float duration, TweenCallback onStart = null, TweenCallback onComplete = null) { return AddEvent(Util.Sequence(duration, onStart, onComplete)); }
//		public SlotEvent AddEvent(TweenCallback action) { return AddEvent(Util.Sequence(0, action)); }
//		public SlotEvent AddEvent(Tweener tween) { return AddEvent(DOTween.Sequence().Join(tween)); }

//		private void SwitchState(State newState) {
//			state = newState;
//			callbacks.onSlotStateChange.Invoke();
//		}

//		public void SwitchMode() { if (state == State.Idle) modes.SwitchMode(); }

//		private void Update() {
//			if (DataManager.Instance.Coins >= slot.gameInfo.roundCost) {
//				AutoSpinBtn.interactable = true;
//				if (checkClickAutoPlay > 0) {
//					currentMode.forcePlay = true;
//					checkClickAutoPlay = 0;
//				}
////				else {
////					currentMode.forcePlay = false;
////				}
////				currentMode.forcePlay = true;
//			} else {
//				AutoSpinBtn.interactable = false;
//				currentMode.forcePlay = false;
//				AutoSpinBtn1.SetActive (false);
//			}
//			// If there's an active event, skip all other updates until the event is finished.
//			if (currentEvent != null) {
//				currentEvent.Update();
//				if (currentEvent.isDeactivated) currentEvent = null;
//				else return;
//			}

//			if (events.Count > 0) {
//				currentEvent = events.Dequeue();
//				currentEvent.Activate();
//				return;
//			}

//			switch (state) {
//				case State.Idle:
//					if (currentMode.forcePlay) StartSpin();
//					debug.OnUpdate();
//					break;

//				case State.SpinStarting:

//					break;

//				case State.Spinning:

//					break;

//				case State.SpinStopping:

//					break;

//				case State.Result:
//					foreach (Line line in lineManager.lines) if (line.hitInfo.ProcessHitCheck()) return;
//					foreach (HitInfo hitInfo in gameInfo.scatterHitInfos) if (hitInfo.ProcessHitCheck()) return;
//					gameInfo.OnRoundComplete();
//					SwitchState(State.NotStarted);
//					callbacks.onRoundComplete.Invoke();
//					if (config.autoStartRound) StartRound();
//					break;
//			}
//		}

//		public void StartRound() {
//			gameInfo.OnStartRound();
//			SwitchState(State.Idle);
//			SwitchMode();
//			lineManager.OnStartRound();
//			callbacks.onRoundStart.Invoke();
//			if (currentMode.forcePlay) {
//				if(DataManager.Instance.Coins >= slot.gameInfo.roundCost)
//				{
//					Play();
//				}
//				else return;
//			}
				
//		}

//		/// <summary>
//		/// Starts spinning reels if State is idle, and stops them if they are spinning.
//		/// </summary>
//		public void Play() {
//			if (isLocked) return;
//			switch (state) {
//				case State.Idle:
//					StartSpin();
//					break;

//				case State.Spinning:
//					if (currentMode.spinMode == SlotMode.SpinMode.ManualStopAll) StopSpin();
//					if (currentMode.spinMode == SlotMode.SpinMode.ManualStopOne) StopReel();
//					break;
//			}
//		}
//		int checkClickAutoPlay=0;
//		public void AutoPlayGame()
//		{
//			checkClickAutoPlay++;
//			currentMode.forcePlay = true;
//		}
//		public void DisactiveAutoPlayGame()
//		{
			
//			currentMode.forcePlay = false;
//		}

//		/// <summary>
//		/// Starts spinning all the reels in a sequence and changes the State to "Intro".
//		/// </summary>
//		public void StartSpin(float duration = 0) {
//			if (!isIdle) return;
//			SwitchState(State.SpinStarting);
//			if (duration == 0) duration = currentMode.spinMode == SlotMode.SpinMode.AutoStop ? currentMode.autoStopTime + 0.1f : 0;
//			float _delay = debug.fastSpin ? 0 : currentMode.spinStartDelay;
//			if (debug.alwaysMaxLines) lineManager.SwitchAllLines(true, true);
//			currentReelIndex = 0;

//			Sequence sequence = DOTween.Sequence();
//			foreach (Reel reel in reels) sequence.Append(Util.Tween(_delay, reel.Spin));
//			sequence.Append(currentMode.reelAccelerateTime, OnIntroComplete);
//			AddEvent(sequence);

//			gameInfo.OnStartSpin();

//			if (duration > 0) AddEvent(duration, null, StopSpin);
//		}

//		/// <summary>
//		/// Stops a spinning reel . If the reel is the last spinning reel, CustomSlot will start performing Hit Check.
//		/// </summary>
//		public void StopReel() {
//			reels[currentReelIndex].Stop();
//			currentReelIndex++;
//			if (currentReelIndex >= reels.Length) OnStopSpin();
//		}

//		/// <summary>
//		/// Stops all the spinning reels and changes the State to "Outro". When all the reels stop animating, Hit Check will be performed on each line.
//		/// </summary>
//		public void StopSpin() {
//			float _delay = debug.fastSpin ? 0 : currentMode.spinStopDelay;
//			for (int i = currentReelIndex; i < reels.Length; i++) reels[i].Invoke("Stop", (i - currentReelIndex)*_delay);
//			OnStopSpin((reels.Length - currentReelIndex)*_delay);
//		}

//		private void OnStopSpin(float delay = 0) {
//			Invoke("OnOutroComplete", delay + currentMode.reelStopTime + 0.1f);
//			SwitchState(State.SpinStopping);
//		}

//		private void OnIntroComplete() { SwitchState(State.Spinning); }

//		private void OnOutroComplete() {
//			callbacks.onRoundInterval.Invoke();
//			SwitchState(State.Result);
//		}

//		internal void ProcessHit(HitInfo info) {
//			info.payout = info.hitSymbol.GetPayAmount(info.hitChains);

//			gameInfo.AddHit();

//			if (info.hitSymbol.payType == Symbol.PayType.Normal) {
//				gameInfo.AddBalance(info.payout*gameInfo.bet, info);
//			} else if (info.hitSymbol.payType == Symbol.PayType.FreesSpin) {
//				AddFreeSpin(info.payout);
//			} else if (info.hitSymbol.payType == Symbol.PayType.Bonus) {
//				AddBonus(info.payout);
//			}

//			Sequence sequence = info.sequence = DOTween.Sequence();
//			AddEvent(sequence);
//			callbacks.onProcessHit.Invoke(info);

//			SlotEffectManager.SymbolHitEffect hitEffect = effects.GetHitEffect(info);
//			if (hitEffect == null) {
//				sequence.Append(Util.Tween(1f));
//			} else {
//				for (int i = 0; i < info.holders.Length; i++) if (i < info.hitChains) sequence.Join(hitEffect.Play(info.holders[i], i));
//				if (info.line) sequence.Join(effects.lineHitEffect.Play(info.line, hitEffect.duration));
//			}
//		}

//		/// <summary>
//		/// Returns a list of all visible SymbolHolder.  
//		/// </summary>
//		public List<SymbolHolder> GetVisibleHolders() {
//			List<SymbolHolder> list = new List<SymbolHolder>();
//			foreach (Row row in rows) {
//				if (row.isHiddenRow) continue;
//				foreach (SymbolHolder holder in row.holders) list.Add(holder);
//			}
//			return list;
//		}

//		public List<SymbolHolder> GetAllHolders() {
//			List<SymbolHolder> list = new List<SymbolHolder>();
//			foreach (Reel reel in reels) foreach (SymbolHolder holder in reel.holders) list.Add(holder);
//			return list;
//		}

//		public void AddFreeSpin(int amount) {
//			gameInfo.freeSpins += amount;
//			if (gameInfo.freeSpins < 0) gameInfo.freeSpins = 0;
//		}

//		public void AddBonus(int amount) {
//			gameInfo.bonuses += amount;
//			if (gameInfo.bonuses < 0) gameInfo.bonuses = 0;
//		}

//		public void SetBet(int amount) { gameInfo.bet = amount; }
//	}
//}