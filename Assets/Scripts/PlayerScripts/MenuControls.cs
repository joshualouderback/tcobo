using UnityEngine;
using System.Collections;

public class MenuControls : MonoBehaviour {
	
	// Note all menus will be navigated with either a direction or a press
	// This means the way to undo is press the selection again 
	// (keeps us at a press any one button game)
	public enum NavigateType { Pressed, Up, Down, Left, Right };

	// Event to send when a player uses controller to navigate menu
	public class MenuNavigateEvent : CustomEvent
	{
		// Constructor for easy sending of new events
		MenuNavigateEvent(NavigateType eType)
		{
			mType = eType;
		}

		public NavigateType mType; // The type of navigation
	}

	// Start Listening to Menu Navigation
	public void StartNavigation()
	{
		//if(mNavigationUpdate_ == null)
			//mNavigationUpdate_ = StartCoroutine(Menu_Navigate());
	}

	// Stop Listening to Menu Navigation
	public void StopNavigation()
	{
		if(mNavigationUpdate_ != null)
			StopCoroutine(mNavigationUpdate_);
	}

	//////////////////////////////////////////////////////////////////////

	private PlayerData mPlayer_;				// Reference to player to get ID for input reading
	private Coroutine mNavigationUpdate_; 		// Coroutine of our update function

	void Start()
	{
		mPlayer_ = this.GetComponent<PlayerData>();

	}
/*
	IEnumerator Menu_Navigate()
	{
		// Get normalized stick direction
		Vector3 direction = new Vector3(InputManager.GetAxis(mLHorizontal_), 
									   -InputManager.GetAxis(mLVertical_), 0).normalized;

		// If any of the directions are outside of the deadzone send it
		if(direction.y > 0.5f)
			EventManager.Instance.SendEvent<MenuNavigateEvent>(new MenuNavigateEvent(NavigateType.Up, mPlayer_.mPlayerID), null);
		if(direction.y < -0.5f)
			EventManager.Instance.SendEvent<MenuNavigateEvent>(new MenuNavigateEvent(NavigateType.Down, mPlayer_.mPlayerID), null);
		if(direction.x > 0.5f)
			EventManager.Instance.SendEvent<MenuNavigateEvent>(new MenuNavigateEvent(NavigateType.Right, mPlayer_.mPlayerID), null);
		if(direction.x < -0.5f)
			EventManager.Instance.SendEvent<MenuNavigateEvent>(new MenuNavigateEvent(NavigateType.Left, mPlayer_.mPlayerID), null);
		// And if they press any button send pressed 
		// TODO: CHECK IF JOYSTICKS ARE INCLUDED IN THIS
		if(InputManager.AnyInput(mPlayer_.mPlayerID))
			EventManager.Instance.SendEvent<MenuNavigateEvent>(new MenuNavigateEvent(NavigateType.Pressed, mPlayer_.mPlayerID), null);
	}
	*/
}
