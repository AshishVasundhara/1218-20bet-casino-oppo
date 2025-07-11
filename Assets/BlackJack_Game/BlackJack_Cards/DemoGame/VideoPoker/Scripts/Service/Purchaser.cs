//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Purchasing;

//public class Purchaser : MonoBehaviour, IStoreListener
//{
//	public static Purchaser Instance;
//	private static IStoreController m_StoreController;
//	private static IExtensionProvider m_StoreExtensionProvider;

//	public static string PRODUCT_5000_COINS = "iap_5000coins.";
//	public static string PRODUCT_12000_COINS = "iap_12000coins.";
//	public static string PRODUCT_30000_COINS = "iap_30000coins.";
//	public static string PRODUCT_150000_COINS = "iap_150000coins.";
//	public static string PRODUCT_500000_COINS = "iap_500000coins.";


//	void Start ()
//	{		
//		if (m_StoreController == null) {			
//			InitializePurchasing ();
//		}
//		if (Instance == null) {
//			Instance = this;
//		}
			
//	}

//	public void InitializePurchasing ()
//	{		
//		if (IsInitialized ()) {			
//			return;
//		}
			

//		var builder = ConfigurationBuilder.Instance (StandardPurchasingModule.Instance ());

//		builder.AddProduct (PRODUCT_5000_COINS, ProductType.Consumable);
//		builder.AddProduct (PRODUCT_12000_COINS, ProductType.Consumable);
//		builder.AddProduct (PRODUCT_30000_COINS, ProductType.Consumable);
//		builder.AddProduct (PRODUCT_150000_COINS, ProductType.Consumable);
//		builder.AddProduct (PRODUCT_500000_COINS, ProductType.Consumable);

//		UnityPurchasing.Initialize (this, builder);
//	}


//	private bool IsInitialized ()
//	{		
//		return m_StoreController != null && m_StoreExtensionProvider != null;
//	}

//	public void Buy5000Coins ()
//	{
//		SoundController.Sound.ClickBtn ();
//		BuyProductID (PRODUCT_5000_COINS);
//	}

//	public void Buy12000Coins ()
//	{	
//		SoundController.Sound.ClickBtn ();	
//		BuyProductID (PRODUCT_12000_COINS);
//	}

//	public void Buy30000Coins ()
//	{	
//		SoundController.Sound.ClickBtn ();	
//		BuyProductID (PRODUCT_30000_COINS);
//	}

//	public void Buy150000Coins ()
//	{	
//		SoundController.Sound.ClickBtn ();	
//		BuyProductID (PRODUCT_150000_COINS);
//	}

//	public void Buy500000Coins ()
//	{	
//		SoundController.Sound.ClickBtn ();	
//		BuyProductID (PRODUCT_500000_COINS);
//	}
		

//	/// <summary>
//	///  _Event_Event_Event_Event_Event_Event_Event_Event_Event
//	/// </summary>
//	void BuyProductID (string productId)
//	{		
//		if (IsInitialized ()) {						
//			Product product = m_StoreController.products.WithID (productId);

//			if (product != null && product.availableToPurchase) {
//				Debug.Log (string.Format ("Purchasing product asychronously: '{0}'", product.definition.id));
//				m_StoreController.InitiatePurchase (product);
//			} else {				
//				Debug.Log ("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
//			}
//		} else {
//			Debug.Log ("BuyProductID FAIL. Not initialized.");
//		}
//	}

//	public void RestorePurchases ()
//	{		
//		if (!IsInitialized ()) 
//		{			
//			Debug.Log ("RestorePurchases FAIL. Not initialized.");
//			SoundController.Sound.DisactiveButtonSound();
//			return;
//		}
			
//		if (Application.platform == RuntimePlatform.IPhonePlayer
//		    || Application.platform == RuntimePlatform.OSXPlayer) {			
//			Debug.Log ("RestorePurchases started ...");

//			var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions> ();

//			apple.RestoreTransactions ((result) => 
//				{
//					SoundController.Sound.DisactiveButtonSound();
//				Debug.Log ("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
//			});
//		} else {
//			SoundController.Sound.DisactiveButtonSound();
//			Debug.Log ("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
//		}
//	}
//	//
//	// --- IStoreListener
//	//
//	public void OnInitialized (IStoreController controller, IExtensionProvider extensions)
//	{		
//		Debug.Log ("OnInitialized: PASS");

//		m_StoreController = controller;
//		m_StoreExtensionProvider = extensions;
//	}

//	public void OnInitializeFailed (InitializationFailureReason error)
//	{		
//		Debug.Log ("OnInitializeFailed InitializationFailureReason:" + error);
//	}

//	public PurchaseProcessingResult ProcessPurchase (PurchaseEventArgs args)
//	{
//		// A consumable product has been purchased by this user.
//		if (String.Equals (args.purchasedProduct.definition.id, PRODUCT_5000_COINS, StringComparison.Ordinal)) {
//			DataManager.Instance.AddCoins(5000);
//		}
//		if (String.Equals (args.purchasedProduct.definition.id, PRODUCT_12000_COINS, StringComparison.Ordinal)) {
//			DataManager.Instance.AddCoins(12000);
//		}
//		if (String.Equals (args.purchasedProduct.definition.id, PRODUCT_30000_COINS, StringComparison.Ordinal)) {
//			DataManager.Instance.AddCoins(30000);


//		}
//		if (String.Equals (args.purchasedProduct.definition.id, PRODUCT_150000_COINS, StringComparison.Ordinal)) {
//			DataManager.Instance.AddCoins(150000);


//		}
//		if (String.Equals (args.purchasedProduct.definition.id, PRODUCT_500000_COINS, StringComparison.Ordinal)) {
//			DataManager.Instance.AddCoins(500000);


//		}
//		//------------

//		return PurchaseProcessingResult.Complete;

//	}

//	public void OnPurchaseFailed (Product product, PurchaseFailureReason failureReason)
//	{
//		// A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
//		// this reason with the user to guide their troubleshooting actions.
//		Debug.Log (string.Format ("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
//	}
//}
////}
