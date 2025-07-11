using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using System.IO;
using System.IO.Compression;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;

///-----------------------------------------------------------------------------------------
///   Namespace:      BE
///   Class:          SceneSlotGame
///   Description:    process user input & display result
///   Usage :		  
///   Author:         BraveElephant inc.                    
///   Version: 		  v1.0 (2015-08-30)
///-----------------------------------------------------------------------------------------
namespace BE {

	public class SceneSlotGame : MonoBehaviour {

		public	static SceneSlotGame instance;

		public	static int 		uiState = 0; 	// popup window shows or not
		public  static BENumber	Win;			// total win number

		public 	SlotGame	Slot;			// slot game class

		public	Text		textLine;		// user selected line info text
		public	Text		textBet;		// current betting info text
		public	Text		textTotalBet;	// total betting info
		public	Text		textTotalWin;	// total win info
		public	Text		textGold;		// user gold info
		public	Text		textInfo;		// other info text

		public 	Button		btnBuy;			// buy gold button
		public 	Button		btnMenu;		// call setting dialog
		public 	Button		btnPayTable;	// show pay table info dialog
		public 	Button		btnMaxLine;		// line count to max number
		public 	Button		btnLines;		// change line selected
		public 	Button		btnBet;			// change bet 
		//public 	Button		btnDouble;		// start double game
		public 	Button		btnSpin;		// start spin
		public 	Toggle		toggleAutoSpin;	// auto spin toggle button
        public GameObject Infotablepanel,Bonusgamepanel , ExitPanel;
        public Sprite[] egg_images;
        public GameObject Egg;
        public Sprite[] Sound_images;
        public GameObject sound;
        public GameObject SettingBtn;
        [SerializeField]
        bool settingflag = false;

        public GameObject SlotHandle;

		public 	GameObject	FreeSpinBackground; // background og game scene
        private int hold_count;

        void Awake () {
			instance=this;
		}

		void Start () {

            settingflag = false;
            SettingBtn.GetComponent<Animator>().enabled = false;
           //  PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("BonusGame", 0);
			// set range of numbers and type
			BESetting.Gold.AddUIText(textGold);
			Win = new BENumber(BENumber.IncType.VALUE, "#,##0.00", 0, 10000000000, 100);			
			Win.AddUIText(textTotalWin);

			//set saved user gold count to slotgame
			Slot.Gold = (float)BESetting.Gold.Target();
			//set win value to zero
			Win.ChangeTo(0);

			UpdateUI ();

			//double button show only user win
		//	btnDouble.gameObject.SetActive (false);
			textInfo.text = "";

            if (PlayerPrefs.GetInt("Sound", 0) == 0)
            {
                sound.GetComponent<Image>().sprite = Sound_images[1];
               // SoundManager.instance.Bg_music_Play();
           
            }
            else
            {
               // SoundManager.instance.music.Stop();
                sound.GetComponent<Image>().sprite = Sound_images[0];

            }
		}

		void Update ()
        {
		
			// if user press 'escape' key, show quit message window
			if ((uiState==0) && Input.GetKeyDown(KeyCode.Escape))
            { 
				UISGMessage.Show("Quit", "Do you want to quit this program ?", MsgType.OkCancel, MessageQuitResult);
			}
			
			Win.Update();

			// if auto spin is on or user has free spin to run, then start spin
			//if((toggleAutoSpin.isOn || Slot.gameResult.InFreeSpin()) && Slot.Spinable()) {
			//	Debug.Log ("Update Spin");
			//	OnButtonSpin();
			//}
		}

		// when user pressed 'ok' button on quit message.
		public void MessageQuitResult(int value) {
			//Debug.Log ("MessageQuitResult value:"+value.ToString ());
			if(value == 0) {
				Application.Quit ();
			}
		}

		//user clicked shop button, then show shop
		public void OnButtonShop() {
			//BEAudioManager.SoundPlay(0);
			//UISGShop.Show();
		}

		// user clicked option button, then show option
		public void OnButtonOption() {
			//BEAudioManager.SoundPlay(0);
			//UISGOption.Show();
		}

		// user clicked paytable button, then show paytable
		public void OnButtonPayTable() {
            SoundManager.instance.Play_click_btn_sound();
			//BEAudioManager.SoundPlay(0);
            Infotablepanel.SetActive(true);
			//UISGPayTable.Show(Slot);
		}

