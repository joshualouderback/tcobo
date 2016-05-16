using UnityEngine;
using System.Collections;

// Game Manager is responsible for storing game mode
// And then calling the corresponding game mode function
public class GameManager : MonoBehaviour 
{
	// Default to classic
	public GameModes.Type ModeType;						  // We need to know what mode we are using
	public Levels.Enum LevelEnum;					 	  // We need to know what level we are on 

	private GameModes.InitFunction modeInitFunction_; 	  // Init function for that game mode
	private GameModes.UpdateFunction modeUpdateFunction_; // Init function for that game mode
	private LevelTypes.Type levelType_;					  // We need to know what type of level we are on

	// Settor to let game manager know what level we are on
	private void SetLevelType(LevelTypeEvent e)
	{
		levelType_ = e.type;
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
		if(levelType_ == LevelTypes.Type.Game)
		{
			// TODO: Add some way of telling the spawn manager to create the correct spawner
			// OR: Get it to just access game manager

			// Grab the init function based off game mode
			modeInitFunction_ = GameModes.GetGameModeInit(ModeType);
			// Call the method, so our game mode can be started
			modeInitFunction_();
		}
	}

}
