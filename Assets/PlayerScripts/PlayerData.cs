using UnityEngine;
using UnityEditor;
using System;
using TeamUtility.IO;

public class PlayerData
{
	public PlayerID mPlayerID = PlayerID.Invalid;
	
	public void SetupPlayer(int playerNumber)
	{
		// Convert int to our enumeration
		mPlayerID = (PlayerID)playerNumber;

		switch(Application.platform)
		{
		case RuntimePlatform.WindowsPlayer:
			InputManager.SetInputConfiguration("XBox_360_Windows", mPlayerID);
			break;
		case RuntimePlatform.OSXPlayer:
			InputManager.SetInputConfiguration("XBox_360_OSX", mPlayerID);
			break;
		case RuntimePlatform.LinuxPlayer:
			InputManager.SetInputConfiguration("XBox_360_Linux", mPlayerID);
			break;
		default:
			break;
		}
	}
}