		// user clicked Max line button, then ser slot's line count to max
		public void OnButtonMaxLine() {
			//BEAudioManager.SoundPlay(0);
			Slot.LineSet(Slot.Lines.Count);
			UpdateUI();
		}

		//user clicked line button, increase line count 
		public void OnButtonLines() {
            SoundManager.instance.Play_click_btn_sound();
			//BEAudioManager.SoundPlay(0);
            print("Slot.Line+1======>>>" + (Slot.Line + 1));
            

			Slot.LineSet(Slot.Line+1);
			UpdateUI();
		}

		// user clicked bet button, then increase bet number
		public void OnButtonBet()
        {
            SoundManager.instance.Play_click_btn_sound();
			//BEAudioManager.SoundPlay(0);
			Slot.BetSet(Slot.Bet+1);
			UpdateUI();
		}

		//user clicked double button, then start double game
		public void OnButtonDouble() {
			//BEAudioManager.SoundPlay(0);
			UIDoubleGame.Show(Slot.gameResult.GameWin);
		}

        public void OnButton_MAXBET()
        {
            
            Debug.Log("OnButton_MAXBET");
            SlotGame.instance.RealBet = 0.5f;
            SlotGame.instance.LineSet1(9);
         
            UpdateUI();
            OnButtonSpin();
        }



		// user clicked spin button
		public void OnButtonSpin() {
            textGold.GetComponent<Animator>().enabled = false;
            textGold.GetComponent<Text>().color = new Color32(255, 187, 2,255); 
    			Debug.Log ("OnButtonSpin");

            SoundManager.instance.Play_click_btn_sound();
            for (int i = 2; i < 10; i++)
            {
                for (int j = 1; j < 5; j++)
                {
                    SceneSlotGame.instance.Infotablepanel.transform.GetChild(0).GetChild(i).GetChild(j).GetChild(0).GetComponent<Text>().color = Color.white;
                    SceneSlotGame.instance.Infotablepanel.transform.GetChild(0).GetChild(i).GetChild(j).GetChild(0).GetComponent<Animator>().enabled = false;
                }
            }

            SlotGame.instance.lins_count.Clear();
            StopCoroutine(SlotGame.instance.SetWinBorder());
            SlotGame.instance.i=7;
			//BEAudioManager.SoundPlay(0);
            SlotGame.instance.HoldFlag = true;

            for (int n = 0; n < SlotGame.instance.Show_Lines.Length; n++)
            {
                SlotGame.instance.Show_Lines[n].SetActive(false);
            }

           
           // SlotGame.instance.Show_Line_image.gameObject.SetActive(false);
           
			// start spin
			SlotReturnCode code = Slot.Spin();
			// if spin succeed
			if(SlotReturnCode.Success == code) {
                SlotHandle.GetComponent<Animator>().enabled = true;
                SlotHandle.GetComponent<Animator>().Play("SlotHandleAnim", -1, 0);
                //SlotHandle.GetComponent<Animator>()
                SoundManager.instance.Play_spin_start_sound();
            SoundManager.instance.Play_spin_sound();
				// disabled inputs
				ButtonEnable(false);
			//	btnDouble.gameObject.SetActive (false);
				UpdateUI();
				// apply decreased user gold
				BESetting.Gold.ChangeTo(Slot.Gold);
				BESetting.Save();

				// set info text
				if(Slot.gameResult.InFreeSpin()) 	textInfo.text = "Free Spin "+Slot.gameResult.FreeSpinCount.ToString ()+" of "+Slot.gameResult.FreeSpinTotalCount.ToString ();
				else 								textInfo.text = "";
			}
			else {
				// if spin fails
				// show Error Message
				//if(SlotReturnCode.InSpin == code) 		{ UISGMessage.Show("Error", "Slot is in spin now.", MsgType.Ok, null); }
				//else if(SlotReturnCode.NoGold == code) 	{ UISGMessage.Show("Error", "Not enough gold.", MsgType.Ok, null); }
				//else {}
                if(SlotReturnCode.NoGold == code) 
                {
                    StopCoroutine(RedText());
                    textGold.GetComponent<Animator>().enabled = true;
                    textGold.GetComponent<Text>().color = new Color32(255, 0, 0,255); 
                    StartCoroutine(RedText());
                }
			}
		}


