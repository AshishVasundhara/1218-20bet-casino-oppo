using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace I2.MiniGames
{
	[System.Serializable]public class UnityEventCurrencyCallbk : UnityEvent<int, System.Action<bool>>{}

	// This Class implements the flow and monetization logic for all MiniGames
	// It provides some free rounds and then allow paying for extra chances and purchasing currency if there is not enough. 
    [AddComponentMenu("I2/MiniGames/Controller")]
    public class MiniGame_Controller : MonoBehaviour
	{
		#region Variables

		public MiniGame _Game;						// Game this controllers manages
		public bool _StartGameOnEnable = false;		// If true, the game will be initialized when this object is enabled
		public float _TimeBeforeStartRound = 0;		// This delay can be used to start the round after some effects/sounds are finished

		//--[ Events to hide/show labels and update texts ]--------------

		public UnityEvent 		_OnStartPlaying 	= new UnityEvent();			// Show/Hide objects for the initial state, execute the entrance effect and call OnReadyForNextRound right after it finishes
		public UnityEvent 		_OnRoundStarted	    = new UnityEvent();
		public UnityEvent 		_OnGameOver 	    = new UnityEvent();

		public UnityEventString _OnUpdateFreeRounds = new UnityEventString();	// Shows the Free Round button and updates the label's text following the _FreeRoundsLabelFormat


		public UnityEventString _OnUpdateRoundCost 	= new UnityEventString();	// Hides the Free Round button, shows the Buy Chance

		private bool mIsPlaying;	  // Its true whenever the game is initialized and its possible to play another round

		#endregion

		#region Currency Management

		// Fake Currency (this is used only for the demo, when used in a game, the _OnConsumeCurrency event 
		// should be redirect to the game's resource to decrease the currency and call OnConsumeCurrencyResult
		// with true/false depending on whatever there was enough currency available
		private int CurrencyAmount;

		// function used to start the consume and purchase if necessary. It implements a fake approach for the demo
		// and a normal flow when _OnConsumeCurrency its set
		public void TryConsumeCurrency( int Amount )
		{

		}

		// Example of ConsumeCurrency function
		public void TestConsumeCurrency( int Amount, System.Action<bool> result )
		{
			if (CurrencyAmount < Amount)
			{
				result( false );
			}
			else
			{
				CurrencyAmount -= Amount;
				result( true );
			}
		}
			
		// This is only used in the demo to simulate user's purchase
		public void AddCurrency( int Amount )
		{
			CurrencyAmount += Amount;
		}
		// This is only used in the demo to simulate user's purchase (this version is used as NGUI doesn't support passing an int to the Button's OnClick)
		public void AddTestCurrency()
		{
			AddCurrency (25);
		}

		#endregion

		#region Setup and State Management

		public void OnEnable()
		{
			if (_StartGameOnEnable)
				Invoke("StartGame", 0);
		}

		// Initializes the game and starts the first round
		public void StartGame()
		{

			mIsPlaying = true;

			_Game.SetupGame ();

			_OnStartPlaying.Invoke ();  // should call OnReadyForNextRound when finishing the entrance effect;

			if (_TimeBeforeStartRound > 0)
				Invoke ("OnReadyForNextRound", _TimeBeforeStartRound);
			else
			if (_TimeBeforeStartRound >= 0)
				OnReadyForNextRound ();
		}

		// Stops the game and prevent any further play intent until the game is reseted
		public virtual void StopGame()
		{
			mIsPlaying = false;
			_OnGameOver.Invoke();
		}

		// Restarts the game from the begining
		public virtual void ResetGame()
		{
			StopGame ();
			StartGame ();
		}

		// Execute _OnGameOver, _OnUpdateFreeRounds or _OnUpdateRoundCost based on game state
		public void OnReadyForNextRound()
		{
		}

		#endregion

		#region Validation

		// Checks if there are free rounds or enough currency to buy a new round. 
		// If so, it calls AllowRound, otherwise it shows the BuyCurrency popup or denies the round
		public void ValidateRound()
		{
			//--[ validate costs and call AllowRound/DenyRound accordinly ]-----

			if (!mIsPlaying)
			{
				DenyRound();
				return;
			}

			AllowRound ();
		}


		// After the round is validated, this function continues the game flow to play another round (PrizeWheel: spins the wheel, etc)
		public void AllowRound()
		{
			_OnRoundStarted.Invoke ();

			_Game.StartRound ();
		}

		// The round validation failed, so this function stops the current play attempt 
		public void DenyRound()
		{
			_Game.CancelRound ();
		}

		#endregion
	}
}