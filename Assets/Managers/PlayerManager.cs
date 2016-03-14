using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TeamUtility.IO;
using UnityEngine.Assertions;


/* Menu Scheme 
 * -------------------
 * Players join in at player select
 * - Create player instances there
 * - Start player menu control coroutine
 * - Set player character data here
 * Continue to game-mode selections
 * - Set all game mode specific data here
 * Start Game
 * - Place the characters 
 * - Start game control coroutine
 */

public class PlayerManager : MonoBehaviour {

	public GameObject PLAYER_PREFAB;

	private List<GameObject> mPlayers_ = new List<GameObject>();
	private int mNumberOfPlayers_;
	private Coroutine mUpdateRoutine_; // Keep track of our routine so we can end it

	// Default constructor, that creates players
	PlayerManager () 
	{
		// Grab the current number of joysticks
		mNumberOfPlayers_ = Input.GetJoystickNames().Length;

		// Then create that many players
		for(int i = 0; i < mNumberOfPlayers_; ++i)
		{
			mPlayers_.Add(Instantiate(PLAYER_PREFAB) as GameObject);
			// After creation setup the controller
			PlayerData data = mPlayers_[i].GetComponent<PlayerData>();
			Assert.IsNull(data, "Player Prefab is missing PlayerData component");
			data.SetupPlayer(i);
		}
	}

	void CheckLevelType(LevelTypeEvent ltEvent)
	{
		// If we have a update routine, stop it
		if(mUpdateRoutine_ != null)
		{
			StopCoroutine(mUpdateRoutine_);
			mUpdateRoutine_ = null;
		}

		// For now only on menus do we allow drop in and out
		if(ltEvent.type == LevelTypes.Type.JoinMenu)
		{
			mUpdateRoutine_ = StartCoroutine(Menu_Update());
		}
		// And in game we want track for disconnections
		else if(ltEvent.type == LevelTypes.Type.Game) 
		{
			mUpdateRoutine_ = StartCoroutine(Game_Update());
		}
	}

	// Listen to the space for level events
	void OnEnable()
	{
		EventManager.Instance.Connect<LevelTypeEvent>(CheckLevelType, null);
	}
	// Disconnect from listening to event when we are destroyed
	void OnDistable()
	{
		EventManager.Instance.Disconnect<LevelTypeEvent>(CheckLevelType, null);
	}

	IEnumerator Game_Update()
	{
		// TODO: Start the player movement coroutine

		// Run this coroutine until a level change ends it
		while(true)
		{
			// If the number of players changes
			int newCount = Input.GetJoystickNames().Length;

			// If we have less players, we need to drop a player
			if(newCount  < mNumberOfPlayers_)
			{
				// Launch the controller disconnected screen

			}

			// Return next frame
			yield return null;
		}
	
	}

	// When we are on a menu, we can drop in and out players
	IEnumerator Menu_Update()
	{
		// Run this coroutine until a level change ends it
		while(true)
		{
			// If the number of players changes
			int newCount = Input.GetJoystickNames().Length;

			// If we have less players, we need to drop a player
			if(newCount  < mNumberOfPlayers_)
			{
				// Launch the player's remaining resume screen

			}
			// Otherwise we need to add a player in
			else if(newCount > mNumberOfPlayers_)
			{
				// Then create that many players
				for(int i = mNumberOfPlayers_; i < newCount; ++i)
				{
					mPlayers_.Add(Instantiate(PLAYER_PREFAB) as GameObject);
					// After creation setup the controller
					PlayerData data = mPlayers_[i].GetComponent<PlayerData>();
					Assert.IsNull(data, "Player Prefab is missing PlayerData component");
					data.SetupPlayer(i);
				}
			}

			// Return next frame
			yield return null;
		}
	}
}
