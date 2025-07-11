//using System;
//using System.Collections.Generic;
//using UnityEngine;

//namespace CSFramework {
//	/// <summary>
//	/// A class that controlls a reel and its symbol holders.
//	/// A reel actually never spins but the symbol holders on the reel do.
//	/// When Spin() is called, all the holders on the reel start moving toward the bottom of the screen
//	/// then jump to the top once they reach the bottom which makes it look like the reel is spinning.
//	/// When a symbol holder reaches the bottom of the screen, it draw a new symbol from the list of symbols its parent has.
//	/// </summary>
//	public class Reel : MonoBehaviour {
//		internal float spacing { get { return slot.layoutRow.cellSize.y; } }
//		internal float maxY { get { return -spacing*holders.Count; } }

//		[Hide]
//		public int index;
//		[Hide]
//		public CustomSlot slot;
//		[NonSerialized]
//		public int lastSymbol = -1;
//		public Symbol[] symbols;
//		[HideInInspector]
//		public List<SymbolHolder> holders;

//		internal void OnRefreshLayout(CustomSlot slot, int index) {
//			this.slot = slot;
//			this.index = index;
//			transform.SetParent(slot.layoutReel.transform, false);
//			symbols = new Symbol[slot.config.symbolsPerReel];
//			holders.Clear();
//			Util.DestroyChildren<SymbolHolder>(this);
//			for (int i = 0; i < slot.config.totalRows - 1; i++) holders.Add(GameObject.Instantiate<SymbolHolder>(slot.skin.symbolHolder).OnRefreshLayout(this, i));
//		}

//		public void RefreshHolders() { foreach (SymbolHolder holder in holders) holder.image.sprite = holder.symbol.sprite; }

//		internal void Validate(CustomSlot slot) {
//			this.slot = slot;
//			for (int i = 0; i < symbols.Length; i++) if (symbols[i] == null) symbols[i] = slot.skin.defaultSymbol;
//			foreach (SymbolHolder holder in holders) holder.SnapToRow();
//			lastSymbol = holders[holders.Count - 1].symbolIndex;
//		}

//		private void Update() {
//			// foreach is simpler but will generate garbage each frame 
//			for (int i = 0; i < holders.Count; i++) {
//				SymbolHolder holder = holders[i];
//				if (holder.speed > 0) {
//					float newY = holder.y - holder.speed*Time.deltaTime;
//					if (newY < maxY) {
//						newY -= maxY;
//						holder.SetNextItem();
//					}
//					holder.y = newY;
//				}
//			}
//		}

//		/// <summary>
//		/// A method to start accelerating the reel.
//		/// </summary>
//		public void Spin() {
//			slot.callbacks.onReelStart.Invoke(new ReelInfo(this));
//			foreach (SymbolHolder holder in holders) {
//				holder.Accelerate(slot.currentMode.reelMaxSpeed);
//			}
//		}

//		/// <summary>
//		/// A method to stop the reel
//		/// Once a reel stops and the symbols snap to rows, the rows will parse symbols each reel has
//		/// and stores them in a list. 
//		/// </summary>
//		public void Stop() {
//			int distance = Mathf.Clamp(slot.currentMode.reelStopDistance - 1, 0, slot.rows.Length - 2)*-1;
//			foreach (SymbolHolder holder in holders) {
//				float diff = -holder.y%spacing;
//				float snapY = spacing*(int) (holder.y/spacing + (holder.y >= 0 ? 1 : 0) + distance) - spacing*((diff > spacing/2) ? 2 : 1);
//				holder.Stop(snapY);
//			}
//			slot.callbacks.onReelStop.Invoke(new ReelInfo(this));
//		}
//	}
//}