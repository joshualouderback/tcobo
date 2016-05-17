using UnityEngine;
using UnityEngine.InputNew;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class SetInputEvent : CustomEvent
{
	public SetInputEvent(PlayerInput input)
	{
		Input = input;
	}

	public PlayerInput Input;
}
	
public class JoinGameEvent : CustomEvent 
{
	public JoinGameEvent(int playerIndex, int spriteIndex)
	{
		PlayerIndex = playerIndex;
		SpriteIndex = spriteIndex;
	}

	public int PlayerIndex;
	public int SpriteIndex;
}

public class LeaveGameEvent : CustomEvent 
{
	public LeaveGameEvent(int playerIndex)
	{
		PlayerIndex = playerIndex;
	}

	public int PlayerIndex;
}


public class PlayerManager : MonoBehaviour {

	// -- Public
	// TODO: Remove eventually
	// Public but not editable in editor, for accessing players
	[HideInInspector]
	public List<GameObject> mPlayers = new List<GameObject>();

	[Header("Player Manager object must be in first level and never deleted.")]
	public PlayerInput characterSelectHUDPrefab; // Prefab for the HUD object for character select
	public ButtonAction joinAction;				 // Input action for joing
	public ButtonAction leaveAction;			 // Input action for leaving
	public int NumberOfPlayers					 // Public gettor for number of players joined in the game
	{
		get { return players.Count; }
	}

	// -- Private
	private List<GameObject> HUDPositions;		 // HUD objects for the join sprite
	private Sprite[] characterSprites_; 		 // Array of Sprites of the Characters
	private PlayerHandle globalHandle;			 // Global Handle to listen to new devices to join in game
	private List<PlayerInfo> players;			 // PlayerInfo array to keep track of players to join
	private class PlayerInfo 					 // Player info to keep track of handles and gameobjects
	{
		public PlayerHandle handle;				 // Input handle
		public GameObject hudObj;				 // Reference to the hud object
		public GameObject playerObj;			 // Reference to the player object we spawn
		public bool ready = false;				 // Keep track if we are ready TODO: May remove

		public PlayerInfo(PlayerHandle playerHandle, GameObject obj)
		{
			this.handle = playerHandle;
			hudObj = obj;
		}
	}
		
	public void Start()
	{
		// Create our arrays to store our data
		HUDPositions = new List<GameObject>();
		players = new List<PlayerInfo>();
		// Load up character sprites for use
		characterSprites_ =  Resources.LoadAll<Sprite>("CharacterSprites");	

		// Don't even destroy the player manager
		DontDestroyOnLoad(this.gameObject);

		// Get HUD prefab and get it's sprite
		GameObject hud = characterSelectHUDPrefab.gameObject;
		Sprite sprite = hud.GetComponent<SpriteRenderer>().sprite;
		
		// So we can use the screen and sprite dimensions to calculate even placement
		float screenAspect = (float)Screen.width / (float)Screen.height;
		float cameraHeight = Camera.main.orthographicSize;
		float xPosition = sprite.bounds.extents.x * hud.transform.localScale.x;
		float spriteWidth = xPosition;
		float yPosition = (cameraHeight) - sprite.bounds.extents.y * hud.transform.localScale.y;
		float gap = ((screenAspect * cameraHeight * 2.0f) - (xPosition * 4.0f)) / 4.0f;
		xPosition -= Camera.main.transform.position.x + (screenAspect * cameraHeight);

		// Once calculated we can then place them
		for(int i = 0; i < 4; ++i)
		{
			GameObject obj = Instantiate(Resources.Load("JoinButtonPrefab")) as GameObject;
			DontDestroyOnLoad(obj);
			obj.transform.position = new Vector3(xPosition, yPosition, 0);
			HUDPositions.Add(obj);
			xPosition += spriteWidth + gap;
		}
			
		// Create a global player handle that listen to all relevant devices not already used
		// by other player handles.
		globalHandle = PlayerHandleManager.GetNewPlayerHandle();
		globalHandle.global = true;
		List<ActionMapSlot> actionMaps = characterSelectHUDPrefab.GetComponent<PlayerInput>().actionMaps;
		foreach (ActionMapSlot actionMapSlot in actionMaps)
		{
			ActionMapInput actionMapInput = ActionMapInput.Create(actionMapSlot.actionMap);
			actionMapInput.TryInitializeWithDevices(globalHandle.GetApplicableDevices());
			actionMapInput.active = actionMapSlot.active;
			actionMapInput.blockSubsequent = actionMapSlot.blockSubsequent;
			globalHandle.maps.Add(actionMapInput);
		}

		// Now bind the joinActions and leaveActions to our global handle to listen to
		joinAction.Bind(globalHandle);
		leaveAction.Bind(globalHandle);
	}
		
