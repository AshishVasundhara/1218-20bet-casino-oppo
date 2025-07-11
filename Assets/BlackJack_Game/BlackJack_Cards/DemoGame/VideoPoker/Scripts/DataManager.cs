using UnityEngine;
using System;
using System.Collections;

public class DataManager : MonoBehaviour 
{
    public enum RewardedVideoAdsType
    {
        UnityAds,
        Admob
    };
    private static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            return instance;
        }
    }
    [Tooltip("Choose your Network Ads to Show Rewarded Video Ads |UnityAds Or Admob|")]
    public RewardedVideoAdsType _rewardAdsType;
    public int Coins
	{ 
		get { return _coins; }
		private set { _coins = value; }
	}
	public int FreeAdNumber
	{ 
		get { return _freeadnum; }
		private set { _freeadnum = value; }
	}

	public static event Action<int> CoinsUpdated = delegate {};
	public static event Action<int> FreeAdNumberUpdated = delegate {};
	[SerializeField]
	int initialCoins = 1000;
	// Show the current coins value in editor for easy testing
	[SerializeField]
	int _coins;
	// key name to store high score in PlayerPrefs
	//	const string COINSGAMESTRING = "coins";
	[SerializeField]
	int initialFreeAdNumber = 10;
	[SerializeField]
	int _freeadnum;
	void Awake()
	{
        //PlayerPrefs.DeleteAll ();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

	void Start()
	{
		Reset();
	}

	public void Reset()
	{
		// Initialize coins
		Coins = PlayerPrefs.GetInt("coins", initialCoins);
		ResetFreeAds ();
	}

	public void AddCoins(int amount)
	{
		Coins += amount;
		// Store new coin value
		PlayerPrefs.SetInt("coins", Coins);
		// Fire event
		CoinsUpdated(Coins);
	}

	public void RemoveCoins(int amount)
	{
		Coins -= amount;
		// Store new coin value
		PlayerPrefs.SetInt("coins", Coins);
		// Fire event
		CoinsUpdated(Coins);
	}
	//----------------------
	public void ResetFreeAds()
	{
		FreeAdNumber = PlayerPrefs.GetInt("FreeAdsNumber", initialFreeAdNumber);
	}

	public void AddFreeAdNumber(int amount)
	{
		FreeAdNumber += amount;
		PlayerPrefs.SetInt("FreeAdsNumber", FreeAdNumber);
		FreeAdNumberUpdated(FreeAdNumber);
	}

	public void RemoveFreeAdNumber(int amount)
	{
		FreeAdNumber -= amount;
		PlayerPrefs.SetInt("FreeAdsNumber", FreeAdNumber);
		FreeAdNumberUpdated(FreeAdNumber);
	}

	//----------------------
}
