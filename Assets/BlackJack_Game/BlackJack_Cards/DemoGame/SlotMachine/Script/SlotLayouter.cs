//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//namespace CSFramework {
//	/// <summary>
//	/// A class that takes care of layouting slot's main screen.
//	/// </summary>
//	[Serializable]
//	public class SlotLayouter {
//		[Hide]
//		public CustomSlot slot;
//		public Vector2 sizeSymbol = new Vector2(150, 150);
//		public Vector2 spacingSymbol;
//		public RectOffset paddingMainScreen;
//		public bool linkLineManagerTransformToMainScreen = true;

//		/// <summary>
//		/// A method to enable/disable LayoutGroups the CustomSlot uses to layout rows and reels.
//		/// LayoutGroups will be disabled once CustomSlot is initialized so that no unnecessary calculations are performed by Unity UI.
//		/// </summary>
//		/// <param name="enable"></param>
//		public virtual void SetActiveLayout(bool enable) {
//			List<LayoutGroup> list = new List<LayoutGroup>();
//			list.Add(slot.layoutReel);
//			list.Add(slot.layoutRow);
//			if (enable) EnableLayout(list);
//			else DisableLayout(list);

//			for (int i = 0; i < slot.rows.Length; i++) {
//				if (slot.config.advanced.disableHiddenRows && (i < slot.config.hiddenTopRows || i >= slot.config.hiddenTopRows + slot.config.rows)) slot.rows[i].gameObject.SetActive(false);
//			}
//		}

//		private void EnableLayout(List<LayoutGroup> groups) {
//			foreach (LayoutGroup group in groups) {
//				group.enabled = false;
//				LayoutRebuilder.ForceRebuildLayoutImmediate(group.transform as RectTransform);
//			}
//		}

//		private void DisableLayout(List<LayoutGroup> groups) {
//			foreach (LayoutGroup group in groups) {
//				LayoutRebuilder.ForceRebuildLayoutImmediate(group.transform as RectTransform);
//				group.enabled = true;
//			}
//		}

//		public void Refresh() {
//			if (Application.isPlaying) SetActiveLayout(true);
//			SymbolManager symbolManager = slot.symbolManager;
//			LineManager lineManager = slot.lineManager;
//			SlotConfig config = slot.config;
//			GridLayoutGroup reel = slot.layoutReel;
//			GridLayoutGroup row = slot.layoutRow;

//			slot.Validate();

//			if (config.symbolsPerReel < config.totalRows) {
//				Debug.Log("[Error] Symbols per reel must be higher than total rows(including hidden rows).");
//				return;
//			}

//			reel.cellSize = sizeSymbol;
//			reel.spacing = spacingSymbol;
//			int px = (int) (spacingSymbol.x*0.5f), py = (int) (spacingSymbol.y*0.5f);
//			reel.padding = new RectOffset(px, px, py, py);

//			Vector2 spacing = sizeSymbol + spacingSymbol;
//			float width = spacing.x*config.reelLength;
//			float height = spacing.y*config.rows;

//			row.cellSize = new Vector2(width, sizeSymbol.y + spacingSymbol.y);
//			row.spacing = Vector2.zero;

//			slot.mainScreen.sizeDelta = new Vector2(width + paddingMainScreen.horizontal, height + paddingMainScreen.vertical);

//			(reel.transform as RectTransform).anchoredPosition = (row.transform as RectTransform).anchoredPosition = new Vector2(paddingMainScreen.left, config.hiddenTopRows*spacing.y - paddingMainScreen.top);

//			SymbolMap map = symbolManager.GetSymbolMap();

//			Util.DestroyChildren<Row>(row);
//			for (int i = 0; i < config.totalRows; i++) GameObject.Instantiate<Row>(slot.skin.row).OnRefreshLayout(slot, i);
//			Util.DestroyChildren<Reel>(reel);
//			for (int i = 0; i < config.reelLength; i++) GameObject.Instantiate<Reel>(slot.skin.reel).OnRefreshLayout(slot, i);
			
//			slot.reels = reel.transform.GetComponentsInChildren<Reel>();
//			slot.rows = row.transform.GetComponentsInChildren<Row>();
//			symbolManager.ApplySymbolMap(map);

//			lineManager.OnRefreshLayout();

//			if (linkLineManagerTransformToMainScreen) {
//				(lineManager.transform as RectTransform).sizeDelta = (slot.mainScreen.transform as RectTransform).sizeDelta;
//				lineManager.transform.position = slot.mainScreen.transform.position;
//			}

//			slot.Validate();

//			if (Application.isPlaying) SetActiveLayout(false);
//		}
//	}
//}