        IEnumerator RedText()
        {
            yield return new WaitForSeconds(1.5f);
            textGold.GetComponent<Animator>().enabled = false;
            textGold.GetComponent<Text>().color = new Color32(255, 187, 2,255); 

                
        }
		// if user clicked auto spin
		public void AutoSpinToggled(bool value) {
			//BEAudioManager.SoundPlay(0);
		}

		// update ui text & win number
		public void UpdateUI() {
			textLine.text = Slot.Line.ToString ();
			textBet.text = Slot.RealBet.ToString ("#0.00");
			textTotalBet.text = Slot.TotalBet.ToString ("#0.00");
			Win.ChangeTo(Slot.gameResult.GameWin);
		}

		// enable or disable button inputs
		public void ButtonEnable(bool bEnable) {
			//btnBuy.interactable = bEnable;
			//btnMenu.interactable = bEnable;
			btnPayTable.interactable = bEnable;
			btnMaxLine.interactable = bEnable;
			btnLines.interactable = bEnable;
			btnBet.interactable = bEnable;
			btnSpin.interactable = bEnable;
		}

		//------------------------------------------
		//callback functions
		// when double game ends
		public void OnDoubleGameEnd(float delta) {
			//Debug.Log("OnDoubleGameEnd:"+delta.ToString ());

			//change user gold by delta (change gold value)
			Slot.Gold += delta;
			BESetting.Gold.ChangeTo(Slot.Gold);
			BESetting.Save();
			Slot.gameResult.GameWin += delta;
			Win.ChangeTo(Slot.gameResult.GameWin);
			//btnDouble.gameObject.SetActive (false);
		}

		// when reel stoped
		public void OnReelStop(int value) {
			//Debug.Log ("OnReelStop:"+value.ToString());
            SoundManager.instance.Play_Reel_end_sound();
			//BEAudioManager.SoundPlay(2);
		}

		// when spin completed
		public void OnSpinEnd() {
            Debug.Log("OnSpinEnd");

            SoundManager.instance.Stop_spin_sound();
            // if user has win
            if (Slot.gameResult.Wins.Count != 0)
            {
                SoundManager.instance.Play_win_sound();
                textInfo.text = "Win " + Slot.gameResult.Wins.Count.ToString() + " Lines ";
                StartCoroutine(SlotGame.instance.SetWinBorder());
            }
            else{
                //if (PlayerPrefs.GetInt("hold", 0) == 1)
                //{
                //    Debug.Log("stop hold");
                //    PlayerPrefs.SetInt("hold", 0);
                //    SlotGame.instance.hold = false;
                //    for (int n = 0; n < SlotGame.instance.hold_images.Length; n++)
                //    {

                //        SlotGame.instance.hold_images[n].SetActive(false);
                //        // hold_images[n].GetComponent<Image>().color = new Color32(255, 255, 255, 0);
                //        SlotGame.instance.hold_images[n].transform.GetChild(0).gameObject.SetActive(false);
                //        SlotGame.instance.hold_images[n].transform.GetChild(1).gameObject.SetActive(false);

                //    }
                //}
                //SlotGame.instance.hold_count = 0;
                ButtonEnable(true);
            }
            

			UpdateUI();
            // increase user gold
            print("--- >> Slot.Gold---->>" + Slot.Gold);
			BESetting.Gold.ChangeTo(Slot.Gold);
			BESetting.Save();
           
		}

		//when splash window shows
		public void OnSplashShow(int value) {
			Debug.Log ("OnSplashShow----------------------------------------------------------------->>>>:"+value.ToString());
			//BEAudioManager.SoundPlay(3);
			//UISGSplash.Show (value);

			//// change background image if free spin
			//if(value == (int)SplashType.FreeSpin) {
			//	FreeSpinBackground.SetActive(true);
			//}
			//else if(value == (int)SplashType.FreeSpinEnd) {
			//	FreeSpinBackground.SetActive(false);
			//}
			//else {}
		}

		// when splash hide
		public void OnSplashHide(int value) {
            Debug.Log ("OnSplash_hide----------------------------------------------------------------->>>>:"+value.ToString());
			//StartCoroutine(SlotSplashHide(value, 0.5f));
		}

