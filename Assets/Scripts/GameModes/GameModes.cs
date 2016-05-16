using UnityEngine;
using System.Collections;

public class GameModes 
{
	// Format your GameModeInit like so
	public delegate void InitFunction();
	public delegate void UpdateFunction();

	// Declare game mode types here
	public enum Type 
	{ 
		// Must start with zero
		Classic = 0,
		COUNT
	};

	// Add case so we can get your init game function
	static public InitFunction GetGameModeInit(GameModes.Type type)
	{
		switch(type)
		{
		default:
		case Type.Classic:
			return ClassicMode.InitializeMode;
		}
	}

	static public UpdateFunction GetGameModeUpdate(GameModes.Type type)
	{
		switch(type)
		{
		default:
		case Type.Classic:
			return ClassicMode.UpdateMode;
		}
	}

}
