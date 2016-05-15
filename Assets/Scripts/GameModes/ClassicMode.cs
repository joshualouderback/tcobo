using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

// The code for how classic mode is started and ran
public class ClassicMode 
{
	// Make static Initialize Method, in same format as GameModeInit delegate
	// This way we can call this method from anywhere without a need for instance
	static public void InitializeMode()
	{
		// TODO: ALL BELOW

		// Set each player's position


		// Randomly assign the shuriken
		int shurikenHolder = Random.Range(0, Singleton<PlayerManager>.Instance.mPlayers.Count);
		for(int i = 0; i < Singleton<PlayerManager>.Instance.mPlayers.Count; ++i)
		{
			GameObject player = Singleton<PlayerManager>.Instance.mPlayers[i];

			// If this player is the shuriken holder
			if(i == shurikenHolder)
			{
				// Give them shuriken and start throw coroutine
				Singleton<ShurikenManager>.Instance.GiveShuriken(i);
				Throw throwLogic = player.GetComponent<Throw>();
				Assert.IsNull(throwLogic, "Player prefab is missing Throw logic");
				throwLogic.EnableThrowing();
			}
			else // Otherwise they don't have it, so start catching coroutine
			{
				Catch catchLogic = player.GetComponent<Catch>();
				Assert.IsNull(catchLogic, "Player prefab is missing Catch component.");
				catchLogic.EnableCatching();
			}
		}

		return;
	}

	// Make static Update Method, in same format as GameModeUpdate delegate
	// This way we can call this method from anywhere without a need for instance
	static public void UpdateMode()
	{
		return;
	}
}