	// Connecting to space wide join and leave events
	void OnEnable()
	{
		EventManager.Instance.Connect<JoinGameEvent>(OnJoinGameEvent, null);
		EventManager.Instance.Connect<LeaveGameEvent>(OnLeaveGameEvent, null);
	}
	// Disconnecting to space wide join and leave events
	void OnDisable()
	{
		EventManager.Instance.Disconnect<JoinGameEvent>(OnJoinGameEvent, null);
		EventManager.Instance.Disconnect<LeaveGameEvent>(OnLeaveGameEvent, null);
	}

	// Every frame check for new devices
	public void Update()
	{
		// Listen to if the join button was pressed on a yet unassigned device.
		if (joinAction.control.wasJustPressed)
		{
			// These are the devices currently active in the global player handle.
			List<InputDevice> devices = globalHandle.GetActions(joinAction.action.actionMap).GetCurrentlyUsedDevices();

			PlayerHandle handle = PlayerHandleManager.GetNewPlayerHandle();
			foreach (var device in devices)
				handle.AssignDevice(device, true);

			foreach (ActionMapSlot actionMapSlot in characterSelectHUDPrefab.actionMaps)
			{
				ActionMapInput map = ActionMapInput.Create(actionMapSlot.actionMap);
				map.TryInitializeWithDevices(handle.GetApplicableDevices());
				map.blockSubsequent = actionMapSlot.blockSubsequent;

				// Activate the ActionMap that is used to join,
				// disregard active state from ActionMapSlots for now (wait until instantiating player).
				if (map.actionMap == joinAction.action.actionMap)
					map.active = true;

				handle.maps.Add(map);
			}

			// Instantiate the player HUD selection object
			var playerHUD = (PlayerInput)Instantiate(characterSelectHUDPrefab, HUDPositions[players.Count].transform.position, 
				                                     Quaternion.identity);
			DontDestroyOnLoad(playerHUD);
			// Give the player their ID
			playerHUD.GetComponent<SelectControls>().PlayerID = players.Count;
			// Turn off the join HUD for that player
			HUDPositions[players.Count].SetActive(false);
			// Keep a reference to our player's handle and object
			players.Add(new PlayerInfo(handle, playerHUD.gameObject));
			// Then attach the handle to that object
			playerHUD.handle = handle;
		}
	}

	void OnJoinGameEvent(JoinGameEvent joinEvent)
	{
		// TODO: Ask spawn manager for position
		Vector3 spawnPosition = new Vector3(0,0,0);

		// Spawn the player there, and keep them from being destroyed by game loads
		players[joinEvent.PlayerIndex].playerObj = (GameObject) Instantiate(Resources.Load<GameObject>("DefaultPlayer"), 
			                                                                spawnPosition, Quaternion.identity);
		DontDestroyOnLoad(players[joinEvent.PlayerIndex].playerObj);

		// Give the game object it's input
		EventManager.Instance.SendEvent<SetInputEvent>
		(
			new SetInputEvent(players[joinEvent.PlayerIndex].hudObj.GetComponent<PlayerInput>()), 
		  	players[joinEvent.PlayerIndex].playerObj
		);

		// Load the correct sprite into the object
		players[joinEvent.PlayerIndex].playerObj.GetComponent<SpriteRenderer>().sprite = characterSprites_[joinEvent.SpriteIndex];
	}

	void OnLeaveGameEvent(LeaveGameEvent leaveEvent)
	{
		// Destroy the player
		if(players[leaveEvent.PlayerIndex].playerObj != null)
			Destroy(players[leaveEvent.PlayerIndex].playerObj);
	}

	void StartGame()
	{
	}

}
