//using Elona;
//using UnityEngine;
//using UnityEngine.UI;

//namespace CSFramework {
//	public class DemoMenu : MonoBehaviour {
//		[System.Serializable]
//		public class DemoCanvas {
//			public Canvas canvas;
//		}

//		public DemoCanvas[] list;
//		private DemoCanvas current;
//		public Transform menu;
//		public Image imageLanguage;
//		public Sprite spriteEN, spriteJP;

//		private void Awake() {
//			foreach (DemoCanvas item in list) if (item.canvas.gameObject.activeSelf) current = item;
//			menu.gameObject.SetActive(false);
//		}

//		public void ToggleMenu() { menu.gameObject.SetActive(!menu.gameObject.activeSelf); }

//		public void ToggleLanguage() {
//			Lang.ToggleLanguage();
//			imageLanguage.sprite = Lang.current == Lang.ID.EN ? spriteEN : spriteJP;
//		}

//		public void SwitchSlot(int index) {
//			if (current != list[index]) {
//				current.canvas.gameObject.SetActive(false);
//				current = list[index];
//				current.canvas.gameObject.SetActive(true);
//			}
//			menu.gameObject.SetActive(false);
//		}
//	}
//}