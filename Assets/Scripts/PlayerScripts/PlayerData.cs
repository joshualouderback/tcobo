using UnityEngine;
using System;

public class PlayerData : MonoBehaviour
{
	//public PlayerID mPlayerID = PlayerID.Invalid;

	public void SetupPlayer(int playerNumber)
	{
		// Convert int to our enumeration
		//mPlayerID = (PlayerID)playerNumber;

		Debug.Log(Application.platform);

		switch(Application.platform)
		{
		case RuntimePlatform.WindowsPlayer:
			//InputManager.SetInputConfiguration("XBox_360_Windows", mPlayerID);
			break;
		case RuntimePlatform.OSXPlayer:
			//InputManager.SetInputConfiguration("XBox_360_OSX", mPlayerID);
			break;
		case RuntimePlatform.LinuxPlayer:
			//InputManager.SetInputConfiguration("XBox_360_Linux", mPlayerID);
			break;
		default:
			// Bind our configuration
			//InputManager.SetInputConfiguration("XBox_360_Windows" + playerNumber, mPlayerID);
			break;
		}
	}
}

