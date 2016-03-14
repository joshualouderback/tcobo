using UnityEngine;
using System.Collections;

// Game Manager is responsible for storing game mode
// And then calling the corresponding game mode function
public class GameManager : MonoBehaviour 
{
	// Default to classic
	public GameModes.Type modeType = GameModes.Type.Classic;
	public LevelTypes.Type levelType				 // We need to know what level we are on 
	{
		get
		{
			return levelType;
		}
		set
		{
			levelType = value;
		}
	}

	private GameModes.InitFunction modeInitFunction; // Init function for that game mode
	private GameModes.UpdateFunction modeUpdateFunction; // Init function for that game mode
		
	// Settor to let game manager know what level we are on
	private void SetLevelType(LevelTypeEvent e)
	{
		levelType = e.type;
	}

	// Listen to the space for level events
	void OnEnable()
	{
		EventManager.Instance.Connect<LevelTypeEvent>(SetLevelType, null);
	}
	// Disconnect from listening to event when we are destroyed
	void OnDistable()
	{
		EventManager.Instance.Disconnect<LevelTypeEvent>(SetLevelType, null);
	}

	// 
	void Start () 
	{
	}

	// When we load a level, check if we need to init a game mode
	void OnLevelWasLoaded()
	{
		// Check to see if it is a game level
		if(levelType == LevelTypes.Type.Game)
		{
			// Grab the init function based off game mode
			modeInitFunction = GameModes.GetGameModeInit(modeType);
			// Call the method, so our game mode can be started
			modeInitFunction();
		}
	}

}
