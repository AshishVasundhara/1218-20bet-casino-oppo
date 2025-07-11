using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class CardGame : MonoBehaviour
{
	public GameObject finger,overlayMoney;
	public CardDeck Deck;
	//List<CardDefinition> m_deck = new List<CardDefinition>();
	
	List<Card> m_dealer = new List<Card>();
	List<Card> m_player = new List<Card>();

	public GameObject InfoPanelObj;
	public GameObject PlayerWinObj;
	public GameObject PlayerLoseObj;
	public GameObject NoWinObj;
	public Button BetBtn;
	public Button StayBtn;
	public Button _HitBtn;
	public Button _ResetBtn;
	enum GameState
	{
		Invalid,
		Started,
		PlayerBusted,
		Resolving,
		DealerWins,
		PlayerWins,
		NobodyWins,
	};

	GameState m_state;

	int countClickPlay = 0;
	// Use this for initialization
	void Start ()
	{
		InfoPanelObj.SetActive (false);
		m_state = GameState.Invalid;
		Deck.Initialize();
		PlayerWinObj.SetActive(false);
		PlayerLoseObj.SetActive(false);
		NoWinObj.SetActive(false);
	}
	
	void ShowMessage(string msg)
	{
		if (msg == "Dealer")
		{
			InfoPanelObj.SetActive (true);
			PlayerWinObj.SetActive(false);
			PlayerLoseObj.SetActive(true);
			NoWinObj.SetActive(false);


		}
		else if (msg == "Player")
		{
			InfoPanelObj.SetActive (true);
			PlayerWinObj.SetActive(true);
			PlayerLoseObj.SetActive(false);
			NoWinObj.SetActive(false);
		}
		else if (msg == "Nobody")
		{
			InfoPanelObj.SetActive (true);
			PlayerWinObj.SetActive(false);
			PlayerLoseObj.SetActive(false);
			NoWinObj.SetActive(true);

		}
		else
		{
			InfoPanelObj.SetActive (false);
			PlayerWinObj.SetActive(false);
			PlayerLoseObj.SetActive(false);
			NoWinObj.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
//		SliderScriptBJ._sliderScipt.be
		if(m_state == GameState.PlayerBusted|| m_state == GameState.Invalid || m_state == GameState.DealerWins|| m_state == GameState.PlayerWins || m_state == GameState.NobodyWins)
		{
			BetBtn.interactable = true;
		}
		else
			BetBtn.interactable = false;
		if (m_state == GameState.PlayerBusted || m_state == GameState.DealerWins || m_state == GameState.PlayerWins || m_state == GameState.NobodyWins) {
			StayBtn.interactable = false;
			_HitBtn.interactable = false;
			_ResetBtn.interactable = true;
		} else
		{
			StayBtn.interactable = true;
			_HitBtn.interactable = true;
			_ResetBtn.interactable = false;
		}
		
		if (Input.GetKeyDown(KeyCode.F1))
		{
			OnReset();
		}
		else if (Input.GetKeyDown(KeyCode.F2))
		{
			OnHitMe();
		}
		else if (Input.GetKeyDown(KeyCode.F3))
		{
			OnStop();
		}
	}
	
	void Shuffle()
	{
		if (m_state != GameState.Invalid)
		{
		}
	}

	
	void Clear()
	{
		foreach (Card c in m_dealer)
		{
			GameObject.DestroyImmediate(c.gameObject);
		}
		foreach (Card c in m_player)
		{
			GameObject.Destroy(c.gameObject);
		}
		m_dealer.Clear();
		m_player.Clear();
		Deck.Reset();
	}
	
	Vector3 GetDeckPosition()
	{
		return new UnityEngine.Vector3(-7,0,0);
	}
	
	const float FlyTime = 0.5f;
	
	void HitDealer()
	{
		CardDef c1 = Deck.Pop();
		if (c1 != null)
		{
			GameObject newObj = new GameObject();
			newObj.name = "Card";
			Card newCard = newObj.AddComponent(typeof(Card)) as Card;
			newCard.Definition = c1;
			newObj.transform.parent = Deck.transform;
			newCard.TryBuild();
			float x = -3+(m_dealer.Count)*2.0f;
			float z = (m_dealer.Count)*-0.1f;
			Vector3 deckPos = GetDeckPosition();
			newObj.transform.position = deckPos;
			m_dealer.Add(newCard);
			newCard.SetFlyTarget(deckPos,new Vector3(x,2,z),FlyTime);
		}
	}
	void HitPlayer()
	{
		CardDef c1 = Deck.Pop();
		if (c1 != null)
		{
			GameObject newObj = new GameObject();
			newObj.name = "Card";
			Card newCard = newObj.AddComponent(typeof(Card)) as Card;
			newCard.Definition = c1;
			newObj.transform.parent = Deck.transform;
			newCard.TryBuild();
			float x = -3+(m_player.Count)*1.5f;
			float y = -3-m_player.Count*0.15f;
			float z = (m_player.Count)*-0.1f;
			m_player.Add(newCard);
			Vector3 deckPos = GetDeckPosition();
			newCard.transform.position = deckPos;
			newCard.SetFlyTarget(deckPos,new Vector3(x,y,z),FlyTime);
		}
	}
	
	static int Value(Card c)
	{
		if (c != null)
		{
			switch (c.Definition.Pattern)
			{
			case 0:
				return 10;
			case 1:
				return 11;
			}
			return c.Definition.Pattern;
		}
		return 0;
	}
	
	static int GetScore(List<Card> cards)
	{
		int score = 0;
		bool ace = false;
		foreach (Card c in cards)
		{
			int s = Value(c);
			if ((score + s) > 21)
			{
				if (s == 11)
				{
					s = 1;
				}
				else if (ace)
				{
					score -= 10;
					ace = false;
				}
			}
			score += s;
			ace |= (s == 11);
		}
		return score;
	}
	
	int GetDealerScore()
	{
		return GetScore(m_dealer);
	}
	
	int GetPlayerScore()
	{
		return GetScore(m_player);
	}
	
	const float DealTime = 0.35f;
	
	IEnumerator OnReset()
	{
		if (m_state != GameState.Resolving)
		{
			m_state = GameState.Resolving;
			ShowMessage("");
			Clear();
			Deck.Shuffle();
			HitDealer();
			yield return new WaitForSeconds(DealTime);
			HitDealer();
			yield return new WaitForSeconds(DealTime);
			HitPlayer();
			yield return new WaitForSeconds(DealTime);
			HitPlayer();
			m_state = GameState.Started;
		}
	}
	void OnHitMe()
	{
		if (m_state == GameState.Started)
		{
			HitPlayer();
			if (GetPlayerScore() > 21)
			{
				m_state = GameState.PlayerBusted;
				InfoPanelObj.SetActive (true);
				PlayerWinObj.SetActive(false);
				PlayerLoseObj.SetActive(true);
				NoWinObj.SetActive(false);
				DataManager.Instance.RemoveCoins (int.Parse(SliderScriptBJ._sliderScipt.BetTex.text));
				SoundController.Sound.VideoPoker_Lose ();
			}
		}
	}
	bool TryFinalize(int playerScore, int dealer)
	{
		if (playerScore > 21)
		{
			// Dealer Wins!
			ShowMessage("Dealer");
			m_state = GameState.DealerWins;
			DataManager.Instance.RemoveCoins (int.Parse(SliderScriptBJ._sliderScipt.BetTex.text));
			SoundController.Sound.VideoPoker_Lose ();
			return true;
		}
		if (dealer > 21)
		{
			ShowMessage("Player");
			m_state = GameState.PlayerWins;
			DataManager.Instance.AddCoins (int.Parse(SliderScriptBJ._sliderScipt.BetTex.text));
			SoundController.Sound.BlackJack_Win ();
			return true;
		}
		if (dealer > playerScore)
		{
			ShowMessage("Dealer");
			m_state = GameState.DealerWins;
			DataManager.Instance.RemoveCoins (int.Parse(SliderScriptBJ._sliderScipt.BetTex.text));
			SoundController.Sound.VideoPoker_Lose ();
			return true;
		}
		// Natural 21 beats everything else.
		bool pn = (playerScore == 21) && (m_player.Count == 2);
		bool dn = (dealer == 21) && (m_dealer.Count == 2);
		if (pn && !dn)
		{
			ShowMessage("Player");
			m_state = GameState.PlayerWins;
			DataManager.Instance.AddCoins (int.Parse(SliderScriptBJ._sliderScipt.BetTex.text));
			SoundController.Sound.BlackJack_Win ();
			return true;
		}
		if (dn && !pn)
		{
			ShowMessage("Dealer");
			DataManager.Instance.RemoveCoins (int.Parse(SliderScriptBJ._sliderScipt.BetTex.text));
			SoundController.Sound.VideoPoker_Lose ();
			m_state = GameState.DealerWins;
			return true;
		}
		if (dealer > 17)
		{
			if (playerScore == dealer)
			{
				// Nobody Wins!
				ShowMessage("Nobody");
				DataManager.Instance.AddCoins (0);
				SoundController.Sound.VideoPoker_Lose ();
				m_state = GameState.NobodyWins;
				return true;
			}
			else if (dealer < playerScore)
			{
				// Player Wins!
				ShowMessage("Player");
				DataManager.Instance.AddCoins (int.Parse(SliderScriptBJ._sliderScipt.BetTex.text));
				SoundController.Sound.BlackJack_Win ();
				m_state = GameState.PlayerWins;
				return true;
			}
			else
			{
				// Dealer Wins!
				ShowMessage("Dealer");
				DataManager.Instance.RemoveCoins (int.Parse(SliderScriptBJ._sliderScipt.BetTex.text));
				SoundController.Sound.VideoPoker_Lose ();
				m_state = GameState.DealerWins;
				return true;
			}
		}
		return false;
	}
	IEnumerator OnStop()
	{
		if (m_state == GameState.Started || m_state == GameState.PlayerBusted)
		{
			m_state = GameState.Resolving;
			int playerScore = GetPlayerScore();
			while (true)
			{
				int d = GetDealerScore();
				Debug.Log(string.Format("Player={0}  Dealer={1}",playerScore,d));
				if (TryFinalize(playerScore,d))
				{
					break;
				}
				else
				{
					Debug.Log("HitDealer");
					HitDealer();
					yield return new WaitForSeconds(DealTime*1.5f);
				}
			}
		}
	}

	public void ResetBtn()
	{
		countClickPlay++;
		if (DataManager.Instance.Coins > 0) 
		{
			if (SliderScriptBJ._sliderScipt.parentSlider.localScale.y > .9f) {
				SliderScriptBJ._sliderScipt.ClickBet ();
			}
			Debug.Log ("Bet: " + int.Parse (SliderScriptBJ._sliderScipt.BetTex.text));
			if (int.Parse (SliderScriptBJ._sliderScipt.BetTex.text) > int.Parse (SliderScriptBJ._sliderScipt.MoneyTex.text)) 
			{
				SliderScriptBJ._sliderScipt.BetTex.text = "" + DataManager.Instance.Coins;
			}
			SoundController.Sound.BlackJack_Start ();
			StartCoroutine (OnReset ());
			if (countClickPlay == 10) 
			{
                //show ads
                //AdmobBannerController.Instance.ShowInterstitial();
                //AdManagerUnity.Instance.ShowAd("video");
                countClickPlay = 0;
			}
		}
		else
		{
			finger.SetActive(true);
			overlayMoney.SetActive(true);
		}
	}
	public void HitBtn()
	{
		SoundController.Sound.VideoPoker_DealOrDrawCard ();
		OnHitMe();
	}
	public void StopBtn()
	{
		SoundController.Sound.ClickBtn ();
		StartCoroutine(OnStop());
	}
}
