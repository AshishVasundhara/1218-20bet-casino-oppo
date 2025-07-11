//using System;
//using DG.Tweening;
//using UnityEngine;
//using UnityEngine.UI;

//namespace CSFramework {
//	/// <summary>
//	/// A class that displays a symbols a reel has.
//	/// See <see cref="Reel"/> for more information.
//	/// SymbolHolder doesn't store references to Symbol but the index of Symbol list on its parent reel.  
//	/// </summary>
//	public class SymbolHolder : MonoBehaviour {
//		private CustomSlot slot { get { return reel.slot; } }
//		public float y { get { return _rect.anchoredPosition.y; } set { _rect.anchoredPosition = new Vector2(0, value); } }
//		public Symbol symbol { get { return reel.symbols[symbolIndex]; } }

//		[Tooltip("Fix the holder's rotation to face front if you modified your slot's rotation.")]
//		public bool faceFront;

//		[Header("References")]
//		public Image image;

//		[Hide]
//		public Reel reel;
//		[Hide]
//		public int symbolIndex;
//		[HideInInspector]
//		public RectTransform _rect;
//		[NonSerialized]
//		public float speed;

//		private void Awake() { _rect = transform as RectTransform; }

//		internal SymbolHolder OnRefreshLayout(Reel reel, int index) {
//			this.reel = reel;
//			_rect = transform as RectTransform;
//			y = -slot.layoutRow.cellSize.y*(index + 1);
//			_rect.SetParent(reel.transform, false);
//			symbolIndex = index;
//			_rect.sizeDelta = reel.slot.layout.sizeSymbol;
//			if (faceFront) transform.eulerAngles = new Vector3(0, 0, 0);
//			return this;
//		}

//		internal void SetNextItem() {
//			symbolIndex = (reel.lastSymbol + 1 >= reel.symbols.Length) ? 0 : reel.lastSymbol + 1;
//			reel.lastSymbol = symbolIndex;
//			if (symbol) image.sprite = symbol.sprite;
//			if (slot.config.sortSymbolTransform == SlotConfig.HolderSortMode.AppearAsFirstSibling) transform.SetAsFirstSibling();
//			else if (slot.config.sortSymbolTransform == SlotConfig.HolderSortMode.AppearAsLastSibling) transform.SetAsLastSibling();
//			slot.callbacks.onNewSymbolAppear.Invoke();
//		}

//		internal void Accelerate(float destSpeed) { DOTween.To(() => speed, x => speed = x, destSpeed, slot.currentMode.reelAccelerateTime).SetEase(slot.currentMode.reelAccelerateEase); }

//		internal void Stop(float destY) {
//			DOTween.Kill(speed);
//			speed = 0;

//			Tweener tween = _rect.DOLocalMoveY(destY, slot.currentMode.reelStopTime).SetEase(slot.currentMode.reelStopEase).OnComplete(SnapToRow);
//			tween.OnUpdate(() => {
//				if (y < reel.maxY - slot.config.magrinBottomRow) {
//					y -= reel.maxY;
//					SetNextItem();
//					_rect.DOLocalMoveY(destY - reel.maxY, slot.currentMode.reelStopTime - tween.Elapsed()).SetEase(slot.currentMode.reelStopEase).OnComplete(SnapToRow);
//					tween.Kill();
//				}
//			});
//		}

//		/// <summary>
//		/// Highlights the symbol this holder has for the given duration.
//		/// </summary>
//		/// <param name="duration"></param>
//		/// <returns></returns>
//		public virtual Tweener HighlightBorder(float duration) {
//			Image border = Util.InstantiateAt<Image>(slot.skin.symbolBorder, transform);
//			Color color = border.color;
//			border.color = new Color(0, 0, 0, 0);
//			return border.DOColor(color, duration*0.5f).SetEase(Ease.OutCubic).SetLoops(2, LoopType.Yoyo).OnComplete(() => { Destroy(border.gameObject); });
//		}

//		/// <summary>
//		/// Snaps the holder position to a nearest row. Should be called after the holder stopped moving.
//		/// </summary>
//		public virtual void SnapToRow() {
//			float spacing = slot.layoutRow.cellSize.y;
//			int index = Mathf.Clamp(-Mathf.RoundToInt(y/spacing), 0, slot.rows.Length - 1);
//			y = -index*spacing;
//			Row row = slot.rows[index];
//			row.holders[reel.index] = this;
//		}
//	}
//}