		// when all splash works end
		public void OnSplashEnd() {
            
           
            Debug.Log ("OnSplashEnd" + Slot.gameResult.InFreeSpin());
			//if(Slot.gameResult.InFreeSpin()) {
			//	textInfo.text = "Free Spin "+Slot.gameResult.FreeSpinCount.ToString ()+" of "+Slot.gameResult.FreeSpinTotalCount.ToString ();
			//}
			//else {
               
			//	textInfo.text = "";
				//btnDouble.gameObject.SetActive ((Slot.gameResult.GameWin > 0.001f) ? true : false);
			//	ButtonEnable(true);

   //             //print("set true 0 0- - -  - - - - - - -  - - - - -  >>  000");
			//}
		}

		// splash idx change
		public IEnumerator SlotSplashHide(int value, float fDelay) {
			
			if(fDelay > 0.01f)
				yield return new WaitForSeconds(fDelay);
			
			Slot.SplashCount[value] = 0;
			Slot.SplashActive++;
			Slot.InSplashShow = false;
		}


        public void ClickOnFish(GameObject fish)
        {
            //if (PlayerPrefs.GetInt("BonusGame", 0) == 0)
            //{
            //   // SoundManager.instance.Play_bonus_fish_click_sound();
            //    PlayerPrefs.SetInt("BonusGame", 1);
            //    fish.GetComponent<Animator>().enabled = true;
            //    StartCoroutine(Wait1sec(fish));
            //}
        }
        //IEnumerator Wait1sec(GameObject fish)
        //{
        //    yield return new WaitForSeconds(1);
        //    GameObject egg_ = Instantiate(Egg);
        //    int n = UnityEngine.Random.Range(0, 3);

           
        //    egg_.GetComponent<Image>().sprite = egg_images[n];
        //    egg_.transform.SetParent(fish.transform);
        //    egg_.transform.localPosition = new Vector3(-7, -51, 0);
        //    if (n == 0)
        //    {
        //        Slot.Gold += 0.5f;


        //        Bonusgamepanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "x5";
        //    }
        //    else if(n==1)
        //    {
        //        Slot.Gold += 5f;

        //        Bonusgamepanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "x50";
        //    }
        //    else if (n == 2)
        //    {
        //        Slot.Gold += 10f;

        //        Bonusgamepanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "x100";
        //    }
        //    //SoundManager.instance.Play_bonus_get_price_sound();
        //    yield return new WaitForSeconds(1);

        //    Bonusgamepanel.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        //    Bonusgamepanel.transform.GetChild(0).GetChild(0).GetComponent<Animator>().enabled = true;
        //    yield return new WaitForSeconds(2);
        //    Bonusgamepanel.SetActive(false);
        //    BESetting.Gold.ChangeTo(Slot.Gold);
        //   // SoundManager.instance.Bg_music_Play();
           
        //    fish.GetComponent<Animator>().enabled = false;
        //    BESetting.Save();
        //    Bonusgamepanel.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        //    Bonusgamepanel.transform.GetChild(0).GetChild(0).GetComponent<Animator>().enabled = false;
        //    Destroy(egg_);
        //}




        public void exitgame()
        {
            Application.Quit();
        }

        public void BAckBtn()
        {
            SoundManager.instance.Play_click_btn_sound();
            SceneManager.LoadSceneAsync("Menu");


        }

        public void SoundToggle()
        {
           
            if (PlayerPrefs.GetInt("Sound", 0) == 0)
            {
                //set off
                PlayerPrefs.SetInt("Sound", 1);
                sound.GetComponent<Image>().sprite = Sound_images[0];
               // SoundManager.instance.music.Stop();


            }
            else
            {
                //set on
                PlayerPrefs.SetInt("Sound", 0);
                sound.GetComponent<Image>().sprite = Sound_images[1];
               // SoundManager.instance.Bg_music_Play();
                SoundManager.instance.Play_click_btn_sound();
            }
        }


        public void SettingToggle()
        {

            SoundManager.instance.Play_click_btn_sound();
            if (!settingflag)
            {
                settingflag = true;
                SettingBtn.GetComponent<Animator>().enabled = true;
                SettingBtn.GetComponent<Animator>().Play("SettingbtnOpen", -1, 0);


            }
            else
            {
                settingflag = false;
                SettingBtn.GetComponent<Animator>().enabled = true;
                SettingBtn.GetComponent<Animator>().Play("SettingbtnClose", -1, 0);
            }
        }


        public void OpenExitPanel()
        {
            SoundManager.instance.Play_click_btn_sound();
            ExitPanel.SetActive(true);
        }


        public void CloseExitPanel()
        {
            SoundManager.instance.Play_click_btn_sound();
            ExitPanel.SetActive(false);
        }

	}  

}
